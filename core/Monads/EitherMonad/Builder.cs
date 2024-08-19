using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class Either
{
    public static Either<TL, TR> Right<TL, TR>(TR right)
        where TL : notnull
        where TR : notnull
    {
        return new Either<TL, TR>(right);
    }

    public static Either<TL, TR> Left<TL, TR>(TL left)
        where TL : notnull
        where TR : notnull
    {
        return new Either<TL, TR>(left);
    }

    public static Either<Exception, TR> Effect<TR>(
        Effect<TR> effect
    )
        where TR : notnull
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

    public static async Task<Either<Exception, TR>> Async<TR>(
        ValueTask<TR> asyncTask
    )
        where TR : notnull
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

    public static async Task<Either<Exception, TR>> Async<TR>(
        Effect<Task<TR>> asyncEffect
    )
        where TR : notnull
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

    public static async Task<Either<TL, TR>> Async<TL, TR>(
        Task<Either<TL, TR>> asyncEither
    )
        where TL : Exception
        where TR : notnull
    {
        try
        {
            var current = await asyncEither.ConfigureAwait(false);

            return current.TryGetRight(out var right)
                ? right
                : current.TryGetLeft(out var left)
                    ? left
                    : (TL)new Exception("#empty_lef_and_right#");
        }
        catch (Exception e)
        {
            return (TL)e;
        }
    }
}