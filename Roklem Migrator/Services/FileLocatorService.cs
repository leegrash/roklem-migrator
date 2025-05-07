using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class FileLocatorService: IFileLocatorService
    {
        public List<string> locateFiles(string srcDir)
        {
            List<string> files = new List<string>();
            try
            {
                var absolutePaths = Directory.GetFiles(srcDir, "*.*", SearchOption.AllDirectories);

                files = absolutePaths
                    .Select(file => Path.GetRelativePath(srcDir, file))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error locating files: {ex.Message}");
            }
            return files;
        }

        public void printFileList(List<string> files)
        {
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            Console.WriteLine();
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
