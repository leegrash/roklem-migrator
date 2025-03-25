using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class CodeMigratorService : ICodeMigratorService
    {
        private readonly IVBSyntaxTreeService _VBSyntaxTreeService;
        
        public CodeMigratorService(IVBSyntaxTreeService vBSyntaxTreeService) { 
            _VBSyntaxTreeService = vBSyntaxTreeService;
        }

        public void Migrate(IEnumerable<string> codeLines)
        {
            var syntaxTree = _VBSyntaxTreeService.ParseSyntaxTree(codeLines);

            //_VBSyntaxTreeService.PrintSyntaxNodeStructure(syntaxTree.GetRoot());


        }
    }
}
