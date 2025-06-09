namespace Roklem_Migrator.Services.Interfaces
{
    internal interface ICodeMigratorService
    {
        public bool Migrate(string srcDir, string targetDir, int llmIterations);
    }
}
