using Roklem_Migrator.Services.Interfaces;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using Azure;

namespace Roklem_Migrator.Services
{
    internal class InvokeAzureAIRequestResponseService : IInvokeAzureAIRequestResponseService
    {
        public async Task<string> InvokeRequestResponse(IEnumerable<string> codeLines)
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
                    new SystemChatMessage("You are a developer tasked to migrate this set of Visual Basic .NetFramework Code to Visual Basic .Net Core. If it is possible to migrate, do it and return only the code - no other text or characters. If not, return Could not migrate code. The response should not include any markdown characters."),
                    new UserChatMessage(string.Join(Environment.NewLine, codeLines))
                };

            try
            {
                var completion = await chatClient.CompleteChatAsync(messages);

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
