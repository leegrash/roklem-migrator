using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class CodeMigratorService : ICodeMigratorService
    {
        private readonly IVBSyntaxTreeService _VBSyntaxTreeService;
        private readonly IInvokeAzureAIRequestResponseService _InvokeAzureAIRequestResponseService;

        public CodeMigratorService(IVBSyntaxTreeService vBSyntaxTreeService, IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService) { 
            _VBSyntaxTreeService = vBSyntaxTreeService;
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
        }

        public void Migrate(IEnumerable<string> codeLines)
        {
            var syntaxTree = _VBSyntaxTreeService.ParseSyntaxTree(codeLines);

            //_VBSyntaxTreeService.PrintSyntaxNodeStructure(syntaxTree.GetRoot());

            _InvokeAzureAIRequestResponseService.InvokeRequestResponse(codeLines).Wait();
        }
    }
}
