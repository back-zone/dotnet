using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionZip
{
    public static Option<TC> ZipWith<TA, TB, TC>(
        this Option<TA> self,
        Option<TB> other,
        OptionalZipper<TA, TB, TC> zipper
    )
    {
        try
        {
            return self.TryGetValue(out var a)
                ? other.TryGetValue(out var b)
                    ? zipper(a, b)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Option<TA> self,
        Task<Option<TB>> otherAsync,
        OptionalZipper<TA, TB, TC> zipper
    )
    {
        try
        {
            return self.TryGetValue(out var a)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var b)
                    ? zipper(a, b)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Option<TA> self,
        Option<TB> other,
        OptionalZipper<TA, TB, Task<TC>> zipperAsync
    )
    {
        try
        {
            return self.TryGetValue(out var a)
                ? other.TryGetValue(out var b)
                    ? await zipperAsync(a, b).ConfigureAwait(false)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Option<TA> self,
        Task<Option<TB>> otherAsync,
        OptionalZipper<TA, TB, Task<TC>> zipperAsync
    )
    {
        try
        {
            return self.TryGetValue(out var a)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var b)
                    ? await zipperAsync(a, b).ConfigureAwait(false)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> ZipWith<TA, TB, TC>(
        this Task<Option<TA>> self,
        Option<TB> other,
        OptionalZipper<TA, TB, TC> zipper
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var a)
                ? other.TryGetValue(out var b)
                    ? zipper(a, b)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Option<TA>> self,
        Task<Option<TB>> otherAsync,
        OptionalZipper<TA, TB, TC> zipper
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var a)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var b)
                    ? zipper(a, b)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Option<TA>> self,
        Option<TB> other,
        OptionalZipper<TA, TB, Task<TC>> zipperAsync
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var a)
                ? other.TryGetValue(out var b)
                    ? await zipperAsync(a, b).ConfigureAwait(false)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Option<TA>> self,
        Task<Option<TB>> otherAsync,
        OptionalZipper<TA, TB, Task<TC>> zipperAsync
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var a)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var b)
                    ? await zipperAsync(a, b).ConfigureAwait(false)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    public static Option<TB> ZipRight<TA, TB>(
        this Option<TA> self,
        Option<TB> other
    )
    {
        return self.ZipWith(other, (_, b) => b);
    }

    public static async Task<Option<TB>> ZipRightAsync<TA, TB>(
        this Option<TA> self,
        Task<Option<TB>> otherAsync
    )
    {
        return await self.ZipWithAsync(otherAsync, (_, b) => b).ConfigureAwait(false);
    }

    public static async Task<Option<TB>> ZipRightAsync<TA, TB>(
        this Task<Option<TA>> selfAsync,
        Task<Option<TB>> otherAsync
    )
    {
        return await selfAsync.ZipWithAsync(otherAsync, (_, b) => b).ConfigureAwait(false);
    }

    public static async Task<Option<TB>> ZipRight<TA, TB>(
        this Task<Option<TA>> selfAsync,
        Option<TB> other
    )
    {
        return await selfAsync.ZipWith(other, (_, b) => b).ConfigureAwait(false);
    }

    public static Option<(TA, TB)> Zip<TA, TB>(
        this Option<TA> self,
        Option<TB> other
    )
    {
        return self.ZipWith(other, (a, b) => (a, b));
    }

    public static async Task<Option<(TA, TB)>> ZipAsync<TA, TB>(
        this Option<TA> self,
        Task<Option<TB>> otherAsync
    )
    {
        return await self.ZipWithAsync(otherAsync, (a, b) => (a, b)).ConfigureAwait(false);
    }

    public static async Task<Option<(TA, TB)>> ZipAsync<TA, TB>(
        this Task<Option<TA>> selfAsync,
        Task<Option<TB>> otherAsync
    )
    {
        return await selfAsync.ZipWithAsync(otherAsync, (a, b) => (a, b)).ConfigureAwait(false);
    }

    public static async Task<Option<(TA, TB)>> Zip<TA, TB>(
        this Task<Option<TA>> selfAsync,
        Option<TB> other
    )
    {
        return await selfAsync.ZipWith(other, (a, b) => (a, b)).ConfigureAwait(false);
    }
}