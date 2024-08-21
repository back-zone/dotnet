using System.Text.Json;
using System.Text.Json.Serialization;

namespace back.zone.core.Serde.Json;

/// <summary>
///     Provides configurations for JSON serialization used in the monadic framework.
/// </summary>
public class BackzoneJsonSerializationOptions
{
    /// <summary>
    ///     A pre-configured instance of <see cref="Options" /> with specific settings for monadic serialization.
    /// </summary>
    public static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = false,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Converters = { new OptionJsonConverterFactory() }
    };
}