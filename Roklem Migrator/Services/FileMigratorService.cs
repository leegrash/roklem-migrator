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
        private readonly IRoslynAnalyzerService _RoslynAnalyzerService;

        public FileMigratorService(IProgressBarService progressBarService, IFileReaderService fileReaderService, IFileWriterService fileWriterService, IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, IFileHandlerService fileHandlerService, IBuildProjectService buildProjectService, IRoslynAnalyzerService roslynAnalyzerService)
        {
            _ProgressBarService = progressBarService;
            _FileReaderService = fileReaderService;
            _FileWriterService = fileWriterService;
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
            _FileHandlerService = fileHandlerService;
            _BuildProjectService = buildProjectService;
            _RoslynAnalyzerService = roslynAnalyzerService;
        }

        public void MigrateFiles(List<string> files, string srcDir, string targetDir, TargetVersionResponse targetVersionResponse, string slnFilePath, int llmIterations)
        {
            List<string> migrationLog = new List<string>();

            int currentStep = 0;
            Console.WriteLine("Migrating files");
            foreach (var file in files)
            {
                currentStep++;
                _ProgressBarService.DisplayProgress(currentStep, files.Count);

                try
                {
                    MigrateFile(file, srcDir, targetDir, targetVersionResponse);
                    migrationLog.Add($"Migrated file: {file}");
                }
                catch (Exception e)
                {
                    migrationLog.Add($"Error migrating file {file}: {e.Message}");
                }
            }

            Console.WriteLine("\n\n1st stage of migration completed. Migration log:");
            foreach (var log in migrationLog)
            {
                Console.WriteLine(log);
            }

            (bool buildSuccess, List<string> buildErrors) = _BuildProjectService.BuildProject(slnFilePath);

            List<string> roslynAnalyzerErrors = _RoslynAnalyzerService.AnalyzeAsync(slnFilePath).GetAwaiter().GetResult();
            bool noRoslynErrors = roslynAnalyzerErrors.Count == 0;

            while (!buildSuccess && llmIterations > 0)
            {
                currentStep = 0;
                Console.WriteLine("Attempting to fix errors");

                foreach (var file in files)
                {
                    currentStep++;
                    _ProgressBarService.DisplayProgress(currentStep, files.Count);

                    try
                    {
                        FixBuildErrors(Path.Combine(targetDir, file), targetVersionResponse.TargetVersion, _FileHandlerService.getFileName(file), buildErrors);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error migrating file {file}: {e.Message}");
                    }
                }
                _ProgressBarService.stopProgressBar("Attempt to fix build errors completed.");
                (buildSuccess, buildErrors) = _BuildProjectService.BuildProject(slnFilePath);
                llmIterations--;
            }

            if (buildSuccess)
            {
                Console.WriteLine("Build successful after migration and fixes.");
            }
            else
            {
                Console.WriteLine($"Build failed after migration and {llmIterations} build error fix iterations. Remaining errors:");
                foreach (var error in buildErrors)
                {
                    Console.WriteLine(error);
                }
            }
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

        private void FixBuildErrors(string filePath, string targetVersion, string fileName, List<string> buildErrors)
        {
            IEnumerable<string> fileContent = _FileReaderService.ReadFile(filePath);
            string errorFixPrompt = GenerateErrorFixPrompt(fileContent, targetVersion, fileName, buildErrors);
            var response = _InvokeAzureAIRequestResponseService.InvokeRequestResponse(errorFixPrompt, 1).GetAwaiter().GetResult();
            _FileWriterService.WriteToFile(filePath, response);
        }

        private string GenerateErrorFixPrompt(IEnumerable<string> fileContent, string targetVersion, string fileName, List<string> buildErrors)
        {
            StringBuilder promptBuilder = new StringBuilder();
            promptBuilder.AppendLine($"You are a .net migration expert. The following file named {fileName} is part of a visual basic project that has been migrated from .net framework to .net core with the following target version: {targetVersion}");
            promptBuilder.AppendLine($"The following build errors occur in the project");

            foreach (var error in buildErrors)
            {
                promptBuilder.AppendLine($"- {error}");
            }

            promptBuilder.AppendLine("The code in the file is as follows:");

            foreach (var line in fileContent)
            {
                promptBuilder.AppendLine(line);
            }

            promptBuilder.AppendLine("Return the code with the fixes applied. Comment what you have changed at the end. Do not include any markdown code block syntax (e.g., ,xml, ```json, etc.). The response should only contain executable code and comments in the given format, no other text or characters.");

            return promptBuilder.ToString();
        }
    }
}
