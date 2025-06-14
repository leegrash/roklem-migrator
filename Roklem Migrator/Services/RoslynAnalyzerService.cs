using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class RoslynAnalyzerService : IRoslynAnalyzerService
    {
        private readonly ISpinnerService _spinnerService;
        public RoslynAnalyzerService(ISpinnerService spinnerService)
        {
            _spinnerService = spinnerService;
        }

        public async Task<Dictionary<string, List<string>>> AnalyzeAsync(string slnFilePath)
        {
            _spinnerService.StartSpinner("Analyzing migrated project using Roslyn...", "Roslyn Analysis completed.");

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

            var categorizedErrors = CategorizeErrorsByFile(errors);

            _spinnerService.StopSpinner();

            return categorizedErrors;
        }

        private Dictionary<string, List<string>> CategorizeErrorsByFile(List<string> errors)
        {
            var categorizedErrors = new Dictionary<string, List<string>>();

            foreach (var error in errors)
            {
                int parenIndex = error.IndexOf('(');
                if (parenIndex > 0)
                {
                    string filePath = error.Substring(0, parenIndex).Trim();

                    string errorWithoutFilePath = error.Substring(parenIndex);

                    if (!categorizedErrors.ContainsKey(filePath))
                    {
                        categorizedErrors[filePath] = new List<string>();
                    }
                    categorizedErrors[filePath].Add(errorWithoutFilePath.Trim());
                }
                else
                {
                    const string unknownKey = "<unknown file>";
                    if (!categorizedErrors.ContainsKey(unknownKey))
                    {
                        categorizedErrors[unknownKey] = new List<string>();
                    }
                    categorizedErrors[unknownKey].Add(error.Trim());
                }
            }

            return categorizedErrors;
        }
    }
}
