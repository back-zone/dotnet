using System.Text.Json.Serialization;
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
    Option<A> Data
)
    where A : notnull
{
    private const string SuccessMessage = "#success#";
    private const string FailureMessage = "#failure#";

    public static Payload<A> Succeed()
    {
        return new Payload<A>(true, SuccessMessage, option.none<A>());
    }

    public static Payload<A> SucceedWithMessage(string message)
    {
        return new Payload<A>(true, message, option.none<A>());
    }

    public static Payload<A> SucceedWithData(Option<A> data)
    {
        return new Payload<A>(true, SuccessMessage, data);
    }

    public static Payload<A> SucceedWithMessageAndData(string message, Option<A> data)
    {
        return new Payload<A>(true, message, data);
    }

    public static Payload<A> Fail()
    {
        return new Payload<A>(false, FailureMessage, option.none<A>());
    }

    public static Payload<A> FailWithMessage(string message = FailureMessage)
    {
        return new Payload<A>(false, message, option.none<A>());
    }

    public static Payload<A> FailWithData(Option<A> data)
    {
        return new Payload<A>(false, FailureMessage, data);
    }

    public static Payload<A> FailWithMessageAndData(string message, Option<A> data)
    {
        return new Payload<A>(false, message, data);
    }

    public static Payload<A> FailFromException(Exception exception)
    {
        return FailWithMessage($"An error occurred: {exception.Message}");
    }
}