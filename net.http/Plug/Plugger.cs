using back.zone.core.Monads.OptionMonad;
using back.zone.net.http.Configuration;
using back.zone.net.http.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back.zone.net.http.Plug;

/// <summary>
///     This class provides a method to plug JWT (JSON Web Token) service into the application's service collection.
/// </summary>
public static class JwtServicePlugger
{
    /// <summary>
    ///     Plugs JWT service into the application's service collection.
    /// </summary>
    /// <param name="config">An instance of <see cref="IConfigurationManager" /> to access application configuration.</param>
    /// <param name="services">An instance of <see cref="IServiceCollection" /> to register services.</param>
    public static void Plug(
        IConfigurationManager config,
        IServiceCollection services
    )
    {
        var secretKey = config["jwt:secret_key"] ??
                        throw new InvalidOperationException("Missing 'jwt:secret_key' configuration key.");
        var audience = config["jwt:audience"] ?? Option.None<string>();
        var issuer = config["jwt:issuer"] ?? Option.None<string>();
        var accessTokenLifetime =
            long.TryParse(config["jwt:access_token_lifetime"], out var accessTokenLt)
                ? accessTokenLt
                : 3600;
        var refreshTokenLifetime =
            long.TryParse(config["jwt:refresh_token_lifetime"], out var refreshTokenLt)
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