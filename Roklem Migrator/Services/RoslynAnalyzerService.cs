using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class RoslynAnalyzerService : IRoslynAnalyzerService
    {
        public async Task<(bool, List<string>)> AnalyzeAsync(string projectPath)
        {
            var errors = new List<string>();

            using var workspace = MSBuildWorkspace.Create();

            var project = await workspace.OpenProjectAsync(projectPath);


            var compilation = await project.GetCompilationAsync();

            var diagnostics = compilation.GetDiagnostics()
                                         .Where(d => d.Severity == DiagnosticSeverity.Error || d.Severity == DiagnosticSeverity.Warning);

            foreach (var diagnostic in diagnostics)
            {
                Console.WriteLine(diagnostic.ToString());
            }

            return (errors.Count == 0, errors);
        }
    }
}
