namespace back.zone.core.Monads.TryMonad;

public static class TryOrElse
{
    public static Try<TU> OrElse<TA, TU>(
        this Try<TA> self,
        Try<TU> other
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

    public static async Task<Try<TU>> OrElseAsync<TA, TU>(
        this Try<TA> self,
        Task<Try<TU>> otherAsync
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

    public static async Task<Try<TU>> OrElseAsync<TA, TU>(
        this Task<Try<TA>> selfAsync,
        Task<Try<TU>> otherAsync
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? (TU)value
                : await otherAsync.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TU>> OrElse<TA, TU>(
        this Task<Try<TA>> selfAsync,
        Try<TU> other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? (TU)value
                : other;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}