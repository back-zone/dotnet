using back.zone.storage.mongodb.Configuration;
using back.zone.storage.mongodb.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back.zone.storage.mongodb.Plug;

public static class MongoDbServicePlugger
{
    public static void Plug(
        IConfigurationManager config,
        IServiceCollection services
    )
    {
        var uri = config["mongo:uri"] ?? throw new InvalidOperationException("Missing 'mongo:uri' configuration key.");
        var database = config["mongo:database"] ??
                       throw new InvalidOperationException("Missing 'mongo:database' configuration key.");

        var mongoDbConfiguration = new MongoDbConfiguration(uri, database);

        var mongoService = new MongoService(mongoDbConfiguration);

        services.AddSingleton(mongoService);
    }
}