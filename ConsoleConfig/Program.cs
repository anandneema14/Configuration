using Microsoft.Extensions.Configuration;

namespace ConsoleConfig;

class Program
{
    static void Main(string[] args)
    {
        var Name = "Anand";
        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("config.json") //This is adding config file dynamically
            .AddEnvironmentVariables()  //This we mainly use in docker files or during containerization
            .AddCommandLine(args)
            .AddUserSecrets(typeof(Program).Assembly)   //The value is stored in JSON file in the local machine's user profile folder
            .Build();
        Console.WriteLine($"{configurationRoot["Greeting"]},{Name}!");
        Console.WriteLine($"Configuration: {configurationRoot["Environment"]}!");
        Console.ReadKey();
    }
}
