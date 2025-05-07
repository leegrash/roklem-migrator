using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class FilePathHandlerService : IFilePathHandlerService
    {
        public (string srcDir, string targetDir) GetSrcAndTargetDirFromArg(string[] args)
        {
            if (args.Length > 1)
            {
                return (args[0].Trim('"'), args[1].Trim('"'));
            }
            else
            {
                Console.WriteLine("No src or target path was given as an argument, provide path:");
                return (Console.ReadLine() ?? string.Empty, Console.ReadLine() ?? string.Empty);
            }
        }

        public bool IsPathValid(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        public string GetNewFilePath(string oldPath)
        {
            if (!IsPathValid(oldPath))
            {
                throw new Exception("Invalid file path");
            }

            string fileName = Path.GetFileName(oldPath);

            string? parentDirectory = Path.GetDirectoryName(oldPath);

            if (parentDirectory == null)
            {
                throw new Exception("Parent directory could not be determined");
            }

            string? grandParentDirectory = Directory.GetParent(parentDirectory)?.FullName;

            if (grandParentDirectory != null)
            {
                string newDirectoryPath = Path.Combine(grandParentDirectory, "roklem-output");

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
