using Roklem_Migrator.Services.Interfaces;
using System.Net.Http.Headers;
using System;
using System.IO;

namespace Roklem_Migrator.Services
{
    internal class InvokeAzureAIRequestResponseService : IInvokeAzureAIRequestResponseService
    {
        public async Task InvokeRequestResponse(IEnumerable<string> codeLines)
        {
            var vbCode = string.Join("\n", codeLines);

            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            using (var client = new HttpClient(handler))
            {
                var requestBody = $@"{{
                      ""messages"": [
                        {{
                          ""role"": ""user"",
                          ""content"": ""{vbCode}""
                        }}
                      ],
                      ""max_tokens"": 4096,
                      ""temperature"": 1,
                      ""top_p"": 1,
                      ""stop"": []
                    }}";

                string apiKey = Environment.GetEnvironmentVariable("AzureAIKey");

                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("A key should be provided to invoke the endpoint");
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("AzureAIUri"));

                var content = new StringContent(requestBody);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }

        }
    }
}
