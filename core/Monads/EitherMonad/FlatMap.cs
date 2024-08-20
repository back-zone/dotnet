using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherFlatMap
{
    /// <summary>
    ///     Applies a function to the value inside an <see cref="Either{TL, TR}" />, allowing for chaining of computations.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either.</typeparam>
    /// <typeparam name="TR1">The type of the result of the continuation function.</typeparam>
    /// <param name="self">The Either to apply the function to.</param>
    /// <param name="continuation">A function that takes the right side of the Either and returns a new Either.</param>
    /// <returns>A new Either with the result of applying the continuation function.</returns>
    public static Either<TL, TR1> FlatMap<TL, TR, TR1>(
        this Either<TL, TR> self,
        Continuation<TR, Either<TL, TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return EitherRunTime.RunEither(self, continuation);
    }

    /// <summary>
    ///     Applies a function to the value inside an <see cref="Either{TL, TR}" />, allowing for chaining of computations.
    ///     This method is specifically designed for asynchronous operations and returns a new <see cref="Either{TL, TR1}" />
    ///     with the result of applying the continuation function.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either.</typeparam>
    /// <typeparam name="TR1">The type of the result of the continuation function.</typeparam>
    /// <param name="self">The Either to apply the function to.</param>
    /// <param name="continuation">
    ///     A function that takes the right side of the Either and returns a new Either wrapped in a Task.
    ///     This function will be executed asynchronously.
    /// </param>
    /// <returns>
    ///     A new Either with the result of applying the continuation function.
    ///     The returned Either will be awaited and its result will be returned from this method.
    /// </returns>
    public static async Task<Either<TL, TR1>> FlatMapAsync<TL, TR, TR1>(
        this Either<TL, TR> self,
        Continuation<TR, Task<Either<TL, TR1>>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Applies a function to the value inside an <see cref="Either{TL, TR}" />, allowing for chaining of
    ///     asynchronous computations.
    ///     This method is specifically designed for asynchronous operations and returns a new
    ///     <see cref="Either{TL, TR1}" />
    ///     with the result of applying the continuation function.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either.</typeparam>
    /// <typeparam name="TR1">The type of the result of the continuation function.</typeparam>
    /// <param name="self">The Task containing the Either to apply the function to.</param>
    /// <param name="continuation">
    ///     A function that takes the right side of the Either and returns a new Either wrapped in a Task.
    ///     This function will be executed asynchronously.
    /// </param>
    /// <returns>
    ///     A new Task containing the Either with the result of applying the continuation function.
    ///     The returned Either will be awaited and its result will be returned from this method.
    /// </returns>
    public static async Task<Either<TL, TR1>> FlatMapAsync<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Continuation<TR, Task<Either<TL, TR1>>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Applies a function to the value inside an <see cref="Either{TL, TR}" />, allowing for chaining of
    ///     asynchronous computations.
    ///     This method is specifically designed for asynchronous operations and returns a new <see cref="Either{TL, TR1}" />
    ///     with the result of applying the continuation function.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either.</typeparam>
    /// <typeparam name="TR1">The type of the result of the continuation function.</typeparam>
    /// <param name="self">The Task containing the Either to apply the function to.</param>
    /// <param name="continuation">
    ///     A function that takes the right side of the Either and returns a new Either wrapped in a Task.
    ///     This function will be executed asynchronously.
    /// </param>
    /// <returns>
    ///     A new Task containing the Either with the result of applying the continuation function.
    ///     The returned Either will be awaited and its result will be returned from this method.
    /// </returns>
    public static async Task<Either<TL, TR1>> FlatMap<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Continuation<TR, Either<TL, TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }
}