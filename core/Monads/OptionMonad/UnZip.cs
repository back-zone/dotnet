using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionUnZip
{
    public static Option<TC> UnZip<TA, TB, TC>(
        this Option<(TA, TB)> self,
        OptionalContinuation<(TA, TB), TC> continuation
    )
    {
        try
        {
            return self.TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> UnZipAsync<TA, TB, TC>(
        this Option<(TA, TB)> self,
        OptionalContinuation<(TA, TB), Task<TC>> continuation
    )
    {
        try
        {
            return self.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> UnZipAsync<TA, TB, TC>(
        this Task<Option<(TA, TB)>> selfAsync,
        OptionalContinuation<(TA, TB), Task<TC>> continuation
    )
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> UnZip<TA, TB, TC>(
        this Task<Option<(TA, TB)>> selfAsync,
        OptionalContinuation<(TA, TB), TC> continuation
    )
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }
}