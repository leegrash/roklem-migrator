using Roklem_Migrator.Services;
using Microsoft.Extensions.DependencyInjection;
using Roklem_Migrator.Services.Interfaces;


class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var filePathHandler = serviceProvider.GetRequiredService<FilePathHandlerService>();
        var fileReader = serviceProvider.GetRequiredService<FileReaderService>();
        var codeMigrator = serviceProvider.GetRequiredService<ICodeMigratorService>();

        RunApplication(args, filePathHandler, fileReader, codeMigrator);
    }

    private static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<FileReaderService>();
        serviceCollection.AddSingleton<FilePathHandlerService>();
        serviceCollection.AddSingleton<IVBSyntaxTreeService, VBSyntaxTreeService>();
        serviceCollection.AddSingleton<ICodeMigratorService, CodeMigratorService>();

    }

    private static void RunApplication(string[] args, FilePathHandlerService filePathHandler, FileReaderService fileReader, ICodeMigratorService codeMigrator)
    {
        string filePath = filePathHandler.GetFilePath(args);

        try
        {
            if (filePathHandler.IsFilePathValid(filePath))
            {
                codeMigrator.Migrate(fileReader.ReadFile(filePath));
            }
        }
        catch (FileNotFoundException)
        {
            Console.Error.WriteLine($"File not found at path: {filePath}");
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"An error occurred: {e.Message}");
        }
    }
}
