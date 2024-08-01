using System.Text.Json.Serialization;

namespace back.zone.net.http.models.parameters;

public static class FilterParametersSchema
{
    public const string Field = "field";
    public const string Value = "value";
}

public sealed record FilterParameters(
    [property: JsonPropertyName(FilterParametersSchema.Field)]
    string Field,
    [property: JsonPropertyName(FilterParametersSchema.Value)]
    string Value
);