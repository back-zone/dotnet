namespace back.zone.net.http.Models.Parameters;

public record ListParameters(
    List<FilterParameters>? Filters,
    List<SortParameters>? Sorters,
    PaginationParameters Pagination
);