using back.zone.monads.Types;

namespace back.zone.monads.TryMonad;

public static class FlatMapExtensions
{
    public static async Task<Try<TB>> FlatMapAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, Task<Try<TB>>> continuation)
        where TA : notnull
        where TB : notnull
    {
        return await Try.ProvideAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Try<TB>> FlatMapAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<TA, Try<TB>> continuation)
        where TA : notnull
        where TB : notnull
    {
        return await Try.ProvideAsync(self, continuation).ConfigureAwait(false);
    }
}