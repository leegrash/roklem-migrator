using Roklem_Migrator.Services.Interfaces;
using System.Collections;
using System.Text;

namespace Roklem_Migrator.Services
{
    internal class FileMigratorService : IFileMigratorService
    {
        private readonly IProgressBarService _ProgressBarService;
        private readonly IFileReaderService _FileReaderService;
        private readonly IFileWriterService _FileWriterService;
        private readonly IInvokeAzureAIRequestResponseService _InvokeAzureAIRequestResponseService;
        private readonly IFileHandlerService _FileHandlerService;

        public FileMigratorService(IProgressBarService progressBarService, IFileReaderService fileReaderService, IFileWriterService fileWriterService, IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, IFileHandlerService fileHandlerService)
        {
            _ProgressBarService = progressBarService;
            _FileReaderService = fileReaderService;
            _FileWriterService = fileWriterService;
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
            _FileHandlerService = fileHandlerService;
        }

        public void MigrateFiles(List<string> files, string srcDir, string targetDir, TargetVersionResponse targetVersionResponse)
        {
            List<string> migrationErrors = new List<string>();

            int currentStep = 0;
            Console.WriteLine("Migrating files");
            foreach (var file in files)
            {
                currentStep++;
                _ProgressBarService.DisplayProgress(currentStep, files.Count);

                try
                {
                    MigrateFile(file, srcDir, targetDir, targetVersionResponse);
                }
                catch (Exception e)
                {
                    migrationErrors.Add($"Error migrating file {file}: {e.Message}");
                }
            }
        }

        private void MigrateFile(string file, string srcDir, string targetDir, TargetVersionResponse targetVersionResponse)
        {
            string filePath = Path.Combine(srcDir, file);

            IEnumerable<string> fileContent = _FileReaderService.ReadFile(filePath);

            string migratinPrompt = GenerateMigrationPrompt(fileContent, targetVersionResponse.TargetVersion, targetVersionResponse.NonMigratablePackages, targetVersionResponse.ProposedDependencySolution, _FileHandlerService.getFileName(file));

            var response = _InvokeAzureAIRequestResponseService.InvokeRequestResponse(migratinPrompt, 0).GetAwaiter().GetResult();

            _FileWriterService.WriteToFile(filePath, response);
        }

        private string GenerateMigrationPrompt(IEnumerable<string> fileContent, string targetVersion, List<string> nonMigratablePackages, Dictionary<string, string> proposedDependencySolution, string fileName)
        {
            StringBuilder promptBuilder = new StringBuilder();
            promptBuilder.AppendLine($"You are a .net migration expert. Migrate the following code from the file {fileName} to the specified target version. Fix migration errors proposed below.");
            promptBuilder.AppendLine($"Target Version: {targetVersion}");
            promptBuilder.AppendLine("Non-migratable packages:");
            foreach (var package in nonMigratablePackages)
            {
                promptBuilder.AppendLine($"- {package}");
            }
            promptBuilder.AppendLine("Proposed dependency solution:");
            foreach (var kvp in proposedDependencySolution)
            {
                promptBuilder.AppendLine($"{kvp.Key}: {kvp.Value}");
            }
            promptBuilder.AppendLine("Code to migrate:");
            foreach (var line in fileContent)
            {
                promptBuilder.AppendLine(line);
            }

            promptBuilder.AppendLine("Return the migrated code and possible fixes that cannot be migrated. Comment what you have changed at the end. Do not include any markdown code block syntax (e.g., ,xml, ```json, etc.). The response should only contain executable code and comments in the given format, no other text or characters.");
            return promptBuilder.ToString();
        }
    }
}
