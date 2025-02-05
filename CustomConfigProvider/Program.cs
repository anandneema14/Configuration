using CustomConfigProvider.Models;
using CustomConfigProvider.Models.ConfigurationProviders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Host.ConfigureAppConfiguration((context, configurationBuilder) =>
{
    var config = configurationBuilder.Build();
    var configSource = new EFConfigSource(opts => opts.UseSqlServer(config.GetConnectionString("sqlConnection")));
    configurationBuilder.Add(configSource);
});

app.MapGet("/", () => "Hello World!");

app.Run();