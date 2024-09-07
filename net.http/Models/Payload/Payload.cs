using back.zone.core.Monads.OptionMonad;

namespace back.zone.net.http.Models.Payload;

public sealed record Payload<TA>(
    bool Result,
    string Message,
    TA? Data
);

public static class Payload
{
    private const string SuccessMessage = "#success#";
    private const string FailureMessage = "#failure#";

    /// <summary>
    ///     Creates a new instance of <see cref="Payload{TA}" /> representing a successful operation with no data.
    /// </summary>
    /// <typeparam name="TA">The type of data to be contained in the payload.</typeparam>
    /// <returns>
    ///     A new instance of <see cref="Payload{TA}" /> with <see cref="Payload{TA}.Result" /> set to true,
    ///     <see cref="Payload{TA}.Message" /> set to <see cref="SuccessMessage" />, and <see cref="Payload{TA}.Data" /> set to
    ///     <see cref="Option.None{TA}" />.
    /// </returns>
    public static Payload<TA> Succeed<TA>()
    {
        return new Payload<TA>(true, SuccessMessage, Option.None<TA>());
    }

    /// <summary>
    ///     Creates a new instance of <see cref="Payload{TA}" /> representing a successful operation with a custom message.
    /// </summary>
    /// <typeparam name="TA">The type of data to be contained in the payload.</typeparam>
    /// <param name="message">The custom message to be included in the payload.</param>
    /// <returns>
    ///     A new instance of <see cref="Payload{TA}" /> with <see cref="Payload{TA}.Result" /> set to true,
    ///     <see cref="Payload{TA}.Message" /> set to the provided <paramref name="message" />, and
    ///     <see cref="Payload{TA}.Data" /> set to <see cref="Option.None{TA}" />.
    /// </returns>
    public static Payload<TA> SucceedWithMessage<TA>(string message)
    {
        return new Payload<TA>(true, message, Option.None<TA>());
    }

    /// <summary>
    ///     Creates a new instance of <see cref="Payload{TA}" /> representing a successful operation with provided data.
    /// </summary>
    /// <typeparam name="TA">The type of data to be contained in the payload.</typeparam>
    /// <param name="data">The data to be included in the payload.</param>
    /// <returns>
    ///     A new instance of <see cref="Payload{TA}" /> with <see cref="Payload{TA}.Result" /> set to true,
    ///     <see cref="Payload{TA}.Message" /> set to <see cref="SuccessMessage" />, and <see cref="Payload{TA}.Data" /> set to
    ///     the provided <paramref name="data" />.
    /// </returns>
    public static Payload<TA> SucceedWithData<TA>(TA data)
    {
        return new Payload<TA>(true, SuccessMessage, data);
    }

    /// <summary>
    ///     Creates a new instance of <see cref="Payload{TA}" /> representing a successful operation with a custom message and
    ///     provided data.
    /// </summary>
    /// <typeparam name="TA">The type of data to be contained in the payload.</typeparam>
    /// <param name="message">
    ///     The custom message to be included in the payload. Default value is
    ///     <see cref="Payload.SuccessMessage" />.
    /// </param>
    /// <param name="data">The data to be included in the payload.</param>
    /// <returns>
    ///     A new instance of <see cref="Payload{TA}" /> with <see cref="Payload{TA}.Result" /> set to true,
    ///     <see cref="Payload{TA}.Message" /> set to the provided <paramref name="message" />, and
    ///     <see cref="Payload{TA}.Data" /> set to the provided <paramref name="data" />.
    /// </returns>
    public static Payload<TA> SucceedWithMessageAndData<TA>(string message, TA data)
    {
        return new Payload<TA>(true, message, data);
    }

