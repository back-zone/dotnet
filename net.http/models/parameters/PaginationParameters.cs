using System.Text.Json.Serialization;
using back.zone.monads.optionmonad;
using back.zone.monads.optionmonad.subtypes;

namespace back.zone.net.http.models.parameters;

public sealed class PaginationParametersSchema
{
    public const string Page = "page";
    public const string PageSize = "page_size";
}

public sealed record PaginationParameters(
    [property: JsonPropertyName(PaginationParametersSchema.Page)]
    int Page,
    [property: JsonPropertyName(PaginationParametersSchema.PageSize)]
    int PageSize
)
{
    public static PaginationParameters Make(Option<int> maybePage, Option<int> maybePageSize)
    {
        return (maybePage, maybePageSize) switch
        {
            (Some<int> (var page), Some<int> (var pageSize)) => Make(page, pageSize),
            (Some<int> (var page), None<int>) => Make(page, 10),
            (None<int>, Some<int>(var pageSize)) => Make(0, pageSize),
            (None<int>, None<int>) => Make(0, 10),
            _ => Make(0, 10)
        };
    }

    public static PaginationParameters Make(int page, int pageSize)
    {
        return new PaginationParameters(page, pageSize);
    }
}