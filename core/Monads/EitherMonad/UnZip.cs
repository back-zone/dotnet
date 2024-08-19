using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherUnZip
{
    public static Either<TL, TR2> UnZip<TL, TR, TR1, TR2>(
        this Either<TL, (TR, TR1)> self,
        Continuation<(TR, TR1), TR2> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? continuation(right)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    public static async Task<Either<TL, TR2>> UnZipAsync<TL, TR, TR1, TR2>(
        this Either<TL, (TR, TR1)> self,
        Continuation<(TR, TR1), Task<TR2>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? await continuation(right).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    public static async Task<Either<TL, TR2>> UnZipAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, (TR, TR1)>> selfAsync,
        Continuation<(TR, TR1), Task<TR2>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetRight(out var right)
                ? await continuation(right).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    public static async Task<Either<TL, TR2>> UnZip<TL, TR, TR1, TR2>(
        this Task<Either<TL, (TR, TR1)>> selfAsync,
        Continuation<(TR, TR1), TR2> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetRight(out var right)
                ? continuation(right)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    public static Either<TL, TR2> UnZip<TL, TR, TR1, TR2>(
        this Either<TL, (TR, TR1)> self,
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
                ? zipper(right.Item1, right.Item2)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    public static async Task<Either<TL, TR2>> UnZipAsync<TL, TR, TR1, TR2>(
        this Either<TL, (TR, TR1)> self,
        Zipper<TR, TR1, Task<TR2>> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? await zipper(right.Item1, right.Item2).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    public static async Task<Either<TL, TR2>> UnZipAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, (TR, TR1)>> selfAsync,
        Zipper<TR, TR1, Task<TR2>> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetRight(out var right)
                ? await zipper(right.Item1, right.Item2).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    public static async Task<Either<TL, TR2>> UnZip<TL, TR, TR1, TR2>(
        this Task<Either<TL, (TR, TR1)>> selfAsync,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetRight(out var right)
                ? zipper(right.Item1, right.Item2)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }
}