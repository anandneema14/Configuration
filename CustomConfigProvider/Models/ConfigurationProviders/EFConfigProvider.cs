using Microsoft.EntityFrameworkCore;

namespace CustomConfigProvider.Models.ConfigurationProviders;

public class EFConfigProvider : ConfigurationProvider
{
    public EFConfigProvider(Action<DbContextOptionsBuilder> optionsAction)
    {
        OptionsAction = optionsAction;
    }

    public Action<DbContextOptionsBuilder> OptionsAction { get; set; }

    public override void Load()
    {
        var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        OptionsAction(builder);

        using (var dbContext= new ConfigurationDbContext(builder.Options))
        {
            dbContext.Database.EnsureCreated();
            Data = !dbContext.ConfigurationEntities.Any()
                ? CreateAndSaveDefaultValues(dbContext: dbContext)
                : dbContext.ConfigurationEntities.ToDictionary(c => c.Key, c => c.Value);
        }
    }

    private static IDictionary<string, string> CreateAndSaveDefaultValues(ConfigurationDbContext dbContext)
    {
        var configValues =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Pages:HomePage:WelcomeMessage", "Welcome to the ProjectConfigurationDemo Home Page" },
                { "Pages:HomePage:ShowWelcomeMessage", "true" },
                { "Pages:HomePage:Color", "black" },
                { "Pages:HomePage:UseRandomTitleColor", "true" }
            };

        dbContext.ConfigurationEntities.AddRange(configValues.Select(kv => new ConfigurationEntity()
        {
            Key = kv.Key,
            Value = kv.Value,
        }).ToArray());
        
        dbContext.SaveChanges();
        return configValues;
    }
}