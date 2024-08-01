using System.Text.Json.Serialization;

namespace back.zone.net.http.models.jwt;

public static class CredentialsSchema
{
    public const string Username = "username";
    public const string Hash = "hash";
    public const string Salt = "salt";
}

public sealed record Credentials(
    [property: JsonPropertyName(CredentialsSchema.Username)]
    string Username,
    [property: JsonPropertyName(CredentialsSchema.Hash)]
    byte[] Hash,
    [property: JsonPropertyName(CredentialsSchema.Salt)]
    byte[] Salt
);