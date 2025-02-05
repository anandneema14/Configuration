using Microsoft.EntityFrameworkCore;

namespace CustomConfigProvider.Models;

public class ConfigurationDbContext : DbContext
{
    public ConfigurationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<ConfigurationEntity> ConfigurationEntities { get; set; }
}