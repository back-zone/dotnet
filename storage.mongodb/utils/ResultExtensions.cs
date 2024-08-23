using back.zone.core.Monads.TryMonad;
using MongoDB.Driver;

namespace back.zone.storage.mongodb.utils;

/// <summary>
///     This class contains extension methods for MongoDB's UpdateResult and DeleteResult.
///     These methods help to check the result of an update or delete operation and map it to a Try monad.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    ///     Checks the UpdateResult and maps it to a Try monad.
    ///     If the update operation was acknowledged, matched at least one document, and modified at least one document,
    ///     the method returns the provided value wrapped in a successful Try.
    ///     Otherwise, it returns an exception wrapped in a failed Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value to be returned.</typeparam>
    /// <param name="result">The UpdateResult to be checked.</param>
    /// <param name="a">The value to be returned if the update operation is successful.</param>
    /// <returns>A Try monad containing the provided value or an exception.</returns>
    public static Try<TA> CheckAndMap<TA>(this UpdateResult result, TA a)
        where TA : notnull
    {
        return result is { IsAcknowledged: true, MatchedCount: > 0, ModifiedCount: >= 0 }
            ? a
            : new Exception("#failed_to_update_document#");
    }

    /// <summary>
    ///     Checks the DeleteResult and maps it to a Try monad.
    ///     If the delete operation was acknowledged and deleted at least one document,
    ///     the method returns the provided value wrapped in a successful Try.
    ///     Otherwise, it returns an exception wrapped in a failed Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value to be returned.</typeparam>
    /// <param name="result">The DeleteResult to be checked.</param>
    /// <param name="a">The value to be returned if the delete operation is successful.</param>
    /// <returns>A Try monad containing the provided value or an exception.</returns>
    public static Try<TA> CheckAndMap<TA>(this DeleteResult result, TA a)
        where TA : notnull
    {
        return result is { IsAcknowledged: true, DeletedCount: > 0 }
            ? a
            : new Exception("#failed_to_delete_document#");
    }
}