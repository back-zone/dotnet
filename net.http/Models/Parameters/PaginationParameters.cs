using back.zone.core.Monads.OptionMonad;

namespace back.zone.net.http.Models.Parameters;

public sealed record PaginationParameters(
    int Page,
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