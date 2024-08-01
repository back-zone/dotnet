using System.Text.Json;
using System.Text.Json.Serialization;
using back.zone.monads.optionmonad;
using back.zone.net.http.models.payload;

namespace back.zone.net.http.configuration.converters;

public class OptionJsonConverter<A> : JsonConverter<Payload<A>> where A : notnull
{
    public override Payload<A>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        var result = root.GetProperty(PayloadSchema.Result).GetBoolean();
        var message = root.GetProperty(PayloadSchema.Message).GetString();
        var data = root.TryGetProperty(PayloadSchema.Data, out var dataElement)
            ? option.some(JsonSerializer.Deserialize<A>(dataElement.GetRawText(), options))
            : option.none<A>();

        return new Payload<A>(result, message, data);
    }

    public override void Write(Utf8JsonWriter writer, Payload<A> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteBoolean(PayloadSchema.Result, value.Result);
        writer.WriteString(PayloadSchema.Message, value.Message);

        var writeEndObject = writer.WriteEndObject;

        Action WriteData(A a)
        {
            writer.WritePropertyName(PayloadSchema.Data);
            JsonSerializer.Serialize(writer, a, options);
            return writeEndObject;
        }

        value.Data.fold(writeEndObject, WriteData).Invoke();
    }
}