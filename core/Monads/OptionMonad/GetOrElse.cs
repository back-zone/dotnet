namespace back.zone.core.Monads.OptionMonad;

public static class OptionGetOrElse
{
    public static TU GetOrElse<TA, TU>(
        this Option<TA> self,
        TU other
    )
        where TU : TA
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (TU)value
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }

    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Option<TA> self,
        Task<TU> otherAsync
    )
        where TU : TA
    {
        return self.TryGetValue(out var value)
            ? (TU)value
            : await otherAsync.ConfigureAwait(false);
    }

    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Task<Option<TA>> selfAsync,
        Task<TU> otherAsync
    )
        where TU : TA
    {
        var current = await selfAsync.ConfigureAwait(false);

        return current.TryGetValue(out var value)
            ? (TU)value
            : await otherAsync.ConfigureAwait(false);
    }

    public static async Task<TU> GetOrElse<TA, TU>(
        this Task<Option<TA>> self,
        TU other
    )
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