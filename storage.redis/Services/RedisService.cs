using back.zone.core.Monads.TryMonad;
using back.zone.storage.redis.Configuration;
using StackExchange.Redis;

namespace back.zone.storage.redis.Services;

/// <summary>
///     Represents a service for interacting with a Redis database.
/// </summary>
public sealed class RedisService
{
    private readonly ConfigurationOptions _configuration;
    private readonly ConnectionMultiplexer _connectionMultiplexer;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RedisService" /> class.
    /// </summary>
    /// <param name="redisConfiguration">The configuration for the Redis service.</param>
    public RedisService(RedisConfiguration redisConfiguration)
    {
        _configuration = redisConfiguration.AsConfigurationOptions();
        _connectionMultiplexer = ConnectionMultiplexer.Connect(_configuration);
    }

    /// <summary>
    ///     Gets a value indicating whether the connection to the Redis server is established.
    /// </summary>
    public bool IsConnected => _connectionMultiplexer.IsConnected;

    /// <summary>
    ///     Attempts to establish a connection to the Redis server.
    /// </summary>
    /// <returns>A <see cref="Try{IDatabase}" /> representing the result of the connection attempt.</returns>
    public Try<IDatabase> Connect()
    {
        return Try
            .Succeed(_connectionMultiplexer)
            .Zip(Try.Succeed(_configuration))
            .UnZip(DoConnect);

        static IDatabase DoConnect(
            ConnectionMultiplexer multiplexer,
            ConfigurationOptions options
        )
        {
            if (multiplexer.IsConnected) return multiplexer.GetDatabase();

            multiplexer = ConnectionMultiplexer.Connect(options);

            return multiplexer.GetDatabase();
        }
    }

    /// <summary>
    ///     Asynchronously attempts to establish a connection to the Redis server.
    /// </summary>
    /// <returns>A <see cref="Try{IDatabase}" /> representing the result of the asynchronous connection attempt.</returns>
    public async Task<Try<IDatabase>> ConnectAsync()
    {
        return await Try
            .Succeed(_connectionMultiplexer)
            .Zip(Try.Succeed(_configuration))
            .UnZipAsync(DoConnectAsync);

        static async Task<IDatabase> DoConnectAsync(
            ConnectionMultiplexer multiplexer,
            ConfigurationOptions options)
        {
            if (multiplexer.IsConnected) return multiplexer.GetDatabase();

            multiplexer = await ConnectionMultiplexer.ConnectAsync(options);

            return multiplexer.GetDatabase();
        }
    }

    /// <summary>
    ///     Gets the Redis database instance.
    ///     If the connection is not established, it will attempt to connect first.
    /// </summary>
    /// <returns>A <see cref="Try{IDatabase}" /> representing the result of the database retrieval.</returns>
    public Try<IDatabase> GetDatabase()
    {
        return Try
            .Succeed(_connectionMultiplexer)
            .FlatMap(mp => Try.Succeed(mp.GetDatabase()))
            .OrElse(Connect());
    }

    /// <summary>
    ///     Asynchronously gets the Redis database instance.
    ///     If the connection is not established, it will attempt to connect first.
    /// </summary>
    /// <returns>A <see cref="Try{IDatabase}" /> representing the result of the asynchronous database retrieval.</returns>
    public async Task<Try<IDatabase>> GetDatabaseAsync()
    {
        return await Try
            .Succeed(_connectionMultiplexer)
            .FlatMap(mp => Try.Succeed(mp.GetDatabase()))
            .OrElseAsync(ConnectAsync());
    }
}