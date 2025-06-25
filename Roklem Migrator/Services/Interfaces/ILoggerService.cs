using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services.Interfaces
{
    internal interface ILoggerService
    {
        void LogFixResult(string directory, Dictionary<string, List<string>> prevRoslynErrors, Dictionary<string, List<string>> newRoslynErrors);
    }
}
