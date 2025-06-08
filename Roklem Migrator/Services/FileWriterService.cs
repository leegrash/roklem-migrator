using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class FileWriterService: IFileWriterService
    {
        private readonly IFilePathHandlerService _FilePathHandlerService;

        public FileWriterService(IFilePathHandlerService filePathHandler) {
            _FilePathHandlerService = filePathHandler;
        }

        public void WriteToFile(string newPath,string fileContent)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                File.WriteAllText(newPath, fileContent);
            }
            catch {
                throw new Exception($"Failed to write to file: {newPath}.");
            }
        }
    }
}
