using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace back.zone.net.http.models.jwt;

public static class CredentialsSchema
{
    public const string Username = "username";
    public const string Hash = "hash";
    public const string Salt = "salt";
}

public sealed record Credentials(
    [property: JsonPropertyName(CredentialsSchema.Username)]
    [property: BsonElement(CredentialsSchema.Username)]
    string Username,
    [property: JsonPropertyName(CredentialsSchema.Hash)]
    [property: BsonElement(CredentialsSchema.Hash)]
    byte[] Hash,
    [property: JsonPropertyName(CredentialsSchema.Salt)]
    [property: BsonElement(CredentialsSchema.Salt)]
    byte[] Salt
);