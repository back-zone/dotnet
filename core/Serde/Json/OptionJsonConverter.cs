using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.core.Monads.OptionMonad;

namespace back.zone.core.Serde.Json;

/// <summary>
///     A custom JSON converter for the Option monad. This converter allows JSON serialization and deserialization of
///     <see cref="Option{TA}" /> types.
/// </summary>
/// <typeparam name="TA">The type of the value inside the Option.</typeparam>
public class OptionJsonConverter<TA> : JsonConverter<Option<TA>> where TA : notnull
{
    /// <summary>
    ///     Reads and converts the JSON to an  <see cref="Option{TA}" /> instance.
    /// </summary>
    /// <param name="reader">The Utf8JsonReader to read from.</param>
    /// <param name="typeToConvert">The type to convert to. In this case,  <see cref="Option{TA}" />.</param>
    /// <param name="options">The JsonSerializerOptions to use during deserialization.</param>
    /// <returns>An <see cref="Option{TA}" /> instance representing the deserialized JSON value.</returns>
    public override Option<TA> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.PropertyName) reader.Read();

        if (reader.TokenType == JsonTokenType.Comment) return Option.None<TA>();

        try
        {
            var value = JsonSerializer.Deserialize<TA>(ref reader, options);
            return value != null ? Option.Some(value) : Option.None<TA>();
        }
        catch (Exception)
        {
            return Option.None<TA>();
        }
    }

    /// <summary>
    ///     Writes an  <see cref="Option{TA}" /> instance as JSON.
    /// </summary>
    /// <param name="writer">The Utf8JsonWriter to write to.</param>
    /// <param name="value">The <see cref="Option{TA}" /> instance to write.</param>
    /// <param name="options">The JsonSerializerOptions to use during serialization.</param>
    public override void Write(Utf8JsonWriter writer, Option<TA> value, JsonSerializerOptions options)
    {
        if (value.TryGetValue(out var a))
            JsonSerializer.Serialize(writer, a, options);
        else
            writer.WriteNullValue();
    }
}