using System.Collections.Immutable;

namespace back.zone.net.http.Models.Jwt;

public sealed record SessionHolder(
    string SessionId,
    string UserId,
    ImmutableArray<string> Roles
);