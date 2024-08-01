using back.zone.monads.iomonad;
using back.zone.storage.mongodb.configuration;
using monads.iomonad;
using MongoDB.Driver;

namespace back.zone.storage.mongodb.services;

public sealed class MongoService
{
    private readonly MongoClient _client;
    private readonly string _database;

    public MongoService(MongoDbConfiguration mongoDbConfiguration)
    {
        _database = mongoDbConfiguration.Database;
        _client = new MongoClient(mongoDbConfiguration.Uri);
    }

    public IO<IMongoCollection<A>> GetCollection<A>(string collectionName)
    {
        return io.succeed(collectionName).map(CollectionOf<A>);

        IMongoCollection<A> CollectionOf<A>(string name)
        {
            return _client.GetDatabase(_database).GetCollection<A>(name);
        }
    }
}