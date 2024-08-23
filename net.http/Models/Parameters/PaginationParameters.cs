using System.Text.Json.Serialization;
using back.zone.core.Monads.OptionMonad;

namespace back.zone.net.http.Models.Parameters;

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
        return (maybePage.TryGetValue(out var page), maybePageSize.TryGetValue(out var pageSize)) switch
        {
            (true, true) => Make(page, pageSize),
            (true, false) => Make(page, 10),
            (false, true) => Make(0, pageSize),
            (false, false) => Make(0, 10)
        };
    }

    public static PaginationParameters Make(int page, int pageSize)
    {
        return new PaginationParameters(page, pageSize);
    }
}