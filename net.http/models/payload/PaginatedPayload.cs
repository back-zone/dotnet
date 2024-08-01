using System.Collections.Immutable;
using System.Text.Json.Serialization;
using back.zone.monads.optionmonad;
using back.zone.net.http.models.parameters;

namespace back.zone.net.http.models.payload;

public static class PaginatedPayloadSchema
{
    public const string Result = "result";
    public const string Message = "message";
    public const string PreviousPage = "previous_page";
    public const string CurrentPage = "current_page";
    public const string NextPage = "next_page";
    public const string TotalPages = "total_pages";
    public const string TotalRecords = "total_records";
    public const string PageSize = "page_size";
    public const string Data = "data";
}

public record PaginatedPayload<A>(
    [property: JsonPropertyName(PaginatedPayloadSchema.Result)]
    bool Result,
    [property: JsonPropertyName(PaginatedPayloadSchema.Message)]
    string Message,
    [property: JsonPropertyName(PaginatedPayloadSchema.PreviousPage)]
    Option<int> PreviousPage,
    [property: JsonPropertyName(PaginatedPayloadSchema.CurrentPage)]
    int CurrentPage,
    [property: JsonPropertyName(PaginatedPayloadSchema.NextPage)]
    Option<int> NextPage,
    [property: JsonPropertyName(PaginatedPayloadSchema.TotalPages)]
    long TotalPages,
    [property: JsonPropertyName(PaginatedPayloadSchema.TotalRecords)]
    long TotalRecords,
    [property: JsonPropertyName(PaginatedPayloadSchema.PageSize)]
    int PageSize,
    [property: JsonPropertyName(PaginatedPayloadSchema.Data)]
    Option<ImmutableArray<A>> Data
)
{
    private const string SuccessMessage = "#success#";
    private const string FailureMessage = "#failure#";


    public static PaginatedPayload<A> Make(
        bool result,
        string message,
        PaginationParameters pagination,
        long totalRecords,
        Option<ImmutableArray<A>> data)
    {
        var pageSize = pagination.PageSize == 0 ? 10 : pagination.PageSize;
        var currentPage = pagination.Page;
        var pageFlooring = totalRecords % pageSize == 0 ? 0 : 1;
        var totalPages = totalRecords / pageSize + pageFlooring;
        var previousPage = currentPage - 1 >= 0 ? option.some(currentPage - 1) : option.none<int>();
        var nextPage = currentPage + 1 <= totalPages ? option.some(currentPage + 1) : option.none<int>();
        return new PaginatedPayload<A>(
            result,
            message,
            previousPage,
            currentPage,
            nextPage,
            totalPages,
            totalRecords,
            pageSize,
            data
        );
    }

    public static PaginatedPayload<A> Succeed(
        PaginationParameters pagination,
        long totalRecords,
        Option<ImmutableArray<A>> data
    )
    {
        return Make(true, SuccessMessage, pagination, totalRecords, data);
    }

    public static PaginatedPayload<A> Fail(
        PaginationParameters pagination,
        long totalRecords,
        Option<ImmutableArray<A>> data
    )
    {
        return Make(false, FailureMessage, pagination, totalRecords, data);
    }
}