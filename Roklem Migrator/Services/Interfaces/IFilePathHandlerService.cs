namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IFilePathHandlerService
    {
        (string srcDir, string targetDir) GetSrcAndTargetDirFromArg(string[] args);
        bool IsPathValid(string filePath);
        string GetNewFilePath(string oldFilePath);
    }
}
