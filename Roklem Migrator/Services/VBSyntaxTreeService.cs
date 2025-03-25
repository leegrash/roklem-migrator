using Roklem_Migrator.Services.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;

namespace Roklem_Migrator.Services
{
    public class VBSyntaxTreeService : IVBSyntaxTreeService
    {
        public SyntaxTree ParseSyntaxTree(IEnumerable<string> codeLines)
        {
            var vbCode = string.Join("\n", codeLines);
            SyntaxTree vbTree = VisualBasicSyntaxTree.ParseText(vbCode);

            return vbTree;
        }

        public void PrintSyntaxNodeStructure(SyntaxNode node, string indent = "")
        {
            Console.WriteLine($"{indent}{node.Kind()}");
            foreach (var childNode in node.ChildNodes())
            {
                PrintSyntaxNodeStructure(childNode, indent + "  ");
            }
        }
    }
}
