using Roklem_Migrator.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Roklem_Migrator.Services
{
    public class TargetVersionResponse
    {
        public List<string> NonMigratablePackages { get; set; }
        public Dictionary<string, string> ProposedDependencySolution { get; set; }
        public string TargetVersion { get; set; }

        public TargetVersionResponse(List<string> nonMigratablePackages, Dictionary<string, string> proposedDependencySolution, string targetVersion)
        {
            NonMigratablePackages = nonMigratablePackages;
            ProposedDependencySolution = proposedDependencySolution;
            TargetVersion = targetVersion;
        }
    }

    internal class TargetVersionService : ITargetVersionService
    {
        private readonly IInvokeAzureAIRequestResponseService _InvokeAzureAIRequestResponseService;
        private readonly ISpinnerService _SpinnerService;

        public TargetVersionService(IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, ISpinnerService spinnerService)
        {
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
            _SpinnerService = spinnerService;
        }

        public async Task<TargetVersionResponse> GetTargetVersion(Dictionary<string, List<string>> supportedVersions)
        {
            string prompt = GenerateTargetVersionPrompt(supportedVersions);

            _SpinnerService.StartSpinner("Determining target .net version", "Target .net version determined");
            
            try
            {
                var  response = await _InvokeAzureAIRequestResponseService.InvokeRequestResponse(prompt, 0);

                var nonMigratablePackages = Regex.Match(response, @"#([\s\S]*?)#")
                                                 .Groups[1].Value
                                                 .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(s => s.Trim())
                                                 .ToList();

                var proposedDependencySolution = Regex.Match(response, @"%([\s\S]*?)%")
                                                      .Groups[1].Value
                                                      .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                                      .Where(s => s.Contains(":"))
                                                      .Select(s => s.Split(new[] { ':' }, 2))
                                                      .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());

                var targetVersion = Regex.Match(response, @"Version:\s*(\d+\.\d+)").Groups[1].Value;

                _SpinnerService.StopSpinner();

                return new TargetVersionResponse(nonMigratablePackages, proposedDependencySolution, targetVersion);
            }
            catch (Exception e)
            {
                _SpinnerService.StopSpinner();
                Console.WriteLine($"Error determining target .net version: {e.Message}");
                throw new Exception("Error determining target .net version", e);
            }
        }

        private string GenerateTargetVersionPrompt(Dictionary<string, List<string>> supportedVersions)
        {
            string prompt = "A Visual Basic .net Framework project has the following dependencies with the followig supported .net versions:";

            foreach(string package in supportedVersions.Keys)
            {
                var parts = package.Split(':');

                string packageName = parts[0].Trim();
                string packageVersion = parts[1].Trim();

                prompt += $"\n{packageName} {packageVersion} - ";

                foreach(string version in supportedVersions[package])
                {
                    prompt += $"{version}, ";
                }

                if (supportedVersions[package].Count == 0)
                {
                    prompt += "No data regarding .net version support is available";
                }
            }

            prompt += "\n\n What packages will not be able to migrate to .net core? Answer with the packages in a list, each package on a new line, starting and ending with a #";
            prompt += "\n\n How can the functionality remain whilst changing these unsupported packages? Answer with a list, each package on a new line with the format [Package name]:[Expalanation of package dependency fix]. Start and end the list with a %";
            prompt += "\n\n What is the optimal .net core version to upgrade to given the available packages? Answer with Version: xxx";

            return prompt;
        }
    }
}
