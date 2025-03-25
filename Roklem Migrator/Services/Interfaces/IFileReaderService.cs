namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IFileReaderService
    {
        IEnumerable<string> ReadFile(string filePath);
    }
}
