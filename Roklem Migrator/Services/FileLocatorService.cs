using Roklem_Migrator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services
{
    internal class FileLocatorService: IFileLocatorService
    {
        public List<string> locateFiles(string rootPath)
        {
            List<string> files = new List<string>();
            try
            {
                files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories).ToList();
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
