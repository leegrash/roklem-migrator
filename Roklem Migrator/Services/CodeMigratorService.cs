using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class CodeMigratorService : ICodeMigratorService
    {
        private readonly IVBSyntaxTreeService _VBSyntaxTreeService;
        private readonly IInvokeAzureAIRequestResponseService _InvokeAzureAIRequestResponseService;

        public CodeMigratorService(IVBSyntaxTreeService vBSyntaxTreeService, IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, IFileWriterService fileWriterService) { 
            _VBSyntaxTreeService = vBSyntaxTreeService;
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
        }

        public string Migrate(IEnumerable<string> codeLines)
        {
            var syntaxTree = _VBSyntaxTreeService.ParseSyntaxTree(codeLines);

            //_VBSyntaxTreeService.PrintSyntaxNodeStructure(syntaxTree.GetRoot());

            try
            {
                var response = _InvokeAzureAIRequestResponseService.InvokeRequestResponse(codeLines).Result;
                return response;
            }
            catch
            {
                throw new Exception("Migration Error");
            }
        }
    }
}
