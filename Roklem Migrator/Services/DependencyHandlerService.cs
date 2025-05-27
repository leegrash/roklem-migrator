using Roklem_Migrator.Services.Interfaces;
using System.Xml.Linq;

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

        public List<string> getPackageDependencies(List<string> files, string srcPath)
        {
            var cts = new CancellationTokenSource();
            var spinnerTask = Task.Run(() => _SpinnerService.ShowSpinner(cts.Token, "Finding files with likely dependencies:", "Files with likely dependencies found:"));

            List<string> dependencyFiles = _InvokeAzureAIRequestResponseService
                .InvokeRequestResponse(
                    "Here are some of the files in a VB .Net Framework project. Which of these does likely include dependencies to external packages? Give the file as a list with each file on a new line. Respond with no other text or characters.\r\nFile types:",
                    files)
                .Result
                .Trim()
                .Split("\n")
                .Select(file => file.Trim())
                .ToList();

            cts.Cancel();
            spinnerTask.Wait();

            _CommonService.printList(dependencyFiles);

            var guaranteedFiles = files.Where(f =>
                Path.GetFileName(f).Equals("packages.config", StringComparison.OrdinalIgnoreCase) ||
                f.EndsWith(".vbproj", StringComparison.OrdinalIgnoreCase) ||
                Path.GetFileName(f).Equals("web.config", StringComparison.OrdinalIgnoreCase) ||
                Path.GetFileName(f).Equals("app.config", StringComparison.OrdinalIgnoreCase)
            ).ToList();

            foreach (var guaranteedFile in guaranteedFiles)
            {
                if (!dependencyFiles.Contains(guaranteedFile, StringComparer.OrdinalIgnoreCase))
                    dependencyFiles.Add(guaranteedFile);
            }

            List<string> projectDependencies = new List<string>();

            cts = new CancellationTokenSource();
            spinnerTask = Task.Run(() => _SpinnerService.ShowSpinner(cts.Token, "Finding project dependencies.", "Project dependencies found:"));

            foreach (string filePath in dependencyFiles)
            {
                string sourcePath = Path.Combine(srcPath, filePath).Trim();

                List<string> fileDependencies = ExtractDependenciesFromFile(sourcePath);

                projectDependencies.AddRange(
                    fileDependencies
                        .Where(dep => !projectDependencies.Contains(dep.Trim()))
                        .Select(dep => dep.Trim())
                );
            }

            cts.Cancel();
            spinnerTask.Wait();

            _CommonService.printList(projectDependencies);

            return projectDependencies;
        }

        private List<string> ExtractDependenciesFromFile(string filePath)
        {
            var dependencies = new List<string>();
            string fileName = Path.GetFileName(filePath);

            try
            {
                if (fileName.Equals("packages.config", StringComparison.OrdinalIgnoreCase))
                {
                    var doc = XDocument.Load(filePath);
                    dependencies.AddRange(
                        doc.Descendants("package")
                           .Select(p => p.Attribute("id")?.Value)
                           .Where(id => !string.IsNullOrWhiteSpace(id))!
                    );
                }
                else if (fileName.EndsWith(".vbproj", StringComparison.OrdinalIgnoreCase))
                {
                    var doc = XDocument.Load(filePath);
                    dependencies.AddRange(
                        doc.Descendants("Reference")
                           .Select(r => r.Attribute("Include")?.Value?.Split(',')[0])
                           .Where(name => !string.IsNullOrWhiteSpace(name))!
                           .Select(name => name!)
                    );
                }
                else if (fileName.Equals("web.config", StringComparison.OrdinalIgnoreCase) ||
                         fileName.Equals("app.config", StringComparison.OrdinalIgnoreCase))
                {
                    var doc = XDocument.Load(filePath);
                    XNamespace ns = "urn:schemas-microsoft-com:asm.v1";
                    dependencies.AddRange(
                        doc.Descendants(ns + "assemblyIdentity")
                           .Select(ai => ai.Attribute("name")?.Value)
                           .Where(name => !string.IsNullOrWhiteSpace(name))!
                           .Select(name => name!)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse dependencies from {fileName}: {ex.Message}");
            }

            return dependencies.Distinct().ToList();
        }
    }
}
