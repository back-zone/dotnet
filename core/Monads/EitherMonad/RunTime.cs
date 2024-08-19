using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherRunTime
{
    public static Either<Exception, TR1> RunValue<TR, TR1>(
        TR value,
        Continuation<TR, TR1> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static Either<Exception, TR1> RunValue<TR, TR1>(
        TR value,
        Continuation<TR, Either<Exception, TR1>> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static async Task<Either<Exception, TR1>> RunAsyncValue<TR, TR1>(
        Task<TR> asyncValue,
        Continuation<TR, TR1> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static async Task<Either<Exception, TR1>> RunAsyncValue<TR, TR1>(
        Task<TR> asyncValue,
        Continuation<TR, Task<TR1>> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static async Task<Either<Exception, TR1>> RunValue<TR, TR1>(
        Task<TR> asyncValue,
        Continuation<TR, Either<Exception, TR1>> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static async Task<Either<Exception, TR1>> RunAsyncValue<TR, TR1>(
        Task<TR> asyncValue,
        Continuation<TR, Task<Either<Exception, TR1>>> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static Either<Exception, TR1> RunEffect<TR, TR1>(
        Effect<TR> effect,
        Continuation<TR, TR1> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static Either<Exception, TR1> RunEffect<TR, TR1>(
        Effect<TR> effect,
        Continuation<TR, Either<Exception, TR1>> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static async Task<Either<Exception, TR1>> RunEffectAsync<TR, TR1>(
        Effect<TR> effect,
        Continuation<TR, Task<TR1>> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static async Task<Either<Exception, TR1>> RunEffectAsync<TR, TR1>(
        Effect<TR> effect,
        Continuation<TR, Task<Either<Exception, TR1>>> continuation
    )
        where TR : notnull
        where TR1 : notnull
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

    public static Either<TL, TR1> RunEither<TL, TR, TR1>(
        Either<TL, TR> current,
        Continuation<TR, TR1> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        try
        {
            return current.TryGetRight(out var right)
                ? continuation(right)
                : current.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return current.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL, TR1>> RunEitherAsync<TL, TR, TR1>(
        Either<TL, TR> current,
        Continuation<TR, Task<TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        try
        {
            return current.TryGetRight(out var right)
                ? await continuation(right).ConfigureAwait(false)
                : current.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return current.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static Either<TL1, TR> RunEither<TL, TL1, TR>(
        Either<TL, TR> current,
        Continuation<TL, TL1> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        try
        {
            return current.TryGetLeft(out var left)
                ? continuation(left)
                : current.TryGetRight(out var right)
                    ? right
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return current.TryGetRight(out var right)
                ? right
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL1, TR>> RunEitherAsync<TL, TL1, TR>(
        Either<TL, TR> current,
        Continuation<TL, Task<TL1>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        try
        {
            return current.TryGetLeft(out var left)
                ? await continuation(left).ConfigureAwait(false)
                : current.TryGetRight(out var right)
                    ? right
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return current.TryGetRight(out var right)
                ? right
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL1, TR>> RunEitherAsync<TL, TL1, TR>(
        Task<Either<TL, TR>> currentAsync,
        Continuation<TL, Task<TL1>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetLeft(out var left)
                ? await continuation(left).ConfigureAwait(false)
                : current.TryGetRight(out var right)
                    ? right
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return (await currentAsync).TryGetRight(out var right)
                ? right
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL1, TR>> RunEitherAsync<TL, TL1, TR>(
        Task<Either<TL, TR>> currentAsync,
        Continuation<TL, TL1> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetLeft(out var left)
                ? continuation(left)
                : current.TryGetRight(out var right)
                    ? right
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return (await currentAsync).TryGetRight(out var right)
                ? right
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }


    public static Either<TL1, TR> RunEither<TL, TL1, TR>(
        Either<TL, TR> current,
        Continuation<TL, Either<TL1, TR>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        try
        {
            return current.TryGetLeft(out var left)
                ? continuation(left)
                : current.TryGetRight(out var right)
                    ? right
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return current.TryGetRight(out var right)
                ? right
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL1, TR>> RunEitherAsync<TL, TL1, TR>(
        Either<TL, TR> current,
        Continuation<TL, Task<Either<TL1, TR>>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        try
        {
            return current.TryGetLeft(out var left)
                ? await continuation(left).ConfigureAwait(false)
                : current.TryGetRight(out var right)
                    ? right
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return current.TryGetRight(out var right)
                ? right
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL1, TR>> RunEitherAsync<TL, TL1, TR>(
        Task<Either<TL, TR>> currentAsync,
        Continuation<TL, Task<Either<TL1, TR>>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetLeft(out var left)
                ? await continuation(left).ConfigureAwait(false)
                : current.TryGetRight(out var right)
                    ? right
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return (await currentAsync).TryGetRight(out var right)
                ? right
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL1, TR>> RunEither<TL, TL1, TR>(
        Task<Either<TL, TR>> currentAsync,
        Continuation<TL, Either<TL1, TR>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetLeft(out var left)
                ? continuation(left)
                : current.TryGetRight(out var right)
                    ? right
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return (await currentAsync).TryGetRight(out var right)
                ? right
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static Either<TL, TR1> RunEither<TL, TR, TR1>(
        Either<TL, TR> current,
        Continuation<TR, Either<TL, TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        try
        {
            return current.TryGetRight(out var right)
                ? continuation(right)
                : current.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return current.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL, TR1>> RunEitherAsync<TL, TR, TR1>(
        Task<Either<TL, TR>> asyncCurrent,
        Continuation<TR, TR1> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        try
        {
            var current = await asyncCurrent.ConfigureAwait(false);

            return current.TryGetRight(out var right)
                ? continuation(right)
                : current.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return (await asyncCurrent.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL, TR1>> RunEitherAsync<TL, TR, TR1>(
        Task<Either<TL, TR>> asyncCurrent,
        Continuation<TR, Either<TL, TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        try
        {
            var current = await asyncCurrent.ConfigureAwait(false);

            return current.TryGetRight(out var right)
                ? continuation(right)
                : current.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return (await asyncCurrent.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL, TR1>> RunEitherAsync<TL, TR, TR1>(
        Task<Either<TL, TR>> asyncCurrent,
        Continuation<TR, Task<TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        try
        {
            var current = await asyncCurrent.ConfigureAwait(false);

            return current.TryGetRight(out var right)
                ? await continuation(right).ConfigureAwait(false)
                : current.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return (await asyncCurrent.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL, TR1>> RunEitherAsync<TL, TR, TR1>(
        Task<Either<TL, TR>> asyncCurrent,
        Continuation<TR, Task<Either<TL, TR1>>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        try
        {
            var current = await asyncCurrent.ConfigureAwait(false);

            return current.TryGetRight(out var right)
                ? await continuation(right).ConfigureAwait(false)
                : current.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return (await asyncCurrent.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }

    public static async Task<Either<TL, TR1>> RunEitherAsync<TL, TR, TR1>(
        Either<TL, TR> current,
        Continuation<TR, Task<Either<TL, TR1>>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        try
        {
            return current.TryGetRight(out var right)
                ? await continuation(right).ConfigureAwait(false)
                : current.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_no_right#");
        }
        catch (Exception)
        {
            return current.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_no_right#");
        }
    }
}