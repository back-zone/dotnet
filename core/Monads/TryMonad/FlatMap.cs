using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryFlatMap
{
    /// <summary>
    ///     Applies a continuation function to the result of a Try monad, flattening the resulting nested Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the input Try monad.</typeparam>
    /// <typeparam name="TB">The type of the value in the resulting Try monad.</typeparam>
    /// <param name="self">The input Try monad to operate on.</param>
    /// <param name="continuation">A function that takes a value of type TA and returns a new Try monad of type TB.</param>
    /// <returns>A new Try monad of type TB, representing the flattened result of applying the continuation to the input Try.</returns>
    public static Try<TB> FlatMap<TA, TB>(
        this Try<TA> self,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return TryRuntime.RunTry(self, continuation);
    }

    /// <summary>
    ///     Asynchronously applies a continuation function to the result of a Try monad, flattening the resulting nested Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the input Try monad.</typeparam>
    /// <typeparam name="TB">The type of the value in the resulting Try monad.</typeparam>
    /// <param name="self">The input Try monad to operate on.</param>
    /// <param name="continuation">
    ///     An asynchronous function that takes a value of type TA and returns a Task containing a new
    ///     Try monad of type TB.
    /// </param>
    /// <returns>
    ///     A Task containing a new Try monad of type TB, representing the flattened result of applying the continuation
    ///     to the input Try.
    /// </returns>
    public static async Task<Try<TB>> FlatMapAsync<TA, TB>(
        this Try<TA> self,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTryAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously applies a continuation function to the result of a Task-wrapped Try monad, flattening the resulting
    ///     nested Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the input Try monad.</typeparam>
    /// <typeparam name="TB">The type of the value in the resulting Try monad.</typeparam>
    /// <param name="self">The Task containing the input Try monad to operate on.</param>
    /// <param name="continuation">
    ///     An asynchronous function that takes a value of type TA and returns a Task containing a new Try monad of type TB.
    /// </param>
    /// <returns>
    ///     A Task containing a new Try monad of type TB, representing the flattened result of applying the continuation
    ///     to the input Try monad.
    /// </returns>
    public static async Task<Try<TB>> FlatMapAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTryAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously applies a continuation function to the result of a Task-wrapped Try monad, flattening the resulting
    ///     nested Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the input Try monad.</typeparam>
    /// <typeparam name="TB">The type of the value in the resulting Try monad.</typeparam>
    /// <param name="self">The Task containing the input Try monad to operate on.</param>
    /// <param name="continuation">A function that takes a value of type TA and returns a new Try monad of type TB.</param>
    /// <returns>
    ///     A Task containing a new Try monad of type TB, representing the flattened result of applying the continuation
    ///     to the input Try monad.
    /// </returns>
    public static async Task<Try<TB>> FlatMap<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTry(self, continuation).ConfigureAwait(false);
    }
}