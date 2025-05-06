using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class CodeMigratorService : ICodeMigratorService
    {
        private readonly IInvokeAzureAIRequestResponseService _InvokeAzureAIRequestResponseService;
        private readonly IFileLocatorService _FileLocatorService;
        private readonly ISpinnerService _SpinnerService;

        public CodeMigratorService(IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, IFileWriterService fileWriterService, IFileLocatorService fileLocatorService, ISpinnerService spinnerService)
        {
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
            _FileLocatorService = fileLocatorService;
            _SpinnerService = spinnerService;
        }

        public bool Migrate(string filePath)
        {
            try
            {
                List<string> files = _FileLocatorService.locateFiles(filePath);

                Console.WriteLine($"Located {files.Count} files");

                List<string> fileTypes = _FileLocatorService.getFileTypes(files);

                Console.WriteLine("\nFile types:");
                _FileLocatorService.printFileList(fileTypes);

                // invoke request with files to find which files might include package dependencies

                // define target version

                var cts = new CancellationTokenSource();
                var spinnerTask = Task.Run(() => _SpinnerService.ShowSpinner(cts.Token, "Analyzing project files."));

                List<string> filesToCopy = _InvokeAzureAIRequestResponseService
                    .InvokeRequestResponse(
                        "Here are the files in a Visual Basic .Net Framework project. Which of these can be copied without any editing? Give the files as a list with each file on a new line. Respond with no other text or characters. For context - all images and binary files can be copied wihtout editing.",
                        files)
                    .Result
                    .Trim()
                    .Split("\n")
                    .ToList();

                cts.Cancel();
                spinnerTask.Wait();

                Console.WriteLine("\nFiles that can be copied without edditing:");
                _FileLocatorService.printFileList(filesToCopy);

                List<string> filesToMigrate = files.Except(filesToCopy).ToList();

                Console.WriteLine("\nFiles that need to be migrated:");
                _FileLocatorService.printFileList(filesToMigrate);

                // copy copyable files

                // migrate files to target version

                return true;
            }
            catch
            {
                throw new Exception("Migration Error");
            }
        }
    }
}
