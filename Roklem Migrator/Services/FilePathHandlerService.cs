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
    }
}
