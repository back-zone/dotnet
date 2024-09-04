using back.zone.storage.sqlite.Services;
using Microsoft.Extensions.DependencyInjection;

namespace back.zone.storage.sqlite.Providers;

public static class SqliteServiceProvider
{
    private static SqliteService? ServiceInstance { get; set; }

    public static void Set(IServiceProvider services)
    {
        ServiceInstance = services.GetRequiredService<SqliteService>();
    }

    public static SqliteService Get()
    {
        if (ServiceInstance is null) throw new InvalidOperationException("#service_provider_not_initialized#");
        return ServiceInstance;
    }
}