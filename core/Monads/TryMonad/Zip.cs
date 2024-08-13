using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryZip
{
    public static Try<TC> ZipWith<TA, TB, TC>(
        this Try<TA> self,
        Try<TB> other,
        Zipper<TA, TB, TC> zipper)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? other.TryGetValue(out var otherValue)
                    ? zipper(value, otherValue)
                    : other.TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Try<TA> self,
        Task<Try<TB>> otherAsync,
        Zipper<TA, TB, TC> zipper)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (await otherAsync).TryGetValue(out var otherValue)
                    ? zipper(value, otherValue)
                    : (await otherAsync).TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Try<TA> self,
        Try<TB> other,
        Zipper<TA, TB, Task<TC>> zipperAsync)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? other.TryGetValue(out var otherValue)
                    ? await zipperAsync(value, otherValue).ConfigureAwait(false)
                    : other.TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Try<TA> self,
        Task<Try<TB>> otherAsync,
        Zipper<TA, TB, Task<TC>> zipperAsync)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (await otherAsync).TryGetValue(out var otherValue)
                    ? await zipperAsync(value, otherValue).ConfigureAwait(false)
                    : (await otherAsync).TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> ZipWith<TA, TB, TC>(
        this Task<Try<TA>> self,
        Try<TB> other,
        Zipper<TA, TB, TC> zipper)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? other.TryGetValue(out var otherValue)
                    ? zipper(value, otherValue)
                    : other.TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : (await self.ConfigureAwait(false)).TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Try<TA>> self,
        Task<Try<TB>> otherAsync,
        Zipper<TA, TB, TC> zipper)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? (await otherAsync).TryGetValue(out var otherValue)
                    ? zipper(value, otherValue)
                    : (await otherAsync).TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : (await self.ConfigureAwait(false)).TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Try<TA>> self,
        Try<TB> other,
        Zipper<TA, TB, Task<TC>> zipperAsync)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? other.TryGetValue(out var otherValue)
                    ? await zipperAsync(value, otherValue).ConfigureAwait(false)
                    : other.TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : (await self.ConfigureAwait(false)).TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Try<TA>> self,
        Task<Try<TB>> otherAsync,
        Zipper<TA, TB, Task<TC>> zipperAsync)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? (await otherAsync).TryGetValue(out var otherValue)
                    ? await zipperAsync(value, otherValue).ConfigureAwait(false)
                    : (await otherAsync).TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : (await self.ConfigureAwait(false)).TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> ZipRight<TA, TB>(
        this Try<TA> self,
        Try<TB> other
    )
        where TA : notnull
        where TB : notnull
    {
        return self.ZipWith(other, (_, b) => b);
    }

    public static async Task<Try<TB>> ZipRightAsync<TA, TB>(
        this Try<TA> self,
        Task<Try<TB>> otherAsync
    )
        where TA : notnull
        where TB : notnull
    {
        return await self.ZipWithAsync(otherAsync, (_, b) => b).ConfigureAwait(false);
    }

    public static async Task<Try<TB>> ZipRightAsync<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Task<Try<TB>> otherAsync
    )
        where TA : notnull
        where TB : notnull
    {
        return await selfAsync.ZipWithAsync(otherAsync, (_, b) => b).ConfigureAwait(false);
    }

    public static async Task<Try<TB>> ZipRight<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Try<TB> other
    )
        where TA : notnull
        where TB : notnull
    {
        return await selfAsync.ZipWith(other, (_, b) => b).ConfigureAwait(false);
    }

    public static Try<(TA, TB)> Zip<TA, TB>(
        this Try<TA> self,
        Try<TB> other
    )
        where TA : notnull
        where TB : notnull
    {
        return self.ZipWith(other, (a, b) => (a, b));
    }

    public static async Task<Try<(TA, TB)>> ZipAsync<TA, TB>(
        this Try<TA> self,
        Task<Try<TB>> otherAsync
    )
        where TA : notnull
        where TB : notnull
    {
        return await self.ZipWithAsync(otherAsync, (a, b) => (a, b)).ConfigureAwait(false);
    }

    public static async Task<Try<(TA, TB)>> ZipAsync<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Task<Try<TB>> otherAsync
    )
        where TA : notnull
        where TB : notnull
    {
        return await selfAsync.ZipWithAsync(otherAsync, (a, b) => (a, b)).ConfigureAwait(false);
    }

    public static async Task<Try<(TA, TB)>> Zip<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Try<TB> other
    )
        where TA : notnull
        where TB : notnull
    {
        return await selfAsync.ZipWith(other, (a, b) => (a, b)).ConfigureAwait(false);
    }
}