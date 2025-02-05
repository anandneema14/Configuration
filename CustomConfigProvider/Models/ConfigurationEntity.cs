using System.ComponentModel.DataAnnotations;

namespace CustomConfigProvider.Models;

public class ConfigurationEntity
{
    [Key]
    public string Key { get; set; }
    public string Value { get; set; }
}