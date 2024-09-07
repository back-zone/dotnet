using System.Collections.Immutable;
using back.zone.core.Monads.OptionMonad;
using back.zone.net.http.Models.Parameters;

namespace back.zone.net.http.Models.Payload;

public record PaginatedPayload<TA>(
    bool Result,
    string Message,
    int? PreviousPage,
    int CurrentPage,
    int? NextPage,
    long TotalPages,
    long TotalRecords,
    int PageSize,
    ImmutableArray<TA>? Data
)
    where TA : notnull;

public static class PaginatedPayload
{
    private const string SuccessMessage = "#success#";
    private const string FailureMessage = "#failure#";

    /// <summary>
    ///     Creates a new instance of <see cref="PaginatedPayload{TA}" /> with the provided parameters.
    /// </summary>
    /// <typeparam name="TA">The type of data contained in the payload.</typeparam>
    /// <param name="result">Indicates the success or failure of the operation.</param>
    /// <param name="message">A message describing the outcome of the operation.</param>
    /// <param name="pagination">The pagination parameters used to calculate the page information.</param>
    /// <param name="totalRecords">The total number of records available.</param>
    /// <param name="data">The data to be included in the payload.</param>
    /// <returns>A new instance of <see cref="PaginatedPayload{TA}" /> with the provided parameters.</returns>
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

    /// <summary>
    ///     Creates a new instance of <see cref="PaginatedPayload{TA}" /> with success status and the provided parameters.
    /// </summary>
    /// <typeparam name="TA">The type of data contained in the payload.</typeparam>
    /// <param name="pagination">The pagination parameters used to calculate the page information.</param>
    /// <param name="totalRecords">The total number of records available.</param>
    /// <param name="data">The data to be included in the payload.</param>
    /// <returns>
    ///     A new instance of <see cref="PaginatedPayload{TA}" /> with success status, the provided parameters, and
    ///     calculated page information.
    /// </returns>
    public static PaginatedPayload<TA> Succeed<TA>(
        PaginationParameters pagination,
        long totalRecords,
        Option<ImmutableArray<TA>> data
    )
        where TA : notnull
    {
        return Make(true, SuccessMessage, pagination, totalRecords, data);
    }

    /// <summary>
    ///     Creates a new instance of <see cref="PaginatedPayload{TA}" /> with failure status and the provided parameters.
    /// </summary>
    /// <typeparam name="TA">The type of data contained in the payload.</typeparam>
    /// <param name="pagination">The pagination parameters used to calculate the page information.</param>
    /// <param name="totalRecords">The total number of records available.</param>
    /// <param name="data">The data to be included in the payload.</param>
    /// <returns>
    ///     A new instance of <see cref="PaginatedPayload{TA}" /> with failure status, the provided parameters, and
    ///     calculated page information.
    /// </returns>
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