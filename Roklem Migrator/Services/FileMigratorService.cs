using Roklem_Migrator.Services.Interfaces;
using System.Xml.Serialization;

namespace Roklem_Migrator.Services
{
    internal class FileMigratorService : IFileMigratorService
    {
        public void MigrateFiles(List<string> files, string srcDir, string targetDir, TargetVersionResponse targetVersionResponse)
        {
            foreach (var file in files)
            {
                try
                {
                    MigrateFile(file, srcDir, targetDir, targetVersionResponse);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error migrating file {file}: {e.Message}");
                }
            }
        }

        private void MigrateFile(string file, string srcDir, string targetDir, TargetVersionResponse targetVersionResponse)
        {
            Console.WriteLine($"Migrating file: {file}");
        }
    }
}
