using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using FileConfigProvider;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Configuration.Sources.Clear();

IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
    
    TransientFaultHandlingOptions options = new();
    builder.Configuration.GetSection(nameof(TransientFaultHandlingOptions))
        .Bind(options);
        
        Console.WriteLine($"TransientFaultHandlingOptions.Enabled: {options.Enabled}");
        Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay: {options.AutoRetryDelay}");
        
        using IHost host = builder.Build();
        await host.RunAsync();
        Console.ReadKey();