    /// <summary>
    ///     Creates a new instance of <see cref="Payload{TA}" /> representing a failed operation with no data.
    /// </summary>
    /// <typeparam name="TA">The type of data to be contained in the payload.</typeparam>
    /// <returns>
    ///     A new instance of <see cref="Payload{TA}" /> with <see cref="Payload{TA}.Result" /> set to false,
    ///     <see cref="Payload{TA}.Message" /> set to <see cref="FailureMessage" />, and <see cref="Payload{TA}.Data" /> set to
    ///     <see cref="Option.None{TA}" />.
    /// </returns>
    public static Payload<TA> Fail<TA>()
    {
        return new Payload<TA>(false, FailureMessage, Option.None<TA>());
    }

    /// <summary>
    ///     Creates a new instance of <see cref="Payload{TA}" /> representing a failed operation with a custom message.
    /// </summary>
    /// <typeparam name="TA">The type of data to be contained in the payload.</typeparam>
    /// <param name="message">
    ///     The custom message to be included in the payload. Default value is
    ///     <see cref="Payload.FailureMessage" />.
    /// </param>
    /// <returns>
    ///     A new instance of <see cref="Payload{TA}" /> with <see cref="Payload{TA}.Result" /> set to false,
    ///     <see cref="Payload{TA}.Message" /> set to the provided <paramref name="message" />, and
    ///     <see cref="Payload{TA}.Data" /> set to <see cref="Option.None{TA}" />.
    /// </returns>
    public static Payload<TA> FailWithMessage<TA>(string message = FailureMessage)
    {
        return new Payload<TA>(false, message, Option.None<TA>());
    }

    /// <summary>
    ///     Creates a new instance of <see cref="Payload{TA}" /> representing a failed operation with provided data.
    /// </summary>
    /// <typeparam name="TA">The type of data to be contained in the payload.</typeparam>
    /// <param name="data">The data to be included in the payload.</param>
    /// <returns>
    ///     A new instance of <see cref="Payload{TA}" /> with <see cref="Payload{TA}.Result" /> set to false,
    ///     <see cref="Payload{TA}.Message" /> set to <see cref="Payload.FailureMessage" />, and
    ///     <see cref="Payload{TA}.Data" /> set to the provided <paramref name="data" />.
    /// </returns>
    public static Payload<TA> FailWithData<TA>(TA data)
    {
        return new Payload<TA>(false, FailureMessage, data);
    }

    /// <summary>
    ///     Creates a new instance of <see cref="Payload{TA}" /> representing a failed operation with a custom message and
    ///     provided data.
    /// </summary>
    /// <typeparam name="TA">The type of data to be contained in the payload.</typeparam>
    /// <param name="message">
    ///     The custom message to be included in the payload. Default value is
    ///     <see cref="Payload.FailureMessage" />.
    /// </param>
    /// <param name="data">The data to be included in the payload.</param>
    /// <returns>
    ///     A new instance of <see cref="Payload{TA}" /> with <see cref="Payload{TA}.Result" /> set to false,
    ///     <see cref="Payload{TA}.Message" /> set to the provided <paramref name="message" />, and
    ///     <see cref="Payload{TA}.Data" /> set to the provided <paramref name="data" />.
    /// </returns>
    public static Payload<TA> FailWithMessageAndData<TA>(string message, TA data)
    {
        return new Payload<TA>(false, message, data);
    }

    /// <summary>
    ///     Creates a new instance of <see cref="Payload{TA}" /> representing a failed operation due to an exception.
    /// </summary>
    /// <typeparam name="TA">The type of data to be contained in the payload.</typeparam>
    /// <param name="exception">
    ///     The exception that caused the failure. The message from this exception will be used as the
    ///     custom message in the payload.
    /// </param>
    /// <returns>
    ///     A new instance of <see cref="Payload{TA}" /> with <see cref="Payload{TA}.Result" /> set to false,
    ///     <see cref="Payload{TA}.Message" /> set to the message from the provided <paramref name="exception" />, and
    ///     <see cref="Payload{TA}.Data" /> set to <see cref="Option.None{TA}" />.
    /// </returns>
    public static Payload<TA> FailFromException<TA>(Exception exception)
    {
        return FailWithMessage<TA>(exception.Message);
    }
}