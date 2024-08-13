using back.zone.monads.Types;

namespace back.zone.monads.zero.TryMonad;

public static class Try
{
    public static Try<TA> Succeed<TA>(TA value)
        where TA : notnull
    {
        return value;
    }

    public static Try<TA> Fail<TA>(Exception exception)
        where TA : notnull
    {
        return exception;
    }

    public static Try<TA> Fail<TA>(string message)
        where TA : notnull
    {
        return new Exception(message);
    }

    public static Try<TA> Effect<TA>(
        Effect<TA> effect
    )
        where TA : notnull
    {
        try
        {
            return effect();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> ProvideAndRun<TA, TB>(
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

    public static Try<TB> ProvideAndRun<TA, TB>(
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

    public static Try<TB> ProvideAndRun<TA, TB>(
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

    public static Try<TB> ProvideAndRun<TA, TB>(
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

    public static Try<TB> ProvideAndRun<TA, TB>(
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

    public static Try<TB> ProvideAndRun<TA, TB>(
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

    public static async Task<Try<TA>> Async<TA>(
        Task<TA> asyncValue
    )
        where TA : notnull
    {
        try
        {
            return await asyncValue.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TA>> Async<TA>(
        Task<Try<TA>> asyncValue
    )
        where TA : notnull
    {
        try
        {
            return await asyncValue.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }
}