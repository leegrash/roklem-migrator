using Roklem_Migrator.Services.Interfaces;
using System.Diagnostics;

namespace Roklem_Migrator.Services
{
    internal class BuildProjectService : IBuildProjectService
    {
        private readonly ISpinnerService _SpinnerService;
        private readonly ICommonService _CommonService;


        public BuildProjectService(ISpinnerService spinnerService, ICommonService commonService) { 
            _SpinnerService = spinnerService;
            _CommonService = commonService;
        }
        
        public (bool sucess, List<string> errors) BuildProject(string slnFilePath)
        {
            _SpinnerService.StartSpinner("Building project...", "Build complete");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"build \"{slnFilePath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            List<string> errorList = new List<string>();

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    errorList.AddRange(ParseErrors(errors));
                }

                if (!string.IsNullOrWhiteSpace(output))
                {
                    errorList.AddRange(ParseErrors(output));
                }

                Console.WriteLine("\nBuild Output:");
                Console.WriteLine(output);
                Console.WriteLine("\nBuild Errors:");
                _CommonService.printList(errorList);
            }
            
            _SpinnerService.StopSpinner();

            return (errorList.Count == 0, errorList);
        }

        private List<string> ParseErrors(string errorOutput)
        {
            List<string> parsedErrors = new List<string>();
            var errorSummaryPattern = new System.Text.RegularExpressions.Regex(@"^\d+\s+Error\(s\)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            foreach (string line in errorOutput.Split(Environment.NewLine))
            {
                if (line.Contains("error", StringComparison.OrdinalIgnoreCase) &&
                    !errorSummaryPattern.IsMatch(line.Trim()))
                {
                    parsedErrors.Add(line.Trim());
                }
            }

            return parsedErrors;
        }
    }
}
