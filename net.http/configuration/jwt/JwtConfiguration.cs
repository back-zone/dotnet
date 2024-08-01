using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace back.zone.net.http.configuration.jwt;

public static class JwtConfigurationSchema
{
    public const string SecretKey = "secret_key";
    public const string Audience = "audience";
    public const string Issuer = "issuer";
    public const string AccessTokenLifetime = "access_token_lifetime";
    public const string RefreshTokenLifetime = "refresh_token_lifetime";
}

public record JwtConfiguration(
    [property: JsonPropertyName(JwtConfigurationSchema.SecretKey)]
    [property: ConfigurationKeyName(JwtConfigurationSchema.SecretKey)]
    string SecretKey,
    [property: JsonPropertyName(JwtConfigurationSchema.Audience)]
    [property: ConfigurationKeyName(JwtConfigurationSchema.Audience)]
    string Audience,
    [property: JsonPropertyName(JwtConfigurationSchema.Issuer)]
    [property: ConfigurationKeyName(JwtConfigurationSchema.Issuer)]
    string Issuer,
    [property: JsonPropertyName(JwtConfigurationSchema.AccessTokenLifetime)]
    [property: ConfigurationKeyName(JwtConfigurationSchema.AccessTokenLifetime)]
    long AccessTokenLifetime,
    [property: JsonPropertyName(JwtConfigurationSchema.RefreshTokenLifetime)]
    [property: ConfigurationKeyName(JwtConfigurationSchema.RefreshTokenLifetime)]
    long RefreshTokenLifetime
);