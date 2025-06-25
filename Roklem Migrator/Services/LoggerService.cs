using Roklem_Migrator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services
{
    internal class LoggerService : ILoggerService
    {
        public void LogFixResult(string directory, Dictionary<string, List<string>> prevRoslynErrors, Dictionary<string, List<string>> newRoslynErrors)
        {
            var fixedErrors = new Dictionary<string, List<string>>();
            var newErrors = new Dictionary<string, List<string>>();
            var unfixedErrors = new Dictionary<string, List<string>>();

            int totalPrevErrors = prevRoslynErrors.Values.Sum(list => list.Count);
            int totalNewErrors = newRoslynErrors.Values.Sum(list => list.Count);
            int fixedErrorsCount = 0;
            int newErrorsCount = 0;
            int unfixedErrorsCount = 0;

            var allFiles = new HashSet<string>(prevRoslynErrors.Keys.Concat(newRoslynErrors.Keys));

            foreach (var file in allFiles)
            {
                var prevList = prevRoslynErrors.ContainsKey(file) ? prevRoslynErrors[file] : new List<string>();
                var newList = newRoslynErrors.ContainsKey(file) ? newRoslynErrors[file] : new List<string>();

                var prevSet = new HashSet<string>(prevList);
                var newSet = new HashSet<string>(newList);

                var fixedInFile = prevSet.Except(newSet).ToList();
                var newInFile = newSet.Except(prevSet).ToList();
                var stillInFile = prevSet.Intersect(newSet).ToList();

                if (fixedInFile.Any())
                {
                    fixedErrors[file] = fixedInFile;
                    fixedErrorsCount += fixedInFile.Count;
                }

                if (newInFile.Any())
                {
                    newErrors[file] = newInFile;
                    newErrorsCount += newInFile.Count;
                }

                if (stillInFile.Any())
                {
                    unfixedErrors[file] = stillInFile;
                    unfixedErrorsCount += stillInFile.Count;
                }
                
            }

            Console.WriteLine("");
        }
    }
}
