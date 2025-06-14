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
        public async Task<(bool, List<string>)> AnalyzeAsync(string slnFilePath)
        {
            var errors = new List<string>();

            using var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(slnFilePath);

            foreach (var project in solution.Projects.Where(p => p.Language == LanguageNames.VisualBasic))
            {
                var compilation = await project.GetCompilationAsync();

                var diagnostics = compilation.GetDiagnostics()
                                             .Where(d => d.Severity == DiagnosticSeverity.Error || d.Severity == DiagnosticSeverity.Warning);

                foreach (var diagnostic in diagnostics)
                {
                    errors.Add(diagnostic.ToString());
                }
            }

            return (errors.Count == 0, errors);
        }
    }
}
