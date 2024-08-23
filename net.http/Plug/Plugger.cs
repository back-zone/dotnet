using back.zone.core.Monads.OptionMonad;
using back.zone.net.http.Configuration;
using back.zone.net.http.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back.zone.net.http.Plug;

public static class JwtServicePlugger
{
    public static void Plug(
        IConfigurationManager config,
        IServiceCollection services
    )
    {
        var secretKey = config["secret_key"] ??
                        throw new InvalidOperationException("Missing 'jwt:secret_key' configuration key.");
        var audience = config["audience"] ?? Option.None<string>();
        var issuer = config["issuer"] ?? Option.None<string>();
        var accessTokenLifetime =
            long.TryParse(config["access_token_lifetime"], out var accessTokenLt)
                ? accessTokenLt
                : 3600;
        var refreshTokenLifetime =
            long.TryParse(config["refresh_token_lifetime"], out var refreshTokenLt)
                ? refreshTokenLt
                : 7776000000;

        var jwtConfiguration =
            new JwtConfiguration(
                secretKey,
                audience,
                issuer,
                accessTokenLifetime,
                refreshTokenLifetime
            );

        var jwtService = new JwtService(jwtConfiguration);

        services.AddSingleton(jwtService);
    }
}