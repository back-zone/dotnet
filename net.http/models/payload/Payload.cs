using System.Text.Json.Serialization;
using back.zone.core.Monads.OptionMonad;
using back.zone.core.Serde.Json;

namespace back.zone.net.http.models.payload;

public static class PayloadSchema
{
    public const string Result = "result";
    public const string Message = "message";
    public const string Data = "data";
}

public sealed record Payload<TA>(
    [property: JsonPropertyName(PayloadSchema.Result)]
    bool Result,
    [property: JsonPropertyName(PayloadSchema.Message)]
    string Message,
    [property: JsonPropertyName(PayloadSchema.Data)]
    [property: JsonConverter(typeof(OptionJsonConverterFactory))]
    Option<TA> Data
)
    where TA : notnull;

public static class Payload
{
    private const string SuccessMessage = "#success#";
    private const string FailureMessage = "#failure#";

    public static Payload<TA> Succeed<TA>()
        where TA : notnull
    {
        return new Payload<TA>(true, SuccessMessage, Option.None<TA>());
    }

    public static Payload<TA> SucceedWithMessage<TA>(string message)
        where TA : notnull
    {
        return new Payload<TA>(true, message, Option.None<TA>());
    }

    public static Payload<TA> SucceedWithData<TA>(Option<TA> data)
        where TA : notnull
    {
        return new Payload<TA>(true, SuccessMessage, data);
    }

    public static Payload<TA> SucceedWithMessageAndData<TA>(string message, Option<TA> data)
        where TA : notnull
    {
        return new Payload<TA>(true, message, data);
    }

    public static Payload<TA> Fail<TA>()
        where TA : notnull
    {
        return new Payload<TA>(false, FailureMessage, Option.None<TA>());
    }

    public static Payload<TA> FailWithMessage<TA>(string message = FailureMessage)
        where TA : notnull
    {
        return new Payload<TA>(false, message, Option.None<TA>());
    }

    public static Payload<TA> FailWithData<TA>(Option<TA> data)
        where TA : notnull
    {
        return new Payload<TA>(false, FailureMessage, data);
    }

    public static Payload<TA> FailWithMessageAndData<TA>(string message, Option<TA> data)
        where TA : notnull
    {
        return new Payload<TA>(false, message, data);
    }

    public static Payload<TA> FailFromException<TA>(Exception exception)
        where TA : notnull
    {
        return FailWithMessage<TA>(exception.Message);
    }
}