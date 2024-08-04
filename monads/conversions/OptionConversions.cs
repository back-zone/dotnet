using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.monads.optionmonad;

namespace back.zone.monads.conversions;

/// <summary>
///     A factory for creating JSON converters for Option and ImmutableArray types.
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
        return genericType == typeof(Option<>) ||
               (genericType == typeof(Option<>) &&
                typeToConvert.GetGenericArguments()[0].IsGenericType &&
                typeToConvert.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(ImmutableArray<>));
    }

    /// <summary>
    ///     Creates a JSON converter for the specified type.
    /// </summary>
    /// <param name="type">The type to convert.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>A JSON converter for the specified type.</returns>
    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Option<>))
        {
            var genericArgument = type.GetGenericArguments()[0];
            if (genericArgument.IsGenericType && genericArgument.GetGenericTypeDefinition() == typeof(ImmutableArray<>))
            {
                var innerType = genericArgument.GetGenericArguments()[0];
                var converterType = typeof(OptionArrayJsonConverter<>).MakeGenericType(innerType);
                return (JsonConverter)Activator.CreateInstance(converterType)!;
            }
            else
            {
                var converterType = typeof(OptionJsonConverter<>).MakeGenericType(genericArgument);
                return (JsonConverter)Activator.CreateInstance(converterType)!;
            }
        }

        throw new JsonException($"[OptionJsonConverterFactory]#unsupported_type#[{type}]");
    }
}

/// <summary>
///     Represents a JSON converter for Option type.
///     This converter can handle Option of any non-nullable type.
/// </summary>
/// <typeparam name="A">The type of the value inside the Option.</typeparam>
public class OptionJsonConverter<A> : JsonConverter<Option<A>> where A : notnull
{
    /// <summary>
    ///     Reads and converts the JSON to an Option of type A.
    /// </summary>
    /// <param name="reader">The Utf8JsonReader to read from.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>An Option of type A.</returns>
    public override Option<A> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.PropertyName) reader.Read();

        if (reader.TokenType == JsonTokenType.Null) return option.none<A>();

        try
        {
            var value = JsonSerializer.Deserialize<A>(ref reader, options);
            return value != null ? option.some(value) : option.none<A>();
        }
        catch (JsonException)
        {
            return option.none<A>();
        }
    }

    /// <summary>
    ///     Writes an Option of type A to a JSON writer.
    /// </summary>
    /// <param name="writer">The Utf8JsonWriter to write to.</param>
    /// <param name="value">The Option of type A to write.</param>
    /// <param name="options">The JSON serializer options.</param>
    public override void Write(Utf8JsonWriter writer, Option<A> value, JsonSerializerOptions options)
    {
        var unit = () => { };

        // Fold over the Option, writing None or Some value
        value.fold(WriteNone, WriteSome).Invoke();
        return;

        // Write None as null value
        Action WriteNone()
        {
            writer.WriteNullValue();
            return unit;
        }

        // Write Some value by serializing it
        Action WriteSome(A a)
        {
            JsonSerializer.Serialize(writer, a, options);
            return unit;
        }
    }
}

/// <summary>
///     Represents a JSON converter for Option type of ImmutableArray.
///     This converter can handle Option of ImmutableArray of any non-nullable type.
/// </summary>
/// <typeparam name="A">The type of the value inside the ImmutableArray inside the Option.</typeparam>
public class OptionArrayJsonConverter<A> : JsonConverter<Option<ImmutableArray<A>>> where A : notnull
{
    /// <summary>
    ///     Reads and converts the JSON to an Option of ImmutableArray of type A.
    /// </summary>
    /// <param name="reader">The Utf8JsonReader to read from.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>An Option of ImmutableArray of type A.</returns>
    public override Option<ImmutableArray<A>> Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        if (reader.TokenType == JsonTokenType.Null)
            return option.none<ImmutableArray<A>>();

        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("[OptionArrayJsonConverter]#expected_start_of_array#");

        var arrayBuilder = ImmutableArray.CreateBuilder<A>();
        while (reader.Read())
        {
            switch (reader)
            {
                case { TokenType: JsonTokenType.EndArray }:
                    return option.some(arrayBuilder.ToImmutableArray());
                case { TokenType: JsonTokenType.Null }:
                    continue;
            }

            var value = JsonSerializer.Deserialize<A>(ref reader, options);
            if (value != null) arrayBuilder.Add(value);
        }

        throw new JsonException("[OptionArrayJsonConverter]#expected_end_of_array#");
    }

    /// <summary>
    ///     Writes an Option of ImmutableArray of type A to a JSON writer.
    /// </summary>
    /// <param name="writer">The Utf8JsonWriter to write to.</param>
    /// <param name="value">The Option of ImmutableArray of type A to write.</param>
    /// <param name="options">The JSON serializer options.</param>
    public override void Write(
        Utf8JsonWriter writer,
        Option<ImmutableArray<A>> value,
        JsonSerializerOptions options
    )
    {
        var unit = () => { };

        value.fold(WriteNone, WriteSome).Invoke();
        return;

        Action WriteNone()
        {
            writer.WriteNullValue();
            return unit;
        }

        Action WriteSome(ImmutableArray<A> array)
        {
            writer.WriteStartArray();

            foreach (var item in array)
                JsonSerializer.Serialize(writer, item, options);

            writer.WriteEndArray();

            return unit;
        }
    }
}