using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.core.Monads.OptionMonad;

namespace back.zone.core.Serde.Json;

/// <summary>
///     A factory for creating JSON converters for Option types.
/// </summary>
public class OptionJsonConverterFactory : JsonConverterFactory
{
    /// <summary>
    ///     Determines whether this instance can convert the specified type.
    /// </summary>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <returns>
    ///     <c>true</c> if this instance can convert the specified type; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType) return false;

        var genericType = typeToConvert.GetGenericTypeDefinition();

        if (genericType != typeof(Option<>)) return false;
        var innerType = typeToConvert.GetGenericArguments()[0];
        return !innerType.IsGenericType ||
               innerType.GetGenericTypeDefinition() == typeof(ImmutableArray<>);
    }

    /// <summary>
    ///     Creates a JSON converter for the specified type.
    /// </summary>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>
    ///     A JSON converter for the specified type, or null if the type cannot be converted.
    /// </returns>
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (!typeToConvert.IsGenericType || typeToConvert.GetGenericTypeDefinition() != typeof(Option<>))
            throw new JsonException($"[OptionJsonConverterFactory] Unsupported type: {typeToConvert.FullName}");

        var genericArgument = typeToConvert.GetGenericArguments()[0];

        if (genericArgument.IsGenericType && genericArgument.GetGenericTypeDefinition() == typeof(ImmutableArray<>))
        {
            var innerType = genericArgument.GetGenericArguments()[0];
            var converterType = typeof(OptionJsonArrayConverter<>).MakeGenericType(innerType);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
        else
        {
            var converterType = typeof(OptionJsonConverter<>).MakeGenericType(genericArgument);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
    }
}