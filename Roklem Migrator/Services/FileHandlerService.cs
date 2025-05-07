using Roklem_Migrator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services
{
    internal class FileHandlerService : IFileHandlerService
    {
        private readonly IInvokeAzureAIRequestResponseService _InvokeAzureAIRequestResponseService;
        private readonly IFileLocatorService _FileLocatorService;
        private readonly ISpinnerService _SpinnerService;

        public FileHandlerService(IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, IFileLocatorService fileLocatorService, ISpinnerService spinnerService)
        {
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
            _FileLocatorService = fileLocatorService;
            _SpinnerService = spinnerService;
        }

        public void copyFiles(List<string> filesToCopy, string targetPath)
        {
            
        }

        public (List<string> filesToMigrate, List<string> filesToCopy) distinguisFiles(List<string> files)
        {
            List<string> fileTypes = _FileLocatorService.getFileTypes(files);

            Console.WriteLine("\nFile types:");
            _FileLocatorService.printFileList(fileTypes);

            var cts = new CancellationTokenSource();
            var spinnerTask = Task.Run(() => _SpinnerService.ShowSpinner(cts.Token, "Analyzing project files."));

            List<string> fileTypesToMigrate = _InvokeAzureAIRequestResponseService
                .InvokeRequestResponse(
                    "Here are the file types in a VB .Net Framework project. Which will need editing in the migration? Give the file types as a list with each type on a new line. Respond with no other text or characters.\r\nFile types:",
                    fileTypes)
                .Result
                .Trim()
                .Split("\n")
                .ToList();

            cts.Cancel();
            spinnerTask.Wait();

            Console.WriteLine("\nFile types that need editing:");
            _FileLocatorService.printFileList(fileTypesToMigrate);

            List<string> fileTypesToCopy = fileTypes
            .Select(ft => ft.Trim().ToLowerInvariant())
            .Except(fileTypesToMigrate.Select(ft => ft.Trim().ToLowerInvariant()))
            .ToList();

            Console.WriteLine("\nFile types that can be copied:");
            _FileLocatorService.printFileList(fileTypesToCopy);

            List<string> filesToCopy = files
                .Where(file => fileTypesToCopy.Any(type => file.EndsWith(type, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            List<string> filesToMigrate = files
                .Where(file => !fileTypesToCopy.Any(type => file.EndsWith(type, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            Console.WriteLine("\nFiles that need to be migrated:");
            _FileLocatorService.printFileList(filesToMigrate);

            Console.WriteLine("\nFiles that can be copied:");
            _FileLocatorService.printFileList(filesToCopy);

            return (filesToMigrate, filesToCopy);
        }
    }
}
