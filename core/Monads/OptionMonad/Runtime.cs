using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

internal static class OptionRuntime
{
    public static Option<TB> RunValue<TA, TB>(
        TA value,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return continuation(value);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static Option<TB> RunValue<TA, TB>(
        TA value,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return continuation(value);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return continuation(await asyncValue.ConfigureAwait(false));
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        try
        {
            return await continuation(await asyncValue.ConfigureAwait(false)).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunValue<TA, TB>(
        Task<TA> asyncValue,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return continuation(await asyncValue.ConfigureAwait(false));
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        try
        {
            return await continuation(await asyncValue.ConfigureAwait(false)).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static Option<TB> RunEffect<TA, TB>(
        OptionalEffect<TA> effect,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return continuation(effect());
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static Option<TB> RunEffect<TA, TB>(
        OptionalEffect<TA> effect,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return continuation(effect());
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunEffectAsync<TA, TB>(
        OptionalEffect<TA> effect,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        try
        {
            return await continuation(effect()).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunEffectAsync<TA, TB>(
        OptionalEffect<TA> effect,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        try
        {
            return await continuation(effect()).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static Option<TB> RunOption<TA, TB>(
        Option<TA> current,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return current.TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static Option<TB> RunOption<TA, TB>(
        Option<TA> current,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return current.TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Option<TA> current,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        try
        {
            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Task<Option<TA>> current,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return (await current.ConfigureAwait(false)).TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Task<Option<TA>> currentAsync,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        try
        {
            return (await currentAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunOption<TA, TB>(
        Task<Option<TA>> currentAsync,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return (await currentAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunOption<TA, TB>(
        Task<Option<TA>> currentAsync,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return (await currentAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Option<TA> current,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        try
        {
            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Task<Option<TA>> currentAsync,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        try
        {
            return (await currentAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }
}