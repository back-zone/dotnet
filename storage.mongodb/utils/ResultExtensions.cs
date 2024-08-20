using back.zone.core.Monads.TryMonad;
using MongoDB.Driver;

namespace back.zone.storage.mongodb.utils;

public static class ResultExtensions
{
    public static Try<TA> CheckAndMap<TA>(this UpdateResult result, TA a)
        where TA : notnull
    {
        return result is { IsAcknowledged: true, MatchedCount: > 0, ModifiedCount: >= 0 }
            ? a
            : new Exception("#failed_to_update_document#");
    }

    public static Try<TA> CheckAndMap<TA>(this DeleteResult result, TA a)
        where TA : notnull
    {
        return result is { IsAcknowledged: true, DeletedCount: > 0 }
            ? a
            : new Exception("#failed_to_delete_document#");
    }
}