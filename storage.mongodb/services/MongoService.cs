using back.zone.monads.iomonad;
using back.zone.storage.mongodb.configuration;
using MongoDB.Driver;

namespace back.zone.storage.mongodb.services;

/// <summary>
///     Represents a service for interacting with MongoDB collections.
/// </summary>
public sealed class MongoService
{
    private readonly MongoClient _client;
    private readonly string _database;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MongoService" /> class.
    /// </summary>
    /// <param name="mongoDbConfiguration">The MongoDB configuration.</param>
    public MongoService(MongoDbConfiguration mongoDbConfiguration)
    {
        _database = mongoDbConfiguration.Database;
        _client = new MongoClient(mongoDbConfiguration.Uri);
    }

    /// <summary>
    ///     Gets a collection of type <typeparamref name="A" /> from MongoDB.
    /// </summary>
    /// <typeparam name="A">The type of the collection elements.</typeparam>
    /// <param name="collectionName">The name of the collection.</param>
    /// <returns>An IO monad containing the requested MongoDB collection.</returns>
    public IO<IMongoCollection<A>> GetCollection<A>(string collectionName)
    {
        return io.succeed(collectionName).zipWith(io.succeed((_client, _database)), CollectionOf<A>);

        static IMongoCollection<A> CollectionOf<A>(
            string collectionName,
            (MongoClient client, string database) connectionParameters
        )
        {
            return connectionParameters.client.GetDatabase(connectionParameters.database)
                .GetCollection<A>(collectionName);
        }
    }
}