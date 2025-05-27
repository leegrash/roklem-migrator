namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IInvokeAzureAIRequestResponseService
    {
        Task<string> InvokeRequestResponse(string prompt, List<string> data, float temperature);
    }
}
