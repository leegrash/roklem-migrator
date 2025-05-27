using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class FileHandlerService : IFileHandlerService
    {
        private readonly IInvokeAzureAIRequestResponseService _InvokeAzureAIRequestResponseService;
        private readonly IFileLocatorService _FileLocatorService;
        private readonly ISpinnerService _SpinnerService;
        private readonly ICommonService _CommonService;

        public FileHandlerService(IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, IFileLocatorService fileLocatorService, ISpinnerService spinnerService, ICommonService commonService)
        {
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
            _FileLocatorService = fileLocatorService;
            _SpinnerService = spinnerService;
            _CommonService = commonService;
        }

        public void copyFiles(List<string> filesToCopy, string srcPath, string targetPath)
        {
            foreach (string filePath in filesToCopy)
            {
                string sourcePath = Path.Combine(srcPath, filePath);
                string destinationPath = Path.Combine(targetPath, filePath);

                string? destinationDirectory = Path.GetDirectoryName(destinationPath);
                if (!string.IsNullOrEmpty(destinationDirectory) && !Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                Console.WriteLine($"Copying file: {filePath} to {targetPath}");
                File.Copy(sourcePath, destinationPath, overwrite: true);
            }
        }

        public (List<string> filesToMigrate, List<string> filesToCopy) distinguisFiles(List<string> files)
        {
            List<string> fileTypes = _FileLocatorService.getFileTypes(files);

            Console.WriteLine("\nFile types:");
            _CommonService.printList(fileTypes);

            var cts = new CancellationTokenSource();
            var spinnerTask = Task.Run(() => _SpinnerService.ShowSpinner(cts.Token, "Analyzing project files.", "Project files analyzed."));

            List<string> fileTypesToMigrate = _InvokeAzureAIRequestResponseService
                .InvokeRequestResponse(
                    "Here are the file types in a VB .Net Framework project. Which will need editing in the migration? Give the file types as a list with each type on a new line. Respond with no other text or characters.\r\nFile types:",
                    fileTypes,
                    0)
                .Result
                .Trim()
                .Split("\n")
                .ToList();

            cts.Cancel();
            spinnerTask.Wait();

            Console.WriteLine("\nFile types that need editing:");
            _CommonService.printList(fileTypesToMigrate);

            List<string> fileTypesToCopy = fileTypes
            .Select(ft => ft.Trim().ToLowerInvariant())
            .Except(fileTypesToMigrate.Select(ft => ft.Trim().ToLowerInvariant()))
            .ToList();

            Console.WriteLine("\nFile types that can be copied:");
            _CommonService.printList(fileTypesToCopy);

            List<string> filesToCopy = files
                .Where(file => fileTypesToCopy.Any(type => file.EndsWith(type, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            List<string> filesToMigrate = files
                .Where(file => !fileTypesToCopy.Any(type => file.EndsWith(type, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            Console.WriteLine("\nFiles that need to be migrated:");
            _CommonService.printList(filesToMigrate);

            Console.WriteLine("\nFiles that can be copied:");
            _CommonService.printList(filesToCopy);

            return (filesToMigrate, filesToCopy);
        }

        public List<string> getFileNames(List<string> files)
        {
            return files.Select(file => Path.GetFileName(file)).ToList();
        }
    }
}
