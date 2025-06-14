namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IFileLocatorService
    {
        List<string> getFileTypes(List<string> files);
        public (List<string> files, string? slnFilePath) locateFiles(string srcDir);
    }
}
