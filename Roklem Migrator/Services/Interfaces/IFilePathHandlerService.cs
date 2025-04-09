namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IFilePathHandlerService
    {
        string GetFilePath(string[] args);
        bool IsFilePathValid(string filePath);
        string GetNewFilePath(string oldFilePath);
    }
}
