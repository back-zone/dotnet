using back.zone.core.Monads.OptionMonad;
using back.zone.storage.redis.Configuration;
using back.zone.storage.redis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back.zone.storage.redis.Plug;

public static class RedisServicePlugger
{
    public static void Plug(
        IConfigurationManager config,
        IServiceCollection services
    )
    {
        var endPoints = config
                            .GetSection("redis:end_points")
                            .AsEnumerable()
                            .Where(m => m.Value is not null)
                            .Select(m => m.Value!)
                            .ToArray()
                        ?? throw new InvalidOperationException("Redis end points not found.");

        var commandName = config["redis:command_name"] ?? Option.None<string>();

        var connectRetry = int.TryParse(config["redis:connect_retry"], out var retry)
            ? retry
            : Option.None<int>();

        var allowAdmin = bool.TryParse(config["redis:allow_admin"], out var admin)
            ? admin
            : Option.None<bool>();

        var user = config["redis:username"] ?? Option.None<string>();

        var password = config["redis:password"] ?? Option.None<string>();

        var abortOnConnectFail = bool.TryParse(config["redis:abort_on_connect_fail"], out var fail)
            ? fail
            : Option.None<bool>();

        var redisConfiguration = new RedisConfiguration(
            endPoints,
            commandName,
            connectRetry,
            allowAdmin,
            user,
            password,
            abortOnConnectFail
        );

        if (redisConfiguration is null) throw new InvalidDataException("Invalid Redis configuration.");

        var redisService = new RedisService(redisConfiguration);

        services.AddSingleton(redisService);
    }
}