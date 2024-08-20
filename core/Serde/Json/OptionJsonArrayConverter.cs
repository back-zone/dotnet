using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.core.Monads.OptionMonad;

namespace back.zone.core.Serde.Json;

/// <summary>
///     A custom JSON converter for serializing and deserializing <see cref="Option{ImmutableArray}" /> instances.
/// </summary>
/// <typeparam name="TA">The type of elements in the array.</typeparam>
public class OptionJsonArrayConverter<TA> : JsonConverter<Option<ImmutableArray<TA>>> where TA : notnull
{
    /// <summary>
    ///     Reads and converts the JSON to a <see cref="ImmutableArray{TA}" /> instance.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>An <see cref="ImmutableArray{TA}" /> instance.</returns>
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

    /// <summary>
    ///     Writes a <see cref="ImmutableArray{TA}" /> instance to JSON.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The <see cref="ImmutableArray{TA}" /> instance to write.</param>
    /// <param name="options">The JSON serializer options.</param>
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