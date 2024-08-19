using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryRecover
{
    /// <summary>
    ///     Recovers from a failed Try computation by applying a specified function to the exception.
    /// </summary>
    /// <typeparam name="TA">The type of the Try result.</typeparam>
    /// <param name="self">The Try computation to recover from.</param>
    /// <param name="continuation">A function that takes the exception and returns a new value of type TA.</param>
    /// <returns>
    ///     If the Try computation was successful, returns the original Try computation.
    ///     If the Try computation failed, applies the continuation function to the exception and returns a new Try computation
    ///     with the result.
    ///     If an exception occurs during the recovery process, returns a new Try computation with the caught exception.
    /// </returns>
    public static Try<TA> Recover<TA>(
        this Try<TA> self,
        Continuation<Exception, TA> continuation
    )
        where TA : notnull
    {
        try
        {
            return self.TryGetException(out var exception)
                ? continuation(exception)
                : self;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Asynchronously recovers from a failed Try computation by applying a specified function to the exception.
    /// </summary>
    /// <typeparam name="TA">The type of the Try result.</typeparam>
    /// <param name="self">The Try computation to recover from.</param>
    /// <param name="continuation">
    ///     A function that takes the exception and returns a new value of type <see cref="Task{TA}" />,
    ///     representing the recovered value.
    /// </param>
    /// <returns>
    ///     If the Try computation was successful, returns the original Try computation.
    ///     If the Try computation failed, applies the continuation function to the exception and returns a new Try computation
    ///     with the result.
    ///     If an exception occurs during the recovery process, returns a new Try computation with the caught exception.
    /// </returns>
    public static async Task<Try<TA>> RecoverAsync<TA>(
        this Try<TA> self,
        Continuation<Exception, Task<TA>> continuation
    )
        where TA : notnull
    {
        try
        {
            return self.TryGetException(out var exception)
                ? await continuation(exception).ConfigureAwait(false)
                : self;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Asynchronously recovers from a failed Try computation by applying a specified function to the exception.
    /// </summary>
    /// <typeparam name="TA">The type of the Try result.</typeparam>
    /// <param name="selfAsync">The asynchronous Try computation to recover from.</param>
    /// <param name="continuation">
    ///     A function that takes the exception and returns a new value of type <see cref="Task{TA}" />,
    ///     representing the recovered value.
    /// </param>
    /// <returns>
    ///     If the Try computation was successful, returns the original Try computation.
    ///     If the Try computation failed, applies the continuation function to the exception and returns a new Try computation
    ///     with the result.
    ///     If an exception occurs during the recovery process, returns a new Try computation with the caught exception.
    /// </returns>
    public static async Task<Try<TA>> RecoverAsync<TA>(
        this Task<Try<TA>> selfAsync,
        Continuation<Exception, Task<TA>> continuation
    )
        where TA : notnull
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetException(out var exception)
                ? await continuation(exception).ConfigureAwait(false)
                : await selfAsync;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Asynchronously recovers from a failed Try computation by applying a specified function to the exception.
    /// </summary>
    /// <typeparam name="TA">The type of the Try result.</typeparam>
    /// <param name="selfAsync">The asynchronous Try computation to recover from.</param>
    /// <param name="continuation">
    ///     A function that takes the exception and returns a new value of type <see cref="TA" />,
    ///     representing the recovered value.
    /// </param>
    /// <returns>
    ///     If the Try computation was successful, returns the original Try computation.
    ///     If the Try computation failed, applies the continuation function to the exception and returns a new Try computation
    ///     with the result.
    ///     If an exception occurs during the recovery process, returns a new Try computation with the caught exception.
    /// </returns>
    public static async Task<Try<TA>> Recover<TA>(
        this Task<Try<TA>> selfAsync,
        Continuation<Exception, TA> continuation
    )
        where TA : notnull
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetException(out var exception)
                ? continuation(exception)
                : await selfAsync;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}