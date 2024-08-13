using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionMap
{
    public static Option<TB> Map<TA, TB>(
        this Option<TA> self,
        OptionalContinuation<TA, TB> continuation
    )
    {
        return OptionRuntime.RunOption(self, continuation);
    }

    public static async Task<Option<TB>> MapAsync<TA, TB>(
        this Option<TA> self,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Option<TB>> MapAsync<TA, TB>(
        this Task<Option<TA>> self,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Option<TB>> Map<TA, TB>(
        this Task<Option<TA>> self,
        OptionalContinuation<TA, TB> continuation
    )
    {
        return await OptionRuntime.RunOption(self, continuation).ConfigureAwait(false);
    }
}