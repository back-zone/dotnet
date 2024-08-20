using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherMap
{
    /// <summary>
    ///     Maps a function over the Right value of an Either, if the Either is in a Right state.
    ///     If the Either is in a Left state, the function does not execute and the Left value is returned as is.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value in the Either.</typeparam>
    /// <typeparam name="TR">The type of the Right value in the Either before mapping.</typeparam>
    /// <typeparam name="TR1">The type of the Right value in the Either after mapping.</typeparam>
    /// <param name="self">The Either to map over.</param>
    /// <param name="continuation">The function to apply to the Right value.</param>
    /// <returns>
    ///     An Either with the same Left value if the input Either was in a Left state,
    ///     or an Either with the result of applying the function to the Right value if the input Either was in a Right state.
    /// </returns>
    public static Either<TL, TR1> Map<TL, TR, TR1>(
        this Either<TL, TR> self,
        Continuation<TR, TR1> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return EitherRunTime.RunEither(self, continuation);
    }

    /// <summary>
    ///     Asynchronously maps a function over the Right value of an Either, if the Either is in a Right state.
    ///     If the Either is in a Left state, the function does not execute and the Left value is returned as is.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value in the Either.</typeparam>
    /// <typeparam name="TR">The type of the Right value in the Either before mapping.</typeparam>
    /// <typeparam name="TR1">The type of the Right value in the Either after mapping.</typeparam>
    /// <param name="self">The Either to map over.</param>
    /// <param name="continuation">
    ///     The function to apply to the Right value. This function should return a Task that represents
    ///     the mapped value.
    /// </param>
    /// <returns>
    ///     An asynchronous task that will return an Either with the same Left value if the input Either was in a Left state,
    ///     or an Either with the result of applying the function to the Right value if the input Either was in a Right state.
    /// </returns>
    public static async Task<Either<TL, TR1>> MapAsync<TL, TR, TR1>(
        this Either<TL, TR> self,
        Continuation<TR, Task<TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously maps a function over the Right value of an Either, if the Either is in a Right state.
    ///     If the Either is in a Left state, the function does not execute and the Left value is returned as is.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value in the Either.</typeparam>
    /// <typeparam name="TR">The type of the Right value in the Either before mapping.</typeparam>
    /// <typeparam name="TR1">The type of the Right value in the Either after mapping.</typeparam>
    /// <param name="self">The asynchronous task that contains the Either to map over.</param>
    /// <param name="continuation">
    ///     The function to apply to the Right value. This function should return a Task that represents
    ///     the mapped value.
    /// </param>
    /// <returns>
    ///     An asynchronous task that will return an Either with the same Left value if the input Either was in a Left state,
    ///     or an Either with the result of applying the function to the Right value if the input Either was in a Right state.
    /// </returns>
    public static async Task<Either<TL, TR1>> MapAsync<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Continuation<TR, Task<TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously maps a function over the Right value of an Either, if the Either is in a Right state.
    ///     If the Either is in a Left state, the function does not execute and the Left value is returned as is.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value in the Either.</typeparam>
    /// <typeparam name="TR">The type of the Right value in the Either before mapping.</typeparam>
    /// <typeparam name="TR1">The type of the Right value in the Either after mapping.</typeparam>
    /// <param name="currentAsync">The asynchronous task that contains the Either to map over.</param>
    /// <param name="continuation">
    ///     The function to apply to the Right value. This function should return a Task that represents
    ///     the mapped value.
    /// </param>
    /// <returns>
    ///     An asynchronous task that will return an Either with the same Left value if the input Either was in a Left state,
    ///     or an Either with the result of applying the function to the Right value if the input Either was in a Right state.
    /// </returns>
    public static async Task<Either<TL, TR1>> MapAsync<TL, TR, TR1>(
        this Task<Either<TL, TR>> currentAsync,
        Continuation<TR, TR1> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await EitherRunTime.RunEitherAsync(currentAsync, continuation).ConfigureAwait(false);
    }
}