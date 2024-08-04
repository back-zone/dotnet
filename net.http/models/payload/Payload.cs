using System.Text.Json.Serialization;
using back.zone.monads.conversions;
using back.zone.monads.optionmonad;

namespace back.zone.net.http.models.payload;

public static class PayloadSchema
{
    public const string Result = "result";
    public const string Message = "message";
    public const string Data = "data";
}

public sealed record Payload<A>(
    [property: JsonPropertyName(PayloadSchema.Result)]
    bool Result,
    [property: JsonPropertyName(PayloadSchema.Message)]
    string Message,
    [property: JsonPropertyName(PayloadSchema.Data)]
    [property: JsonConverter(typeof(OptionJsonConverterFactory))]
    Option<A> Data
)
    where A : notnull;

public static class Payload
{
    private const string SuccessMessage = "#success#";
    private const string FailureMessage = "#failure#";

    public static Payload<A> Succeed<A>()
    {
        return new Payload<A>(true, SuccessMessage, option.none<A>());
    }

    public static Payload<A> SucceedWithMessage<A>(string message)
    {
        return new Payload<A>(true, message, option.none<A>());
    }

    public static Payload<A> SucceedWithData<A>(Option<A> data)
    {
        return new Payload<A>(true, SuccessMessage, data);
    }

    public static Payload<A> SucceedWithMessageAndData<A>(string message, Option<A> data)
    {
        return new Payload<A>(true, message, data);
    }

    public static Payload<A> Fail<A>()
    {
        return new Payload<A>(false, FailureMessage, option.none<A>());
    }

    public static Payload<A> FailWithMessage<A>(string message = FailureMessage)
    {
        return new Payload<A>(false, message, option.none<A>());
    }

    public static Payload<A> FailWithData<A>(Option<A> data)
    {
        return new Payload<A>(false, FailureMessage, data);
    }

    public static Payload<A> FailWithMessageAndData<A>(string message, Option<A> data)
    {
        return new Payload<A>(false, message, data);
    }

    public static Payload<A> FailFromException<A>(Exception exception)
    {
        return FailWithMessage<A>(exception.Message);
    }
}