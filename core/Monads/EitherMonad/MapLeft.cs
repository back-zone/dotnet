using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherMapLeft
{
    /// <summary>
    ///     Maps a function over the left side of an Either monad.
    /// </summary>
    /// <typeparam name="TL">The original left type.</typeparam>
    /// <typeparam name="TL1">The new left type.</typeparam>
    /// <typeparam name="TR">The right type.</typeparam>
    /// <param name="self">The Either monad to map over.</param>
    /// <param name="continuation">A function to apply to the left value if it exists.</param>
    /// <returns>A new Either monad with the transformed left value or the original right value.</returns>
    /// <remarks>
    ///     This function is used to apply a function to the left side of an Either monad. If the Either monad contains a left
    ///     value,
    ///     the function is applied to it, and a new Either monad with the transformed left value is returned. If the Either
    ///     monad
    ///     contains a right value, the original Either monad is returned unchanged.
    /// </remarks>
    public static Either<TL1, TR> MapLeft<TL, TL1, TR>(
        this Either<TL, TR> self,
        Continuation<TL, TL1> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return EitherRunTime.RunEither(self, continuation);
    }

    /// <summary>
    ///     Asynchronously maps a function over the left side of an Either monad.
    /// </summary>
    /// <typeparam name="TL">The original left type.</typeparam>
    /// <typeparam name="TL1">The new left type.</typeparam>
    /// <typeparam name="TR">The right type.</typeparam>
    /// <param name="self">The Either monad to map over.</param>
    /// <param name="continuation">
    ///     A function to apply to the left value if it exists. This function should return a Task that represents the new left
    ///     value.
    /// </param>
    /// <returns>
    ///     A new Task that represents an Either monad with the transformed left value or the original right value.
    /// </returns>
    /// <remarks>
    ///     This function is used to apply a function to the left side of an Either monad asynchronously. If the Either monad
    ///     contains a left
    ///     value,
    ///     the function is applied to it, and a new Either monad with the transformed left value is returned. If the Either
    ///     monad
    ///     contains a right value, the original Either monad is returned unchanged.
    /// </remarks>
    public static async Task<Either<TL1, TR>> MapLeftAsync<TL, TL1, TR>(
        this Either<TL, TR> self,
        Continuation<TL, Task<TL1>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously maps a function over the left side of an Either monad.
    /// </summary>
    /// <typeparam name="TL">The original left type.</typeparam>
    /// <typeparam name="TL1">The new left type.</typeparam>
    /// <typeparam name="TR">The right type.</typeparam>
    /// <param name="self">The Task that represents an Either monad to map over.</param>
    /// <param name="continuation">
    ///     A function to apply to the left value if it exists. This function should return a Task that represents the new left
    ///     value.
    /// </param>
    /// <returns>
    ///     A new Task that represents an Either monad with the transformed left value or the original right value.
    /// </returns>
    /// <remarks>
    ///     This function is used to apply a function to the left side of an Either monad asynchronously. If the Either monad
    ///     contains a left value, the function is applied to it, and a new Either monad with the transformed left value is
    ///     returned.
    ///     If the Either monad contains a right value, the original Either monad is returned unchanged.
    /// </remarks>
    public static async Task<Either<TL1, TR>> MapLeftAsync<TL, TL1, TR>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, Task<TL1>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously maps a function over the left side of an Either monad.
    /// </summary>
    /// <typeparam name="TL">The original left type.</typeparam>
    /// <typeparam name="TL1">The new left type.</typeparam>
    /// <typeparam name="TR">The right type.</typeparam>
    /// <param name="self">The Task that represents an Either monad to map over.</param>
    /// <param name="continuation">
    ///     A function to apply to the left value if it exists. This function should return a new left value.
    /// </param>
    /// <returns>
    ///     A new Task that represents an Either monad with the transformed left value or the original right value.
    /// </returns>
    /// <remarks>
    ///     This function is used to apply a function to the left side of an Either monad asynchronously. If the Either monad
    ///     contains a left value, the function is applied to it, and a new Either monad with the transformed left value is
    ///     returned. If the Either monad contains a right value, the original Either monad is returned unchanged.
    /// </remarks>
    public static async Task<Either<TL1, TR>> MapLeftAsync<TL, TL1, TR>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, TL1> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }
}