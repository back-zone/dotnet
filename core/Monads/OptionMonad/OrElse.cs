namespace back.zone.core.Monads.OptionMonad;

public static class OptionOrElse
{
    public static Option<TA> OrElse<TA>(
        this Option<TA> self,
        Option<TA> other
    )
    {
        try
        {
            return self.TryGetValue(out var value)
                ? value
                : other;
        }
        catch (Exception)
        {
            return Option.None<TA>();
        }
    }

    public static async Task<Option<TA>> OrElseAsync<TA>(
        this Option<TA> self,
        Task<Option<TA>> otherAsync
    )
    {
        try
        {
            return self.TryGetValue(out var value)
                ? value
                : await otherAsync.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TA>();
        }
    }

    public static async Task<Option<TA>> OrElseAsync<TA>(
        this Task<Option<TA>> self,
        Task<Option<TA>> other
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? value
                : await other.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TA>();
        }
    }

    public static async Task<Option<TA>> OrElse<TA>(
        this Task<Option<TA>> self,
        Option<TA> other
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? value
                : other;
        }
        catch (Exception)
        {
            return Option.None<TA>();
        }
    }
}