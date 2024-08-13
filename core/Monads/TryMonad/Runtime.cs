using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

internal static class TryRuntime
{
    public static Try<TB> RunValue<TA, TB>(
        TA value,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(value);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> RunValue<TA, TB>(
        TA value,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(value);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(await asyncValue.ConfigureAwait(false));
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(await asyncValue.ConfigureAwait(false)).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunValue<TA, TB>(
        Task<TA> asyncValue,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(await asyncValue.ConfigureAwait(false));
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(await asyncValue.ConfigureAwait(false)).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> RunEffect<TA, TB>(
        Effect<TA> effect,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(effect());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> RunEffect<TA, TB>(
        Effect<TA> effect,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(effect());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunEffectAsync<TA, TB>(
        Effect<TA> effect,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(effect()).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunEffectAsync<TA, TB>(
        Effect<TA> effect,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(effect()).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> RunTry<TA, TB>(
        Try<TA> current,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return current.TryGetValue(out var value)
                ? continuation(value)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunTryAsync<TA, TB>(
        Try<TA> current,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunTryAsync<TA, TB>(
        Task<Try<TA>> currentAsync,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunTry<TA, TB>(
        Task<Try<TA>> currentAsync,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetValue(out var value)
                ? continuation(value)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> RunTry<TA, TB>(
        Try<TA> current,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return current.TryGetValue(out var value)
                ? continuation(value)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunTryAsync<TA, TB>(
        Try<TA> current,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunTryAsync<TA, TB>(
        Task<Try<TA>> currentAsync,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> RunTry<TA, TB>(
        Task<Try<TA>> currentAsync,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetValue(out var value)
                ? continuation(value)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }
}