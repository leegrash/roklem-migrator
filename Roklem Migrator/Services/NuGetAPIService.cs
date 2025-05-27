using Roklem_Migrator.Services.Interfaces;
using System.IO.Compression;
using System.Text.Json.Nodes;

namespace Roklem_Migrator.Services
{
    internal class NuGetAPIService : INuGetAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly IProgressBarService _progressBarService;

        public NuGetAPIService(IProgressBarService progressBarService)
        {
            _httpClient = new HttpClient();
            _progressBarService = progressBarService;
        }

        public async Task<Dictionary<string, List<string>>> GetSupportedVersionsAsync(List<string> packageDependencies)
        {
            var result = new Dictionary<string, List<string>>();

            int totalPackages = packageDependencies.Count;
            int currentStep = 0;
            Console.WriteLine($"Fetching supported versions for dependencies...");

            foreach (var package in packageDependencies)
            {
                currentStep++;
                _progressBarService.DisplayProgress(currentStep, totalPackages);

                try
                {
                    var supportedFrameworks = await GetSupportedFrameworksForPackageAsync(package);
                    result[package] = supportedFrameworks;
                }
                catch (Exception ex)
                {
                    result[package] = new List<string>();
                }
            }

            _progressBarService.stopProgressBar("Supported versions fetched successfully.");

            return result;
        }

        private async Task<List<string>> GetSupportedFrameworksForPackageAsync(string packageName)
        {
            string lowerName = packageName.ToLowerInvariant();
            string indexUrl = $"https://api.nuget.org/v3/registration5-gz-semver2/{lowerName}/index.json";

            var indexResponse = await _httpClient.GetAsync(indexUrl);
            indexResponse.EnsureSuccessStatusCode();

            using var stream = await indexResponse.Content.ReadAsStreamAsync();
            Stream contentStream = stream;

            if (indexResponse.Content.Headers.ContentEncoding.Contains("gzip"))
            {
                contentStream = new GZipStream(stream, CompressionMode.Decompress);
            }

            using var reader = new StreamReader(contentStream);
            string json = await reader.ReadToEndAsync();

            var frameworks = new HashSet<string>();
            var root = JsonNode.Parse(json);
            var items = root?["items"]?.AsArray();
            if (items != null)
            {
                foreach (var page in items)
                {
                    var pageItems = page?["items"]?.AsArray();
                    if (pageItems != null)
                    {
                        foreach (var pkg in pageItems)
                        {
                            var catalogEntry = pkg?["catalogEntry"];
                            var dependencyGroups = catalogEntry?["dependencyGroups"]?.AsArray();
                            if (dependencyGroups != null)
                            {
                                foreach (var group in dependencyGroups)
                                {
                                    var tfm = group?["targetFramework"]?.ToString();
                                    if (!string.IsNullOrEmpty(tfm))
                                        frameworks.Add(tfm);
                                }
                            }
                        }
                    }
                }
            }

            return frameworks.ToList();
        }
    }
}
