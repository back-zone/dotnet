using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace back.zone.net.http.Models.Jwt;

public static class SessionHolderSchema
{
    public const string SessionId = "session_id";
    public const string UserId = "user_id";
    public const string Roles = "roles";
}

public sealed record SessionHolder(
    [property: JsonPropertyName(SessionHolderSchema.SessionId)]
    string SessionId,
    [property: JsonPropertyName(SessionHolderSchema.UserId)]
    string UserId,
    [property: JsonPropertyName(SessionHolderSchema.Roles)]
    ImmutableArray<string> Roles
);