using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class Option
{
    public static Option<TA> Some<TA>(TA value)
    {
        return value;
    }

    public static Option<TA> None<TA>()
    {
        return default;
    }

    public static Option<TA> Effect<TA>(OptionalEffect<TA> effect)
    {
        try
        {
            return effect();
        }
        catch (Exception)
        {
            return None<TA>();
        }
    }

    public static async Task<Option<TA>> AsyncValue<TA>(ValueTask<TA> asyncTask)
    {
        try
        {
            return await asyncTask.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return None<TA>();
        }
    }

    public static async Task<Option<TA>> Async<TA>(
        OptionalEffect<Task<TA>> asyncEffect
    )
    {
        try
        {
            return await asyncEffect().ConfigureAwait(false);
        }
        catch (Exception)
        {
            return None<TA>();
        }
    }

    public static async Task<Option<TA>> Async<TA>(Task<Option<TA>> asyncTask)
    {
        try
        {
            return (await asyncTask.ConfigureAwait(false)).TryGetValue(out var value)
                ? value
                : None<TA>();
        }
        catch (Exception)
        {
            return None<TA>();
        }
    }
}