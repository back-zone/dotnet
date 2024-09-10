using back.zone.core.Monads.OptionMonad;
using StackExchange.Redis;

namespace back.zone.storage.redis.Configuration;

public sealed record RedisConfiguration(
    string[] EndPoints,
    Option<string> CommandName,
    Option<string> ServiceName,
    Option<int> ConnectRetry,
    Option<bool> AllowAdmin,
    Option<string> User,
    Option<string> Password,
    Option<bool> AbortOnConnectFail
)
{
    public ConfigurationOptions AsConfigurationOptions()
    {
        var endPoints = new EndPointCollection();
        foreach (var endPoint in EndPoints) endPoints.Add(endPoint);
        var commandName = CommandName.GetOrElse("default") switch
        {
            "envoy_proxy" => CommandMap.Envoyproxy,
            "sentinel" => CommandMap.Sentinel,
            "twem_proxy" => CommandMap.Twemproxy,
            "ssdb" => CommandMap.SSDB,
            _ => CommandMap.Default
        };

        return new ConfigurationOptions
        {
            EndPoints = endPoints,
            CommandMap = commandName,
            ServiceName = ServiceName.GetOrElse(string.Empty),
            ConnectRetry = ConnectRetry.GetOrElse(3),
            AllowAdmin = AllowAdmin.GetOrElse(false),
            User = User.GetOrElse(string.Empty),
            Password = Password.GetOrElse(string.Empty),
            AbortOnConnectFail = AbortOnConnectFail.GetOrElse(false)
        };
    }
}