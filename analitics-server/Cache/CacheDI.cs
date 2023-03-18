using Redis.OM;

namespace AnalyticsServer.Cache;

public static class CacheDi
{
    public static IServiceCollection AddCache(this IServiceCollection services, string redisConnectionString)
    {
        services.AddSingleton(new RedisConnectionProvider(redisConnectionString));
        services.AddHostedService<IndexCreationService>();
        return services;
    }
}