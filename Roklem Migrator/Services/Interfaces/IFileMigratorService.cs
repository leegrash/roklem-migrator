using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IFileMigratorService
    {
        public void MigrateFiles(List<string> files, string srcDir, string targetDir, TargetVersionResponse targetVersionResponse, string slnFilePath, int llmIterations);
    }
}
