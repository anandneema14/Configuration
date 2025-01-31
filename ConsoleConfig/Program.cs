using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace ConsoleConfig;

class Program
{    
    static void Main(string[] args)
    {
        var mappings = new Dictionary<string, string>()
        {
            {"--greeting","Greeting:Message" },
            {"--color","Greeting:Color" },
            { "--env","Environment" }
        };

        var Name = "Anand";
        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("config.json") //This is adding config file dynamically
            .AddEnvironmentVariables()  //This we mainly use in docker files or during containerization
            .AddUserSecrets(typeof(Program).Assembly)   //The value is stored in JSON file in the local machine's user profile folder
            .AddCommandLine(args, mappings)
            .Build();
        AnsiConsole.Markup($"[{configurationRoot["Greeting:Color"]}]{configurationRoot["Greeting:Message"]}[/],{Name}!");
        Console.WriteLine($"Configuration: {configurationRoot["Environment"]}!");
        Console.ReadKey();
    }
}
