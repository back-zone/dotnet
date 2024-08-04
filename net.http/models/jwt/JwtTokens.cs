using System.Text.Json.Serialization;

namespace back.zone.net.http.models.jwt;

public sealed class JwtTokensSchema
{
    public const string AccessToken = "access_token";
    public const string RefreshToken = "refresh_token";
}

public sealed record JwtTokens(
    [property: JsonPropertyName(JwtTokensSchema.AccessToken)]
    string AccessToken,
    [property: JsonPropertyName(JwtTokensSchema.RefreshToken)]
    string RefreshToken
);