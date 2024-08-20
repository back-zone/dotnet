using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.core.Monads.OptionMonad;

namespace back.zone.core.Serde.Json;

public class OptionJsonArrayConverter<TA> : JsonConverter<Option<ImmutableArray<TA>>> where TA : notnull
{
    public override Option<ImmutableArray<TA>> Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.PropertyName) reader.Read();

        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("[OptionArrayJsonConverter]#excepted_start_of_array#");

        try
        {
            var arrayBuilder = ImmutableArray.CreateBuilder<TA>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;

                if (reader.TokenType == JsonTokenType.Null)
                    continue;

                var value = JsonSerializer.Deserialize<TA>(ref reader, options);
                if (value != null) arrayBuilder.Add(value);
            }

            return arrayBuilder.Count > 0
                ? arrayBuilder.MoveToImmutable()
                : Option.None<ImmutableArray<TA>>();
        }
        catch (JsonException)
        {
            return Option.None<ImmutableArray<TA>>();
        }
    }

    public override void Write(Utf8JsonWriter writer, Option<ImmutableArray<TA>> value, JsonSerializerOptions options)
    {
        if (value.TryGetValue(out var immutableArray))
        {
            writer.WriteStartArray();
            foreach (var a in immutableArray) JsonSerializer.Serialize(writer, a, options);
            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}