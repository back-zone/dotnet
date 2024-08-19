using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherZip
{
    public static Either<TL, TR2> ZipWith<TL, TR, TR1, TR2>(
        this Either<TL, TR> self,
        Either<TL, TR1> other,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? other.TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : other.TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Either<TL, TR> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Either<TL, TR> self,
        Either<TL, TR1> other,
        Zipper<TR, TR1, Task<TR2>> zipperAsync
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? other.TryGetRight(out var otherRight)
                    ? await zipperAsync(right, otherRight).ConfigureAwait(false)
                    : other.TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Either<TL, TR> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, Task<TR2>> zipperAsync
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? await zipperAsync(right, otherRight).ConfigureAwait(false)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<Either<TL, TR2>> ZipWith<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR1> other,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? other.TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : other.TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<Either<TL, TR2>> ZipWith<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR1> other,
        Zipper<TR, TR1, Task<TR2>> zipperAsync
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? other.TryGetRight(out var otherRight)
                    ? await zipperAsync(right, otherRight).ConfigureAwait(false)
                    : other.TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, Task<TR2>> zipperAsync
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? await zipperAsync(right, otherRight).ConfigureAwait(false)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    public static Either<TL, TR1> ZipRight<TL, TR, TR1>(
        this Either<TL, TR> self,
        Either<TL, TR1> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return self.ZipWith(other, (_, b) => b);
    }

    public static async Task<Either<TL, TR1>> ZipRightAsync<TL, TR, TR1>(
        this Either<TL, TR> self,
        Task<Either<TL, TR1>> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await self.ZipWithAsync(other, (_, b) => b).ConfigureAwait(false);
    }

    public static async Task<Either<TL, TR1>> ZipRightAsync<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await self.ZipWithAsync(other, (_, b) => b).ConfigureAwait(false);
    }

    public static async Task<Either<TL, TR1>> ZipRight<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR1> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return (await self.ConfigureAwait(false)).ZipWith(other, (_, b) => b);
    }

    public static Either<TL, (TR, TR1)> Zip<TL, TR, TR1>(
        this Either<TL, TR> self,
        Either<TL, TR1> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return self.ZipWith(other, (a, b) => (a, b));
    }

    public static async Task<Either<TL, (TR, TR1)>> ZipAsync<TL, TR, TR1>(
        this Either<TL, TR> self,
        Task<Either<TL, TR1>> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await self.ZipWithAsync(other, (a, b) => (a, b)).ConfigureAwait(false);
    }

    public static async Task<Either<TL, (TR, TR1)>> ZipAsync<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await self.ZipWithAsync(other, (a, b) => (a, b)).ConfigureAwait(false);
    }

    public static async Task<Either<TL, (TR, TR1)>> Zip<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR1> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return (await self.ConfigureAwait(false)).ZipWith(other, (a, b) => (a, b));
    }
}