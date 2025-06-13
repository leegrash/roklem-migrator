using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class RoslynAnalyzerService : IRoslynAnalyzerService
    {
        public async Task<(bool, List<string>)> AnalyzeAsync(string slnPath)
        {
            var results = new List<string>();

            

            return (true, results);
        }
    }
}
