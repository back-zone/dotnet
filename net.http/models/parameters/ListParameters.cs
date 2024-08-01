using System.Text.Json.Serialization;

namespace back.zone.net.http.models.parameters;

public static class ListParametersSchema
{
    public const string Filters = "filters";
    public const string Sorters = "sorters";
    public const string Pagination = "pagination";
}

public record ListParameters(
    [property: JsonPropertyName(ListParametersSchema.Filters)]
    List<FilterParameters>? Filters,
    [property: JsonPropertyName(ListParametersSchema.Sorters)]
    List<SortParameters>? Sorters,
    [property: JsonPropertyName(ListParametersSchema.Pagination)]
    PaginationParameters Pagination
);