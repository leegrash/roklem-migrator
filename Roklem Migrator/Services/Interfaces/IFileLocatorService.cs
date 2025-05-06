namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IFileLocatorService
    {
        List<string> getFileTypes(List<string> files);
        List<string> locateFiles(string rootPath);
        void printFileList(List<string> files);
    }
}
