namespace back.zone.core.Monads.EitherMonad;

public static class EitherSwap
{
    /// <summary>
    ///     Swaps the position of the left and right values in an Either monad.
    ///     If the Either contains a right value, it will be returned as a left value.
    ///     If the Either contains a left value, it will be returned as a right value.
    ///     If the Either does not contain either a left or right value, an InvalidOperationException will be thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value.</typeparam>
    /// <typeparam name="TR">The type of the right value.</typeparam>
    /// <param name="self">The Either monad to swap.</param>
    /// <returns>
    ///     An Either monad with the left and right values swapped.
    ///     If the original Either contained a right value, the returned Either will contain a left value.
    ///     If the original Either contained a left value, the returned Either will contain a right value.
    /// </returns>
    public static Either<TR, TL> Swap<TL, TR>(
        this Either<TL, TR> self
    )
        where TL : notnull
        where TR : notnull
    {
        return self.TryGetRight(out var right)
            ? right
            : self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_value_present#");
    }

    /// <summary>
    ///     Asynchronously swaps the position of the left and right values in an Either monad.
    ///     If the Either contains a right value, it will be returned as a left value.
    ///     If the Either contains a left value, it will be returned as a right value.
    ///     If the Either does not contain either a left or right value, an InvalidOperationException will be thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value.</typeparam>
    /// <typeparam name="TR">The type of the right value.</typeparam>
    /// <param name="self">The Task of Either monad to swap.</param>
    /// <returns>
    ///     An awaitable Task of Either monad with the left and right values swapped.
    ///     If the original Either contained a right value, the returned Either will contain a left value.
    ///     If the original Either contained a left value, the returned Either will contain a right value.
    /// </returns>
    public static async Task<Either<TR, TL>> SwapAsync<TL, TR>(
        this Task<Either<TL, TR>> self
    )
        where TL : notnull
        where TR : notnull
    {
        return (await self.ConfigureAwait(false)).Swap();
    }
}