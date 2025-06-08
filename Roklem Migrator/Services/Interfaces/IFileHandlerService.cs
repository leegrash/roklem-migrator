namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IFileHandlerService
    {
        (List<string> filesToMigrate, List<string> filesToCopy) distinguisFiles(List<string> files);
        void copyFiles(List<string> filesToCopy, string srcPath, string targetPath);
        List<string> getFileNames(List<string> files);
        string getFileName(string filePath);
    }
}
