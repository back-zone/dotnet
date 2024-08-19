namespace back.zone.core.Monads.EitherMonad;

public static class EitherOrElse
{
    public static Either<TL, TR> OrElse<TL, TR>(
        this Either<TL, TR> self,
        Either<TL, TR> other
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? right
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }

    public static async Task<Either<TL, TR>> OrElseAsync<TL, TR>(
        this Either<TL, TR> self,
        Task<Either<TL, TR>> otherAsync
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? right
                : await otherAsync.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return await otherAsync.ConfigureAwait(false);
        }
    }

    public static async Task<Either<TL, TR>> OrElseAsync<TL, TR>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR>> other
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? right
                : await other.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return await other.ConfigureAwait(false);
        }
    }

    public static async Task<Either<TL, TR>> OrElse<TL, TR>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR> other
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? right
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }
}