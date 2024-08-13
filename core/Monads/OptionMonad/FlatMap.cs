using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionFlatMap
{
    public static Option<TB> FlatMap<TA, TB>(
        this Option<TA> self,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        return OptionRuntime.RunOption(self, continuation);
    }

    public static async Task<Option<TB>> FlatMapAsync<TA, TB>(
        this Option<TA> self,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Option<TB>> FlatMapAsync<TA, TB>(
        this Task<Option<TA>> self,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Option<TB>> FlatMap<TA, TB>(
        this Task<Option<TA>> self,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }
}