namespace back.zone.core.Monads.TryMonad;

public static class TryGetOrElse
{
    public static TU GetOrElse<TA, TU>(
        this Try<TA> self,
        TU other
    )
        where TA : notnull
        where TU : TA
    {
        return self.TryGetValue(out var value)
            ? (TU)value
            : other;
    }

    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Try<TA> self,
        Task<TU> otherAsync
    )
        where TA : notnull
        where TU : TA
    {
        return self.TryGetValue(out var value)
            ? (TU)value
            : await otherAsync.ConfigureAwait(false);
    }

    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Task<Try<TA>> self,
        Task<TU> other
    )
        where TA : notnull
        where TU : TA
    {
        return (await self.ConfigureAwait(false)).TryGetValue(out var value)
            ? (TU)value
            : await other.ConfigureAwait(false);
    }

    public static async Task<TU> GetOrElse<TA, TU>(
        this Task<Try<TA>> self,
        TU other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? (TU)value
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }
}