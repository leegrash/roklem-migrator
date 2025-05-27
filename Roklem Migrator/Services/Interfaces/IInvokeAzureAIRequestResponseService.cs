namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IInvokeAzureAIRequestResponseService
    {
        public Task<string> InvokeRequestResponse(string prompt, float temperature, List<string>? data = null);
    }
}
