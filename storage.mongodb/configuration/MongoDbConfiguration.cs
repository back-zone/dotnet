using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace back.zone.storage.mongodb.configuration;

public static class MongoDbConfigurationSchema
{
    public const string Uri = "uri";
    public const string Database = "database";
}

public sealed record MongoDbConfiguration(
    [property: JsonPropertyName(MongoDbConfigurationSchema.Uri)]
    [property: ConfigurationKeyName(MongoDbConfigurationSchema.Uri)]
    string Uri,
    [property: JsonPropertyName(MongoDbConfigurationSchema.Database)]
    [property: ConfigurationKeyName(MongoDbConfigurationSchema.Database)]
    string Database
);