using System.Collections.Immutable;
using System.Text.Json.Serialization;
using back.zone.core.Monads.OptionMonad;
using back.zone.core.Serde.Json;
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

public record PaginatedPayload<TA>(
    [property: JsonPropertyName(PaginatedPayloadSchema.Result)]
    bool Result,
    [property: JsonPropertyName(PaginatedPayloadSchema.Message)]
    string Message,
    [property: JsonPropertyName(PaginatedPayloadSchema.PreviousPage)]
    [property: JsonConverter(typeof(OptionJsonConverterFactory))]
    Option<int> PreviousPage,
    [property: JsonPropertyName(PaginatedPayloadSchema.CurrentPage)]
    int CurrentPage,
    [property: JsonPropertyName(PaginatedPayloadSchema.NextPage)]
    [property: JsonConverter(typeof(OptionJsonConverterFactory))]
    Option<int> NextPage,
    [property: JsonPropertyName(PaginatedPayloadSchema.TotalPages)]
    long TotalPages,
    [property: JsonPropertyName(PaginatedPayloadSchema.TotalRecords)]
    long TotalRecords,
    [property: JsonPropertyName(PaginatedPayloadSchema.PageSize)]
    int PageSize,
    [property: JsonPropertyName(PaginatedPayloadSchema.Data)]
    [property: JsonConverter(typeof(OptionJsonConverterFactory))]
    Option<ImmutableArray<TA>> Data
)
    where TA : notnull;

public static class PaginatedPayload
{
    private const string SuccessMessage = "#success#";
    private const string FailureMessage = "#failure#";

    public static PaginatedPayload<TA> Make<TA>(
        bool result,
        string message,
        PaginationParameters pagination,
        long totalRecords,
        Option<ImmutableArray<TA>> data
    )
        where TA : notnull
    {
        var pageSize = pagination.PageSize == 0 ? 10 : pagination.PageSize;
        var currentPage = pagination.Page;
        var pageFlooring = totalRecords % pageSize == 0 ? 0 : 1;
        var totalPages = totalRecords / pageSize + pageFlooring;
        var previousPage = currentPage - 1 >= 0 ? Option.Some(currentPage - 1) : Option.None<int>();
        var nextPage = currentPage + 1 <= totalPages ? Option.Some(currentPage + 1) : Option.None<int>();
        return new PaginatedPayload<TA>(
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

    public static PaginatedPayload<TA> Succeed<TA>(
        PaginationParameters pagination,
        long totalRecords,
        Option<ImmutableArray<TA>> data
    )
        where TA : notnull
    {
        return Make(true, SuccessMessage, pagination, totalRecords, data);
    }

    public static PaginatedPayload<TA> Fail<TA>(
        PaginationParameters pagination,
        long totalRecords,
        Option<ImmutableArray<TA>> data
    )
        where TA : notnull
    {
        return Make(false, FailureMessage, pagination, totalRecords, data);
    }
}