using Microsoft.EntityFrameworkCore;

namespace CustomConfigProvider.Models.ConfigurationProviders;

public class EFConfigSource : IConfigurationSource
{
    private readonly Action<DbContextOptionsBuilder> _options;

    public EFConfigSource(Action<DbContextOptionsBuilder> options)
    {
        _options = options;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new EFConfigProvider(_options);
    }
}