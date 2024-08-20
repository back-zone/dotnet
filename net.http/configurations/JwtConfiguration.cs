using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.core.Monads.OptionMonad;
using back.zone.core.Serde.Json;
using Microsoft.Extensions.Configuration;

namespace back.zone.net.http.configurations;

public static class JwtConfigurationSchema
{
    public const string SecretKey = "secret_key";
    public const string Audience = "audience";
    public const string Issuer = "issuer";
    public const string AccessTokenLifetime = "access_token_lifetime";
    public const string RefreshTokenLifetime = "refresh_token_lifetime";
    public const string Section = "jwt";
}

public record JwtConfiguration(
    [property: JsonPropertyName(JwtConfigurationSchema.SecretKey)]
    string SecretKey,
    [property: JsonPropertyName(JwtConfigurationSchema.Audience)]
    [property: JsonConverter(typeof(OptionJsonConverterFactory))]
    Option<string> Audience,
    [property: JsonPropertyName(JwtConfigurationSchema.Issuer)]
    [property: JsonConverter(typeof(OptionJsonConverterFactory))]
    Option<string> Issuer,
    [property: JsonPropertyName(JwtConfigurationSchema.AccessTokenLifetime)]
    long AccessTokenLifetime,
    [property: JsonPropertyName(JwtConfigurationSchema.RefreshTokenLifetime)]
    long RefreshTokenLifetime
)
{
    public static JwtConfiguration FromConfig(IConfigurationSection section)
    {
        return JsonSerializer.Deserialize<JwtConfiguration>(
            JsonSerializer.Serialize(section.GetAsRawJson(), BackzoneJsonSerializationOptions.Options)
        ) ?? throw new Exception("#jwt_configuration_failed_to_deserialize#");
    }
}