namespace back.zone.core.Monads.TryMonad;

public static class TryGetOrElse
{
    public static Try<TU> GetOrElse<TA, TU>(
        this Try<TA> self,
        TU other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (TU)value
                : other;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TU>> GetOrElseAsync<TA, TU>(
        this Try<TA> self,
        Task<TU> otherAsync
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (TU)value
                : await otherAsync.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TU>> GetOrElseAsync<TA, TU>(
        this Task<Try<TA>> self,
        Task<TU> other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? (TU)value
                : await other.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TU>> GetOrElse<TA, TU>(
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
        catch (Exception e)
        {
            return e;
        }
    }
}