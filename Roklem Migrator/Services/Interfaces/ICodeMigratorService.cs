namespace Roklem_Migrator.Services.Interfaces
{
    internal interface ICodeMigratorService
    {
        public string Migrate(IEnumerable<string> codeLines);
    }
}
