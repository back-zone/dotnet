using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryMap
{
    /// <summary>
    ///     Maps a Try monad of type TA to a Try monad of type TB using the provided continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the source Try monad.</typeparam>
    /// <typeparam name="TB">The type of the resulting Try monad.</typeparam>
    /// <param name="self">The source Try monad to map.</param>
    /// <param name="continuation">The function to apply to the value inside the Try monad if it's successful.</param>
    /// <returns>
    ///     A new Try monad of type TB containing the result of applying the continuation function,
    ///     or an error if the original Try monad contained an error or if the continuation function throws an exception.
    /// </returns>
    public static Try<TB> Map<TA, TB>(
        this Try<TA> self,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return TryRuntime.RunTry(self, continuation);
    }

    /// <summary>
    ///     Asynchronously maps a Try monad of type TA to a Try monad of type TB using the provided asynchronous continuation
    ///     function.
    /// </summary>
    /// <typeparam name="TA">The type of the source Try monad.</typeparam>
    /// <typeparam name="TB">The type of the resulting Try monad.</typeparam>
    /// <param name="self">The source Try monad to map.</param>
    /// <param name="continuation">The asynchronous function to apply to the value inside the Try monad if it's successful.</param>
    /// <returns>
    ///     A Task representing the asynchronous operation, which resolves to a new Try monad of type TB containing the result
    ///     of applying the continuation function,
    ///     or an error if the original Try monad contained an error or if the continuation function throws an exception.
    /// </returns>
    public static async Task<Try<TB>> MapAsync<TA, TB>(
        this Try<TA> self,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTryAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously maps a Task of Try monad of type TA to a Try monad of type TB using the provided asynchronous
    ///     continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the source Try monad.</typeparam>
    /// <typeparam name="TB">The type of the resulting Try monad.</typeparam>
    /// <param name="self">The Task containing the source Try monad to map.</param>
    /// <param name="continuation">The asynchronous function to apply to the value inside the Try monad if it's successful.</param>
    /// <returns>
    ///     A Task representing the asynchronous operation, which resolves to a new Try monad of type TB containing the result
    ///     of applying the continuation function, or an error if the original Try monad contained an error or if the
    ///     continuation
    ///     function throws an exception.
    /// </returns>
    public static async Task<Try<TB>> MapAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTryAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously maps a Task of Try monad of type TA to a Try monad of type TB using the provided continuation
    ///     function.
    /// </summary>
    /// <typeparam name="TA">The type of the source Try monad.</typeparam>
    /// <typeparam name="TB">The type of the resulting Try monad.</typeparam>
    /// <param name="self">The Task containing the source Try monad to map.</param>
    /// <param name="continuation">The function to apply to the value inside the Try monad if it's successful.</param>
    /// <returns>
    ///     A Task representing the asynchronous operation, which resolves to a new Try monad of type TB containing the result
    ///     of applying the continuation function, or an error if the original Try monad contained an error or if the
    ///     continuation
    ///     function throws an exception.
    /// </returns>
    public static async Task<Try<TB>> Map<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTry(self, continuation).ConfigureAwait(false);
    }
}