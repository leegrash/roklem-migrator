using Microsoft.Extensions.DependencyInjection;
using Roklem_Migrator.Services.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        RunApplication(args, serviceProvider);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        var serviceTypes = typeof(Program).Assembly.GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.Name.StartsWith("I")))
        .ToList();

        foreach (var type in serviceTypes)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault();
            if (interfaceType != null)
            {
                services.AddSingleton(interfaceType, type);
            }
        }
    }

    private static void RunApplication(string[] args, IServiceProvider serviceProvider)
    {
        var filePathHandler = serviceProvider.GetRequiredService<IFilePathHandlerService>();
        var fileReader = serviceProvider.GetRequiredService<IFileReaderService>();
        var codeMigrator = serviceProvider.GetRequiredService<ICodeMigratorService>();
        var fileWriter = serviceProvider.GetRequiredService<IFileWriterService>();

        string filePath = filePathHandler.GetFilePath(args);

        try
        {
            if (filePathHandler.IsFilePathValid(filePath))
            {
                string migratedCode = codeMigrator.Migrate(fileReader.ReadFile(filePath));

                fileWriter.WriteToFile(filePath, migratedCode);
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
