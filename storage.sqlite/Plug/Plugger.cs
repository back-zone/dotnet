using System.Text;
using back.zone.core.Monads.OptionMonad;
using back.zone.storage.sqlite.Configuration;
using back.zone.storage.sqlite.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back.zone.storage.sqlite.Plug;

public static class SqliteServicePlugger
{
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
        var password = Option.Of(configurationManager["sqlite:password"]);

        if (minPoolSize < 1 || maxPoolSize < minPoolSize)
            throw new InvalidDataException("Invalid pool size parameters.");

        var connectionString = new StringBuilder();
        connectionString.Append("Data Source=");
        connectionString.Append(dbPath);
        connectionString.Append(';');

        if (password.TryGetValue(out var pwd))
        {
            connectionString.Append("Password=");
            connectionString.Append(pwd);
            connectionString.Append(';');
        }

        var sqliteConfiguration = new SqliteConfiguration(
            connectionString.ToString(),
            minPoolSize,
            maxPoolSize,
            password
        );

        var sqliteService = new SqliteService(sqliteConfiguration);

        serviceCollection.AddSingleton(sqliteService);
    }
}