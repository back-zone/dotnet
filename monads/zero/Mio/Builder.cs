using System.Runtime.CompilerServices;
using back.zone.monads.Types;

namespace back.zone.monads.zero.Mio;

public static class Mio
{
    public static Mio<TA> Succeed<TA>(TA value)
        where TA : notnull
    {
        return new Mio<TA>(value);
    }

    public static Mio<TA> Fail<TA>(Exception exception)
        where TA : notnull
    {
        return new Mio<TA>(exception);
    }

    public static Mio<TA> Effect<TA>(Effect<TA> effect)
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

    public static async Task<Mio<TA>> Async<TA>(
        Task<TA> task
    )
        where TA : notnull
    {
        try
        {
            return await task;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Mio<TB> Provide<TA, TB>(
        TA value,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Mio<TB> Provide<TA, TB>(
        TA value,
        Continuation<TA, Mio<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
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

    public static Mio<TB> Provide<TA, TB>(
        Mio<TA> self,
        Continuation<TA, Mio<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return !self.IsSuccess
                ? self.Exception!
                : continuation(self.Value!);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Mio<TB>> ProvideAsync<TA, TB>(
        TA value,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(value).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Mio<TB>> ProvideAsync<TA, TB>(
        Task<Mio<TA>> self,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await self.ConfigureAwait(false);

            return current.IsSuccess
                ? await continuation(current.Value!).ConfigureAwait(false)
                : current.Exception!;
        }
        catch (Exception e)
        {
            return e;
        }
    }


    public static async Task<Mio<TB>> ProvideAsync<TA, TB>(
        Mio<TA> self,
        Continuation<TA, Task<Mio<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            if (!self.IsSuccess) return self.Exception!;

            return await continuation(self.Value!).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Mio<TB>> ProvideAsync<TA, TB>(
        TA value,
        Continuation<TA, Task<Mio<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(value).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Mio<TB>> ProvideAsync<TA, TB>(
        Task<TA> asyncValue,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
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

    public static async Task<TU> GetOrElse<TA, TU>(
        this Task<Mio<TA>> self,
        TU tu
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            var result = await self.ConfigureAwait(false);
            return result.IsSuccess
                ? (TU)result.Value!
                : tu;
        }
        catch (Exception)
        {
            return tu;
        }
    }

    public static TU GetOrElse<TA, TU>(
        this Mio<TA> self,
        TU tu
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return self is { IsSuccess: true, Value: not null }
                ? (TU)self.Value
                : tu;
        }
        catch (Exception)
        {
            return tu;
        }
    }

    public static async Task<Mio<TB>> MapAsync<TA, TB>(
        this Task<Mio<TA>> self,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await self.ConfigureAwait(false);

            return current.IsSuccess
                ? await continuation(current.Value!).ConfigureAwait(false)
                : current.Exception!;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Mio<TB>> MapAsync<TA, TB>(
        this Task<Mio<TA>> self,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await self.ConfigureAwait(false);
            return current.Map(continuation);
        }
        catch (Exception e)
        {
            return e;
        }
    }
}