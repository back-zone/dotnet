namespace back.zone.core.Monads.EitherMonad;

public static class EitherSwap
{
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

    public static async Task<Either<TR, TL>> SwapAsync<TL, TR>(
        this Task<Either<TL, TR>> self
    )
        where TL : notnull
        where TR : notnull
    {
        return (await self.ConfigureAwait(false)).Swap();
    }
}