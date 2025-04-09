using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class FileWriterService: IFileWriterService
    {
        private readonly IFilePathHandlerService _FilePathHandlerService;

        public FileWriterService(IFilePathHandlerService filePathHandler) {
            _FilePathHandlerService = filePathHandler;
        }

        public void WriteToFile(string oldPath,string fileContent)
        {
            string newPath = _FilePathHandlerService.GetNewFilePath(oldPath);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                File.WriteAllText(newPath, fileContent);
                Console.WriteLine("Successfully wrote to file");
            }
            catch {
                Console.WriteLine("Error writing to file");
            }
        }
    }
}
