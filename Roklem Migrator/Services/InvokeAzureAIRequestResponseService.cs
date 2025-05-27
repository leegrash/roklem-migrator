using Roklem_Migrator.Services.Interfaces;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using Azure;

namespace Roklem_Migrator.Services
{
    internal class InvokeAzureAIRequestResponseService : IInvokeAzureAIRequestResponseService
    {
        public async Task<string> InvokeRequestResponse(string prompt, float temperature, List<string>? data = null)
        {
            var endpoint = Environment.GetEnvironmentVariable("AzureEndpoint");
            if (string.IsNullOrEmpty(endpoint))
            {
                Console.WriteLine("Please set the AZURE_OPENAI_ENDPOINT environment variable.");
                throw new InvalidOperationException("Endpoint is not set.");
            }

            var key = Environment.GetEnvironmentVariable("AzureKey");
            if (string.IsNullOrEmpty(key))
            {
                Console.WriteLine("Please set the AZURE_OPENAI_KEY environment variable.");
                throw new InvalidOperationException("Key is not set.");
            }

            AzureKeyCredential credential = new AzureKeyCredential(key);

            var azureClient = new AzureOpenAIClient(new Uri(endpoint), credential);

            ChatClient chatClient = azureClient.GetChatClient("gpt-4o-mini");

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(prompt)
            };

            if (data != null && data.Count > 0)
            {
                messages.Add(new UserChatMessage(string.Join(Environment.NewLine, data)));
            }

            var chatOptions = new ChatCompletionOptions
            {
                Temperature = temperature
            };

            try
            {
                var completion = await chatClient.CompleteChatAsync(messages, chatOptions);

                if (completion != null)
                {
                    var chatResult = completion.Value;
                    return chatResult.Content[^1].Text;
                }
                else
                {
                    Console.WriteLine("No response received.");
                    throw new InvalidOperationException("No response received.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error during chat completion: {ex.Message}", ex);
            }
        }
    }
}
