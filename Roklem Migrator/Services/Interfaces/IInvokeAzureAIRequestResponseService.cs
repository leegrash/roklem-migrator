namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IInvokeAzureAIRequestResponseService
    {
        Task<string> InvokeRequestResponse(IEnumerable<string> codeLines);
    }
}
