using back.zone.core.Monads.TryMonad;
using back.zone.net.http.Models.Payload;

namespace back.zone.net.http.Extensions;

public static class PayloadExtensions
{
    /// <summary>
    ///     Converts a Try monad to a Payload.
    /// </summary>
    /// <typeparam name="TA">The type of the data contained in the Try monad.</typeparam>
    /// <param name="tryA">The Try monad to convert.</param>
    /// <returns>A Payload containing the data from the Try monad if successful, or an error Payload if the Try monad failed.</returns>
    public static Payload<TA> ToPayload<TA>(
        this Try<TA> tryA
    )
        where TA : notnull
    {
        return tryA.Fold(
            Payload.FailFromException<TA>,
            Payload.SucceedWithData
        );
    }

    /// <summary>
    ///     Converts an asynchronous Try monad to a Payload.
    /// </summary>
    /// <typeparam name="TA">The type of the data contained in the Try monad.</typeparam>
    /// <param name="tryAsync">The asynchronous Try monad to convert.</param>
    /// <returns>
    ///     An asynchronous Payload containing the data from the Try monad if successful, or an error Payload if the Try
    ///     monad failed.
    /// </returns>
    public static async Task<Payload<TA>> ToPayloadAsync<TA>(
        this Task<Try<TA>> tryAsync
    )
        where TA : notnull
    {
        return (await tryAsync.ConfigureAwait(false)).ToPayload();
    }
}