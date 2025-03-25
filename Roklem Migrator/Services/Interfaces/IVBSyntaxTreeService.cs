using Microsoft.CodeAnalysis;

namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IVBSyntaxTreeService
    {
        SyntaxTree ParseSyntaxTree(IEnumerable<string> codeLines);

        void PrintSyntaxNodeStructure(SyntaxNode node, string indent = "");
    }
}
