using Roklem_Migrator.Services;

class Program
{
    static void Main(string[] args)
    {
        string filePath;

        if (args.Length > 0)
        {
            filePath = args[0];
        }
        else
        {
            Console.WriteLine("No file path was given as argument, provide file path:");
            filePath = Console.ReadLine();
        }

        var fileReader = new FileReaderService();

        try
        {
            fileReader.ReadFile(filePath);
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
