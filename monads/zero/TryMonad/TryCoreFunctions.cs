using back.zone.monads.Types;

namespace back.zone.monads.zero.TryMonad;

public static class TryX
{
    public static Try<TB> Map<TA, TB>(
        this Try<TA> current,
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
                    : new Exception("#try_monad_internal_map_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> FlatMap<TA, TB>(
        this Try<TA> current,
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
                    : new Exception("#try_monad_internal_flat_map_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> MapPR<TA, TB>(
        this Try<TA> current,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return Try.ProvideAndRun(current, continuation);
    }

    public static Try<TB> MapFMBased<TA, TB>(
        this Try<TA> current,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        return current.FlatMap(a => Try.Succeed(continuation(a)));
    }

    public static async Task<Try<TB>> Map<TA, TB>(
        this Task<Try<TA>> currentTask,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return (await currentTask.ConfigureAwait(false)).Map(continuation);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> MapAsync<TA, TB>(
        this Task<Try<TA>> currentAsync,
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
                    : new Exception("#try_monad_internal_map_async_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }


    public static async Task<TU> GetOrElse<TA, TU>(
        this Task<Try<TA>> current,
        TU alt
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            var result = await current.ConfigureAwait(false);
            return result.TryGetValue(out var value)
                ? (TU)value
                : alt;
        }
        catch (Exception)
        {
            return alt;
        }
    }

    public static TU GetOrElse<TA, TU>(
        this Try<TA> current,
        TU alt
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return current.TryGetValue(out var value)
                ? (TU)value
                : alt;
        }
        catch (Exception)
        {
            return alt;
        }
    }
}