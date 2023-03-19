#pragma warning disable CS8618
namespace AnalyticsServer.Models;

public class ConfigurationService
{
    public string RedisConnectionString { get; init; }
    public RabbitMqConfiguration RabbitMqConfiguration { get; init; }
}

public class RabbitMqConfiguration
{
    public string Host { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
}