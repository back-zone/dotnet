using back.zone.storage.redis.Configuration;
using StackExchange.Redis;

namespace back.zone.storage.redis.Services;

public sealed class RedisService
{
    private readonly ConfigurationOptions _configuration;
    private ConnectionMultiplexer _connectionMultiplexer;

    public RedisService(RedisConfiguration redisConfiguration)
    {
        _configuration = redisConfiguration.AsConfigurationOptions();
        _connectionMultiplexer = ConnectionMultiplexer.Connect(_configuration);
    }

    public bool IsConnected => _connectionMultiplexer.IsConnected;

    public IDatabase GetDatabase()
    {
        if (IsConnected) return _connectionMultiplexer.GetDatabase();

        if (Connect()) return _connectionMultiplexer.GetDatabase();

        throw new InvalidOperationException("Failed to connect to Redis");
    }

    public async Task<bool> ConnectAsync()
    {
        if (IsConnected) return true;

        _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(_configuration);

        return IsConnected;
    }

    public bool Connect()
    {
        if (IsConnected) return true;

        _connectionMultiplexer = ConnectionMultiplexer.Connect(_configuration);

        return IsConnected;
    }
}