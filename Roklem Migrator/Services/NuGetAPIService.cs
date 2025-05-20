using Roklem_Migrator.Services.Interfaces;
using System.IO.Compression;

namespace Roklem_Migrator.Services
{
    internal class NuGetAPIService : INuGetAPIService
    {
        public NuGetAPIService()
        {
            // Initialize the NuGet API client here if needed
        }

        public string getOptimalTargetVersion(List<string> packageDependencies)
        {
            foreach (var package in packageDependencies)
            {
                var latestVersion = getPackageVersionsAsync(package).Result;
            }
            return "1.0.0";
        }

        private async Task<List<string>> getPackageVersionsAsync(string packageName)
        {
            string packageInfo = await getPackageInfoAsync(packageName);

            return new List<string> { "1.0.0", "2.0.0", "3.0.0" };
        }

        private async Task<string> getPackageInfoAsync(string packageName)
        {
            using HttpClient client = new HttpClient();

            string url = $"https://api.nuget.org/v3/registration5-gz-semver2/{packageName.ToLower()}/index.json";
            
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using Stream responseStream = await response.Content.ReadAsStreamAsync();
                using GZipStream decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
                using StreamReader reader = new StreamReader(decompressedStream);
                string jsonResponse = await reader.ReadToEndAsync();
                return jsonResponse;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                throw new Exception($"Error fetching package info: {response.StatusCode}");
            }
        }
    }
}
