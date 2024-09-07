using back.zone.storage.sqlite.Services;
using Microsoft.Extensions.DependencyInjection;

namespace back.zone.storage.sqlite.Providers;

/// <summary>
///     Provides a static access to the SqliteService instance.
/// </summary>
public static class SqliteServiceProvider
{
    /// <summary>
    ///     The singleton instance of SqliteService.
    /// </summary>
    private static SqliteService? ServiceInstance { get; set; }

    /// <summary>
    ///     Initializes the SqliteService instance using the provided service provider.
    /// </summary>
    /// <param name="services">The service provider to retrieve the SqliteService instance.</param>
    public static void Set(IServiceProvider services)
    {
        ServiceInstance = services.GetRequiredService<SqliteService>();
    }

    /// <summary>
    ///     Retrieves the SqliteService instance.
    /// </summary>
    /// <returns>The SqliteService instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the SqliteService instance is not initialized.</exception>
    public static SqliteService Get()
    {
        if (ServiceInstance is null) throw new InvalidOperationException("#service_provider_not_initialized#");
        return ServiceInstance;
    }
}