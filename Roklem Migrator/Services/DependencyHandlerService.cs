using Roklem_Migrator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services
{
    internal class DependencyHandlerService : IDependencyHandlerService
    {
        private readonly IInvokeAzureAIRequestResponseService _InvokeAzureAIRequestResponseService;
        private readonly ISpinnerService _SpinnerService;
        private readonly IFileReaderService _FileReaderService;
        private readonly ICommonService _CommonService;

        public DependencyHandlerService(IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, ISpinnerService spinnerService, IFileReaderService fileReaderService, ICommonService commonService)
        {

            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
            _SpinnerService = spinnerService;
            _FileReaderService = fileReaderService;
            _CommonService = commonService;
        }

        public List<string> getPackageDependencies(List<string> files, string srcPath) {
            var cts = new CancellationTokenSource();
            var spinnerTask = Task.Run(() => _SpinnerService.ShowSpinner(cts.Token, "Finding files with likely dependencies:", "Files with likely dependecies found."));

            List<string> dependencyFiles = _InvokeAzureAIRequestResponseService
                .InvokeRequestResponse(
                    "Here are some of the files in a VB .Net Framework project. Which of these does likely include dependecies to external packages? Give the file as a list with each file on a new line. Respond with no other text or characters.\r\nFile types:",
                    files)
                .Result
                .Trim()
                .Split("\n")
                .Select(file => file.Trim())
                .ToList();

            cts.Cancel();
            spinnerTask.Wait();

            List<string> projectDependencies = new List<string>();

            cts = new CancellationTokenSource();
            spinnerTask = Task.Run(() => _SpinnerService.ShowSpinner(cts.Token, "Finding project dependencies.", "Project dependencies found."));

            foreach (string filePath in dependencyFiles) {
                string sourcePath = Path.Combine(srcPath, filePath).Trim();
                string fileName = Path.GetFileName(sourcePath);

                IEnumerable<string> fileLines = _FileReaderService.ReadFile(sourcePath);                

                List<string> fileDependencies = _InvokeAzureAIRequestResponseService
                    .InvokeRequestResponse(
                        $"Here are are the lines of code in a VB .Net Framework file named {fileName}. Which package dependencies does the file have? Give the dependecy package names as a list, each package name on a new line. If no dependency is found, respond with No dependency found Respond with no other text or characters.\r\nLines:",
                        fileLines.ToList())
                    .Result
                    .Trim()
                    .Split("\n")
                    .Select(dep => dep.Trim())
                    .ToList();

                if (fileDependencies[0] != "No dependency found")
                {
                    projectDependencies.AddRange(
                        fileDependencies
                            .Where(dep => !projectDependencies.Contains(dep.Trim()))
                            .Select(dep => dep.Trim())
                    );
                }
            }

            cts.Cancel();
            spinnerTask.Wait();

            Console.WriteLine("Project dependencies found:");
            _CommonService.printList(projectDependencies);

            return projectDependencies;
        }
    }
}
