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
    }
}
