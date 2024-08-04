using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.monads.conversions;

namespace back.zone.monads.configurations;

/// <summary>
///     Provides configurations for JSON serialization used in the monadic framework.
/// </summary>
public class MonadicSerializationConfigurations
{
    /// <summary>
    ///     A pre-configured instance of <see cref="JsonSerializerOptions" /> with specific settings for monadic serialization.
    /// </summary>
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = false,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Converters = { new OptionJsonConverterFactory() }
    };
}