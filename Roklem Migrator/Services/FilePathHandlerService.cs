using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class FilePathHandlerService : IFilePathHandlerService
    {
        public string GetFilePath(string[] args)
        {
            if (args.Length > 0)
            {
                return args[0];
            }
            else
            {
                Console.WriteLine("No file path was given as an argument, provide file path:");
                return Console.ReadLine() ?? string.Empty;
            }
        }

        public bool IsFilePathValid(string filePath)
        {
            return !string.IsNullOrEmpty(filePath) && File.Exists(filePath);
        }

        public string GetNewFilePath(string oldPath)
        {
            if (!IsFilePathValid(oldPath))
            {
                throw new Exception("Invalid file path");
            }
            
            string fileName = Path.GetFileName(oldPath);

            string parantDirectory = Path.GetDirectoryName(oldPath);

            string grandParantDirectory = Directory.GetParent(parantDirectory)?.FullName;

            if (grandParantDirectory != null)
            {
                string newDirectoryPath = Path.Combine(grandParantDirectory, "roklem-output");
                
                string newPath = Path.Combine(newDirectoryPath, fileName);
                
                return newPath;
            }
            else
            {
                throw new Exception("Could not generate new Path");
            }

        }
    }
}
