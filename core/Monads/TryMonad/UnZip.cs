using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryUnZip
{
    public static Try<TC> UnZip<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Continuation<(TA, TB), TC> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? continuation(value)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnZipAsync<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Continuation<(TA, TB), Task<TC>> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnZipAsync<TA, TB, TC>(
        this Task<Try<(TA, TB)>> selfAsync,
        Continuation<(TA, TB), Task<TC>> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnZip<TA, TB, TC>(
        this Task<Try<(TA, TB)>> selfAsync,
        Continuation<(TA, TB), TC> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? continuation(value)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TC> UnZip<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Zipper<TA, TB, TC> zipper
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? zipper(value.Item1, value.Item2)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnZipAsync<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Zipper<TA, TB, Task<TC>> zipper
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? await zipper(value.Item1, value.Item2).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnZipAsync<TA, TB, TC>(
        this Task<Try<(TA, TB)>> selfAsync,
        Zipper<TA, TB, Task<TC>> zipper
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? await zipper(value.Item1, value.Item2).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnZip<TA, TB, TC>(
        this Task<Try<(TA, TB)>> selfAsync,
        Zipper<TA, TB, TC> zipper
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? zipper(value.Item1, value.Item2)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }
}