namespace back.zone.net.http.Models.Jwt;

public sealed record Credentials(
    string Username,
    byte[] Hash,
    byte[] Salt
);