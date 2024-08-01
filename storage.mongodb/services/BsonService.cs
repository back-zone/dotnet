using System.Collections.Immutable;
using back.zone.monads.iomonad;
using monads.iomonad;
using MongoDB.Bson;

namespace back.zone.storage.mongodb.services;

public static class BsonService
{
    public static IO<ObjectId> Parse(string id)
    {
        return io.succeed(id).map(ObjectId.Parse);
    }

    public static IO<ImmutableArray<ObjectId>> ParseMany(ImmutableArray<string> ids)
    {
        return io.succeed(ids).map(ParseIds);

        static ImmutableArray<ObjectId> ParseIds(ImmutableArray<string> ids)
        {
            var result = ImmutableArray.CreateBuilder<ObjectId>(ids.Length);
            foreach (var id in ids) result.Add(ObjectId.Parse(id));
            return result.ToImmutable();
        }
    }
}