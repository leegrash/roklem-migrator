namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IFileWriterService
    {
        void WriteToFile(string oldPath, string fileContent);
    }
}
