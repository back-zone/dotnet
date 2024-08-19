using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherFold
{
    public static TF Fold<TL, TR, TF>(
        this Either<TL, TR> self,
        Continuation<TL, TF> leftHandler,
        Continuation<TR, TF> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? rightHandler(right)
                : self.TryGetLeft(out var left)
                    ? leftHandler(left)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? leftHandler(left)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Either<TL, TR> self,
        Continuation<TL, Task<TF>> leftHandler,
        Continuation<TR, Task<TF>> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? await rightHandler(right).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? await leftHandler(left).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? await leftHandler(left).ConfigureAwait(false)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Either<TL, TR> self,
        Continuation<TL, Task<TF>> leftHandler,
        Continuation<TR, TF> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? rightHandler(right)
                : self.TryGetLeft(out var left)
                    ? await leftHandler(left).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? await leftHandler(left).ConfigureAwait(false)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Either<TL, TR> self,
        Continuation<TL, TF> leftHandler,
        Continuation<TR, Task<TF>> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? await rightHandler(right).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? leftHandler(left)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? leftHandler(left)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, Task<TF>> leftHandler,
        Continuation<TR, Task<TF>> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? await rightHandler(right).ConfigureAwait(false)
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? await leftHandler(left).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? await leftHandler(left).ConfigureAwait(false)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, Task<TF>> leftHandler,
        Continuation<TR, TF> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? rightHandler(right)
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? await leftHandler(left).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.Result.TryGetLeft(out var left)
                ? await leftHandler(left).ConfigureAwait(false)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, TF> leftHandler,
        Continuation<TR, Task<TF>> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? await rightHandler(right).ConfigureAwait(false)
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? leftHandler(left)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.Result.TryGetLeft(out var left)
                ? leftHandler(left)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<TF> Fold<TL, TR, TF>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, TF> leftHandler,
        Continuation<TR, TF> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? rightHandler(right)
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? leftHandler(left)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.Result.TryGetLeft(out var left)
                ? leftHandler(left)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static TF Fold<TF>(
        this Either<TF, TF> self
    )
        where TF : notnull
    {
        return self.TryGetRight(out var right)
            ? right
            : self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
    }

    public static async Task<TF> FoldAsync<TF>(
        this Task<Either<TF, TF>> self
    )
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).Fold();
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }
}