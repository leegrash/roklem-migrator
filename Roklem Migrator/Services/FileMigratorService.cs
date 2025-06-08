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
        private readonly IBuildProjectService _BuildProjectService;

        public FileMigratorService(IProgressBarService progressBarService, IFileReaderService fileReaderService, IFileWriterService fileWriterService, IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, IFileHandlerService fileHandlerService, IBuildProjectService buildProjectService)
        {
            _ProgressBarService = progressBarService;
            _FileReaderService = fileReaderService;
            _FileWriterService = fileWriterService;
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
            _FileHandlerService = fileHandlerService;
            _BuildProjectService = buildProjectService;
        }

        public void MigrateFiles(List<string> files, string srcDir, string targetDir, TargetVersionResponse targetVersionResponse, string slnFilePath)
        {
            //List<string> migrationLog = new List<string>();

            //int currentStep = 0;
            //Console.WriteLine("Migrating files");
            //foreach (var file in files)
            //{
            //    currentStep++;
            //    _ProgressBarService.DisplayProgress(currentStep, files.Count);

            //    try
            //    {
            //        MigrateFile(file, srcDir, targetDir, targetVersionResponse);
            //        migrationLog.Add($"Migrated file: {file}");
            //    }
            //    catch (Exception e)
            //    {
            //        migrationLog.Add($"Error migrating file {file}: {e.Message}");
            //    }
            //}

            //Console.WriteLine("\n\n1st stage of migration completed. Migration log:");
            //foreach (var log in migrationLog)
            //{
            //    Console.WriteLine(log);
            //}

            (bool buildSuccess, List<string> buildErrors) = _BuildProjectService.BuildProject(slnFilePath);
        }

        private void MigrateFile(string file, string srcDir, string targetDir, TargetVersionResponse targetVersionResponse)
        {
            string filePath = Path.Combine(srcDir, file);

            IEnumerable<string> fileContent = _FileReaderService.ReadFile(filePath);

            string migratinPrompt = GenerateMigrationPrompt(fileContent, targetVersionResponse.TargetVersion, targetVersionResponse.NonMigratablePackages, targetVersionResponse.ProposedDependencySolution, _FileHandlerService.getFileName(file));

            var response = _InvokeAzureAIRequestResponseService.InvokeRequestResponse(migratinPrompt, 1).GetAwaiter().GetResult();

            _FileWriterService.WriteToFile(Path.Combine(targetDir, file), response);
        }

        private string GenerateMigrationPrompt(IEnumerable<string> fileContent, string targetVersion, List<string> nonMigratablePackages, Dictionary<string, string> proposedDependencySolution, string fileName)
        {
            StringBuilder promptBuilder = new StringBuilder();
            promptBuilder.AppendLine($"You are a .net migration expert. Migrate the following vb .net framework code from the file {fileName} to the specified visual basic .net core target version. Fix migration errors proposed below.");
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
