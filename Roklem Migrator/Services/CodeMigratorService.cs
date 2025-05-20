using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class CodeMigratorService : ICodeMigratorService
    {
        private readonly IFileLocatorService _FileLocatorService;
        private readonly IFileHandlerService _FileHandlerService;
        private readonly IDependencyHandlerService _DependencyHandlerService;
        private readonly INuGetAPIService _NuGetAPIService;

        public CodeMigratorService(IFileLocatorService fileLocatorService, IFileHandlerService fileHandlerService, IDependencyHandlerService dependencyHandlerService, INuGetAPIService nuGetAPIService)
        {
            _FileLocatorService = fileLocatorService;
            _FileHandlerService = fileHandlerService;
            _DependencyHandlerService = dependencyHandlerService;
            _NuGetAPIService = nuGetAPIService;
        }

        public bool Migrate(string srcDir, string targetDir)
        {
            try
            {
                List<string> files = _FileLocatorService.locateFiles(srcDir);

                Console.WriteLine($"Located {files.Count} files");

                var (filesToMigrate, filesToCopy) = _FileHandlerService.distinguisFiles(files);

                try
                {
                    _FileHandlerService.copyFiles(filesToCopy, srcDir, targetDir);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error copying files: {e.Message}");
                }

                List<string> packageDependencies = _DependencyHandlerService.getPackageDependencies(filesToMigrate, srcDir);

                string targetVersion = _NuGetAPIService.getOptimalTargetVersion(packageDependencies);

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
