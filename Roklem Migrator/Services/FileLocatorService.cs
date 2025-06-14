using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class FileLocatorService: IFileLocatorService
    {
        public (List<string> files, string? slnFilePath, List<string> vbprojPaths) locateFiles(string srcDir)
        {
            List<string> files = new List<string>();
            string? slnFilePath = null;
            List<string> vbprojPaths = new List<string>();

            try
            {
                var absolutePaths = Directory.GetFiles(srcDir, "*.*", SearchOption.AllDirectories);

                files = absolutePaths
                    .Select(file => Path.GetRelativePath(srcDir, file))
                    .ToList();

                var slnAbsolutePath = absolutePaths.FirstOrDefault(f => Path.GetExtension(f).Equals(".sln", StringComparison.OrdinalIgnoreCase));
                if (slnAbsolutePath != null)
                {
                    slnFilePath = Path.GetRelativePath(srcDir, slnAbsolutePath);
                }

                vbprojPaths = absolutePaths
                    .Where(f => Path.GetExtension(f).Equals(".vbproj", StringComparison.OrdinalIgnoreCase))
                    .Select(f => Path.GetRelativePath(srcDir, f))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error locating files: {ex.Message}");
            }
            return (files, slnFilePath, vbprojPaths);
        }

        public List<string> getFileTypes(List<string> files)
        {
            List<string> fileTypes = new List<string>();
            foreach (var file in files)
            {
                string fileType = Path.GetExtension(file);
                if (!fileTypes.Contains(fileType))
                {
                    fileTypes.Add(fileType);
                }
            }
            return fileTypes;
        }
    }
}
