using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryMap
{
    public static Try<TB> Map<TA, TB>(
        this Try<TA> self,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return TryRuntime.RunTry(self, continuation);
    }

    public static async Task<Try<TB>> MapAsync<TA, TB>(
        this Try<TA> self,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTryAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Try<TB>> MapAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTryAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Try<TB>> Map<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTry(self, continuation).ConfigureAwait(false);
    }
}