using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.monads.configurations;
using back.zone.monads.extensions;
using Microsoft.Extensions.Configuration;

namespace back.zone.storage.mongodb.configuration;

public static class MongoDbConfigurationSchema
{
    public const string Uri = "uri";
    public const string Database = "database";
    public const string Section = "mongodb";
}

public sealed record MongoDbConfiguration(
    [property: JsonPropertyName(MongoDbConfigurationSchema.Uri)]
    string Uri,
    [property: JsonPropertyName(MongoDbConfigurationSchema.Database)]
    string Database
)
{
    public static MongoDbConfiguration FromConfig(IConfigurationSection section)
    {
        return JsonSerializer.Deserialize<MongoDbConfiguration>(
            JsonSerializer.Serialize(section.GetAsRawJson(), MonadicSerializationConfigurations.JsonSerializerOptions)
        ) ?? throw new Exception("#mongodb_configuration_failed_to_deserialize#");
    }
}