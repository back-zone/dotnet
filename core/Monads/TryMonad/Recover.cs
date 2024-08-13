using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryRecover
{
    public static Try<TA> Recover<TA>(
        this Try<TA> self,
        Continuation<Exception, TA> continuation
    )
        where TA : notnull
    {
        try
        {
            return self.TryGetException(out var exception)
                ? continuation(exception)
                : self;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TA>> RecoverAsync<TA>(
        this Try<TA> self,
        Continuation<Exception, Task<TA>> continuation
    )
        where TA : notnull
    {
        try
        {
            return self.TryGetException(out var exception)
                ? await continuation(exception).ConfigureAwait(false)
                : self;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TA>> RecoverAsync<TA>(
        this Task<Try<TA>> selfAsync,
        Continuation<Exception, Task<TA>> continuation
    )
        where TA : notnull
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetException(out var exception)
                ? await continuation(exception).ConfigureAwait(false)
                : await selfAsync;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TA>> Recover<TA>(
        this Task<Try<TA>> selfAsync,
        Continuation<Exception, TA> continuation
    )
        where TA : notnull
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetException(out var exception)
                ? continuation(exception)
                : await selfAsync;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}