using System.Text.Json.Serialization;

namespace back.zone.net.http.models.parameters;

public static class SortParametersSchema
{
    public const string Field = "field";
    public const string Direction = "direction";
}

public sealed record SortParameters(
    [property: JsonPropertyName(SortParametersSchema.Field)]
    string Field,
    [property: JsonPropertyName(SortParametersSchema.Direction)]
    string Direction
);