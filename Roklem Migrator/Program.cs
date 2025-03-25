using Roklem_Migrator.Services;
using Microsoft.Extensions.DependencyInjection;


class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var filePathHandler = serviceProvider.GetService<FilePathHandlerService>();
        var fileReader = serviceProvider.GetService<FileReaderService>();

        RunApplication(args, filePathHandler, fileReader);
    }

    private static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<FileReaderService>();
        serviceCollection.AddSingleton<FilePathHandlerService>();
    }

    private static void RunApplication(string[] args, FilePathHandlerService filePathHandler, FileReaderService fileReader)
    {
        string filePath = filePathHandler.GetFilePath(args);

        try
        {
            if (filePathHandler.IsFilePathValid(filePath))
            {
                fileReader.ReadFile(filePath);

                foreach (var line in fileReader.ReadFile(filePath))
                {
                    Console.WriteLine(line);
                }
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
