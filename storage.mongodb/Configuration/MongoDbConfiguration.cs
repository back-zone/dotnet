namespace back.zone.storage.mongodb.Configuration;

public sealed record MongoDbConfiguration(
    string Uri,
    string Database
);