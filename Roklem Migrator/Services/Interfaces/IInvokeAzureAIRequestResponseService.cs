namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IInvokeAzureAIRequestResponseService
    {
        Task InvokeRequestResponse(IEnumerable<string> codeLines);
    }
}
