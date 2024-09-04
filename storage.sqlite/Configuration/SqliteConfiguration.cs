namespace back.zone.storage.sqlite.Configuration;

public sealed record SqliteConfiguration(
    string ConnectionString,
    int MinPoolSize,
    int MaxPoolSize
);