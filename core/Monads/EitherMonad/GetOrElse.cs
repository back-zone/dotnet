namespace back.zone.core.Monads.EitherMonad;

public static class EitherGetOrElse
{
    public static TU GetOrElse<TL, TR, TU>(
        this Either<TL, TR> self,
        TU other
    )
        where TL : notnull
        where TR : notnull
        where TU : TR
    {
        try
        {
            return self.TryGetRight(out var right)
                ? (TU)right
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }

    public static async Task<TU> GetOrElseAsync<TL, TR, TU>(
        this Either<TL, TR> self,
        Task<TU> otherAsync
    )
        where TL : notnull
        where TR : notnull
        where TU : TR
    {
        return self.TryGetRight(out var right)
            ? (TU)right
            : await otherAsync.ConfigureAwait(false);
    }

    public static async Task<TU> GetOrElseAsync<TL, TR, TU>(
        this Task<Either<TL, TR>> selfAsync,
        Task<TU> otherAsync
    )
        where TL : notnull
        where TR : notnull
        where TU : TR
    {
        var current = await selfAsync.ConfigureAwait(false);

        return current.TryGetRight(out var right)
            ? (TU)right
            : await otherAsync.ConfigureAwait(false);
    }

    public static async Task<TU> GetOrElse<TL, TR, TU>(
        this Task<Either<TL, TR>> self,
        TU other
    )
        where TL : notnull
        where TR : notnull
        where TU : TR
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? (TU)right
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }
}