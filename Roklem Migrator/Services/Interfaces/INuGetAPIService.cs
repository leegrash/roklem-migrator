using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services.Interfaces
{
    internal interface INuGetAPIService
    {
        Task<Dictionary<string, List<string>>> GetSupportedVersionsAsync(List<string> packageDependencies);
    }
}
