using System.Text;
using back.zone.storage.sqlite.Configuration;
using back.zone.storage.sqlite.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back.zone.storage.sqlite.Plug;

/// <summary>
///     This class provides a method to plug in the SQLite service into the application's service collection.
/// </summary>
public static class SqliteServicePlugger
{
    /// <summary>
    ///     Plugs in the SQLite service into the application's service collection.
    /// </summary>
    /// <param name="configurationManager">
    ///     An instance of <see cref="IConfigurationManager" /> to retrieve configuration
    ///     settings.
    /// </param>
    /// <param name="serviceCollection">An instance of <see cref="IServiceCollection" /> to register the SQLite service.</param>
    public static void Plug(
        IConfigurationManager configurationManager,
        IServiceCollection serviceCollection
    )
    {
        var dbPath =
            configurationManager["sqlite:db_path"]
            ?? throw new InvalidDataException("Sqlite path not found!");

        var minPoolSize = int.TryParse(configurationManager["sqlite:min_pool_size"], out var minPs)
            ? minPs - 1
            : 4;
        var maxPoolSize = int.TryParse(configurationManager["sqlite:max_pool_size"], out var maxPs)
            ? maxPs - 1
            : 19;

        if (minPoolSize < 1 || maxPoolSize < minPoolSize)
            throw new InvalidDataException("Invalid pool size parameters.");

        var connectionString = new StringBuilder();
        connectionString.Append("Data Source=");
        connectionString.Append(dbPath);
        connectionString.Append(';');

        var sqliteConfiguration = new SqliteConfiguration(
            connectionString.ToString(),
            minPoolSize,
            maxPoolSize
        );

        var sqliteService = new SqliteService(sqliteConfiguration);

        serviceCollection.AddSingleton(sqliteService);
    }
}