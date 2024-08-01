using System.Text.Json.Serialization;

namespace back.zone.net.http.models.jwt;

public sealed class JwtSessionSchema
{
    public const string SessionId = "session_id";
    public const string AccessToken = "access_token";
    public const string RefreshToken = "refresh_token";
}

public sealed record JwtSession(
    [property: JsonPropertyName(JwtSessionSchema.SessionId)]
    string SessionId,
    [property: JsonPropertyName(JwtSessionSchema.AccessToken)]
    string AccessToken,
    [property: JsonPropertyName(JwtSessionSchema.RefreshToken)]
    string RefreshToken
);