using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IRoslynAnalyzerService
    {
        Task<(bool success, List<string> errors)> AnalyzeAsync(string slnPath);
    }
}
