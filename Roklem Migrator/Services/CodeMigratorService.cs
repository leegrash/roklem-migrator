using Roklem_Migrator.Services.Interfaces;
using System.Linq;

namespace Roklem_Migrator.Services
{
    internal class CodeMigratorService : ICodeMigratorService
    {
        private readonly IFileLocatorService _FileLocatorService;
        private readonly IFileHandlerService _FileHandlerService;

        public CodeMigratorService(IFileLocatorService fileLocatorService, IFileHandlerService fileHandlerService)
        {
            _FileLocatorService = fileLocatorService;
            _FileHandlerService = fileHandlerService;
        }

        public bool Migrate(string srcDir, string targetDir)
        {
            try
            {
                List<string> files = _FileLocatorService.locateFiles(srcDir);

                Console.WriteLine($"Located {files.Count} files");

                var (filesToMigrate, filesToCopy) = _FileHandlerService.distinguisFiles(files);

                // copy copyable files

                // invoke request with files to find which files might include package dependencies

                // define target version

                // migrate files to target version

                return true;
            }
            catch
            {
                throw new Exception("Migration Error");
            }
        }
    }
}
