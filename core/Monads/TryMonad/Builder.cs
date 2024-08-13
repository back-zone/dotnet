using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class Try
{
    public static Try<TA> Succeed<TA>(
        TA value
    )
        where TA : notnull
    {
        return value;
    }

    public static Try<TA> Fail<TA>(
        Exception exception
    )
        where TA : notnull
    {
        return exception;
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

    public static async Task<Try<TA>> AsyncValue<TA>(
        ValueTask<TA> asyncTask
    )
        where TA : notnull
    {
        try
        {
            return await asyncTask.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TA>> Async<TA>(
        Effect<Task<TA>> asyncEffect
    )
    {
        try
        {
            return await asyncEffect().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TA>> Async<TA>(
        Task<Try<TA>> asyncTry
    )
        where TA : notnull
    {
        try
        {
            var current = await asyncTry.ConfigureAwait(false);

            return current.TryGetException(out var ex)
                ? ex
                : current;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}