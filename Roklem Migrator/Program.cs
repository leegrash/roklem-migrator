using Roklem_Migrator.Services;
using Microsoft.Extensions.DependencyInjection;


class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var fileReader = serviceProvider.GetService<FileReaderService>();

        if (fileReader != null)
        {
            RunApplication(args, fileReader);
        }
        else
        {
            Console.Error.WriteLine("FileReaderService could not be resolved.");
        }
    }

    private static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<FileReaderService>();
    }

    private static void RunApplication(string[] args, FileReaderService fileReader)
    {
        string filePath;

        if (args.Length > 0)
        {
            filePath = args[0];
        }
        else
        {
            Console.WriteLine("No file path was given as argument, provide file path:");
            filePath = Console.ReadLine() ?? string.Empty;
        }

        try
        {
            if (filePath.Length > 0)
            {
                fileReader.ReadFile(filePath);
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
