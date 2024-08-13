using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryFlatMap
{
    public static Try<TB> FlatMap<TA, TB>(
        this Try<TA> self,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return TryRuntime.RunTry(self, continuation);
    }

    public static async Task<Try<TB>> FlatMapAsync<TA, TB>(
        this Try<TA> self,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTryAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Try<TB>> FlatMapAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTryAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Try<TB>> FlatMap<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return await TryRuntime.RunTry(self, continuation).ConfigureAwait(false);
    }
}