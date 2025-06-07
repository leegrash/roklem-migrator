using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class CodeMigratorService : ICodeMigratorService
    {
        private readonly IFileLocatorService _FileLocatorService;
        private readonly IFileHandlerService _FileHandlerService;
        private readonly IDependencyHandlerService _DependencyHandlerService;
        private readonly INuGetAPIService _NuGetAPIService;
        private readonly ITargetVersionService _TargetVersionService;
        private readonly IFileMigratorService _FileMigratorService;

        public CodeMigratorService(IFileLocatorService fileLocatorService, IFileHandlerService fileHandlerService, IDependencyHandlerService dependencyHandlerService, INuGetAPIService nuGetAPIService, ITargetVersionService targetVersionService, IFileMigratorService fileMigratorService)
        {
            _FileLocatorService = fileLocatorService;
            _FileHandlerService = fileHandlerService;
            _DependencyHandlerService = dependencyHandlerService;
            _NuGetAPIService = nuGetAPIService;
            _TargetVersionService = targetVersionService;
            _FileMigratorService = fileMigratorService;
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

                Dictionary < string, List<string> > supportedVersions = _NuGetAPIService.GetSupportedVersionsAsync(packageDependencies).GetAwaiter().GetResult();

                TargetVersionResponse targetVersionResponse = _TargetVersionService.GetTargetVersion(supportedVersions).Result;

                _FileMigratorService.MigrateFiles(filesToMigrate, srcDir, targetDir, targetVersionResponse);

                return true;
            }
            catch
            {
                throw new Exception("Migration Error");
            }
        }
    }
}
