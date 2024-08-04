using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.monads.optionmonad;

namespace back.zone.monads.conversions;

public class OptionJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType) return false;
        var genericType = typeToConvert.GetGenericTypeDefinition();
        return genericType == typeof(Option<>) ||
               (genericType == typeof(Option<>) &&
                typeToConvert.GetGenericArguments()[0].IsGenericType &&
                typeToConvert.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(ImmutableArray<>));
    }

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

public class OptionJsonConverter<A> : JsonConverter<Option<A>> where A : notnull
{
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

    public override void Write(Utf8JsonWriter writer, Option<A> value, JsonSerializerOptions options)
    {
        var unit = () => { };

        value.fold(WriteNone, WriteSome).Invoke();
        return;

        Action WriteNone()
        {
            writer.WriteNullValue();
            return unit;
        }

        Action WriteSome(A a)
        {
            JsonSerializer.Serialize(writer, a, options);
            return unit;
        }
    }
}

public class OptionArrayJsonConverter<A> : JsonConverter<Option<ImmutableArray<A>>> where A : notnull
{
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