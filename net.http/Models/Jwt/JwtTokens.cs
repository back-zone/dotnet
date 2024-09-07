namespace back.zone.net.http.Models.Jwt;

public sealed record JwtTokens(
    string AccessToken,
    string RefreshToken
);