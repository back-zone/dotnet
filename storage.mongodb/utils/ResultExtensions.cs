using back.zone.monads.iomonad;
using MongoDB.Driver;

namespace back.zone.storage.mongodb.utils;

public static class ResultExtensions
{
    public static IO<A> CheckAndMap<A>(this UpdateResult result, A a)
        where A : notnull
    {
        return result is { IsAcknowledged: true, MatchedCount: > 0, ModifiedCount: >= 0 }
            ? io.succeed(a)
            : io.fail<A>("#failed_to_update_document#");
    }

    public static IO<A> CheckAndMap<A>(this DeleteResult result, A a)
        where A : notnull
    {
        return result is { IsAcknowledged: true, DeletedCount: > 0 }
            ? io.succeed(a)
            : io.fail<A>("#failed_to_delete_document#");
    }
}