using back.zone.core.Monads.OptionMonad;

namespace back.zone.storage.sqlite.Configuration;

public sealed record SqliteConfiguration(
    string ConnectionString,
    int MinPoolSize,
    int MaxPoolSize,
    Option<string> Password
);