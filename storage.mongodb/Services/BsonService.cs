using System.Collections.Immutable;
using back.zone.core.Monads.TryMonad;
using MongoDB.Bson;

namespace back.zone.storage.mongodb.Services;

/// <summary>
///     Provides services for parsing string representations of MongoDB ObjectIds using the IO monad.
/// </summary>
public static class BsonService
{
    /// <summary>
    ///     Parses a string representation of an ObjectId and returns an IO monad containing the parsed ObjectId.
    /// </summary>
    /// <param name="id">The string representation of the ObjectId to parse.</param>
    /// <returns>An IO monad containing the parsed ObjectId.</returns>
    public static Try<ObjectId> Parse(string id)
    {
        return Try.Succeed(id).Map(ObjectId.Parse);
    }

    /// <summary>
    ///     Parses an array of string representations of ObjectIds and returns an IO monad containing an immutable array of
    ///     parsed ObjectIds.
    /// </summary>
    /// <param name="ids">The array of string representations of ObjectIds to parse.</param>
    /// <returns>An IO monad containing an immutable array of parsed ObjectIds.</returns>
    public static Try<ImmutableArray<ObjectId>> ParseMany(ImmutableArray<string> ids)
    {
        return Try.Succeed(ids).Map(ParseIds);

        static ImmutableArray<ObjectId> ParseIds(ImmutableArray<string> ids)
        {
            var parsedIds = ImmutableArray.CreateBuilder<ObjectId>(ids.Length);
            foreach (var id in ids) parsedIds.Add(ObjectId.Parse(id));
            return parsedIds.MoveToImmutable();
        }
    }
}