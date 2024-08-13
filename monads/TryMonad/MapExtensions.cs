using back.zone.monads.Types;

namespace back.zone.monads.TryMonad;

public static class MapExtensions
{
    public static async Task<Try<TB>> MapAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, Task<TB>> continuation)
        where TA : notnull
        where TB : notnull
    {
        return await Try.ProvideAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Try<TB>> MapAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, TB> continuation)
        where TA : notnull
        where TB : notnull
    {
        return await Try.ProvideAsync(self, continuation).ConfigureAwait(false);
    }
}