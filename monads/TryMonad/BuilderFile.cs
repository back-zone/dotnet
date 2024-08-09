using back.zone.monads.Types;

namespace back.zone.monads.TryMonad;

public static class Try
{
    public static Try<TA> Succeed<TA>(TA value)
        where TA : notnull
    {
        return value;
    }

    public static Try<TA> Fail<TA>(Exception exception)
        where TA : notnull
    {
        return exception;
    }

    public static Try<TA> Fail<TA>(string message)
        where TA : notnull
    {
        return new Exception(message);
    }

    public static Try<TA> Effect<TA>(Effect<TA> effect)
        where TA : notnull
    {
        try
        {
            var result = effect();
            return result;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> Provide<TA, TB>(
        TA value,
        Continuation<TA, TB> continuation)
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

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        TA value,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(value);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Task<TA> valueTask,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var value = await valueTask;
            return await continuation(value);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Task<TA> valueTask,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var value = await valueTask;
            return continuation(value);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> Provide<TA, TB>(
        Try<TA> self,
        Continuation<TA, TB> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.IsFailure()
                ? self.Exception()
                : continuation(self.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Try<TA> self,
        Continuation<TA, Task<TB>> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.IsFailure()
                ? self.Exception()
                : await continuation(self.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Task<Try<TA>> self,
        Continuation<TA, Task<TB>> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;

            return result.IsFailure()
                ? result.Exception()
                : await continuation(result.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Task<Try<TA>> self,
        Continuation<TA, TB> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;

            return result.IsFailure()
                ? result.Exception()
                : continuation(result.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> Provide<TA, TB>(
        TA value,
        Continuation<TA, Try<TB>> continuation)
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

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        TA value,
        Continuation<TA, Task<Try<TB>>> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(value);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Task<TA> self,
        Continuation<TA, Task<Try<TB>>> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;
            return await continuation(result);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Task<TA> self,
        Continuation<TA, Try<TB>> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;
            return continuation(result);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TB> Provide<TA, TB>(
        Try<TA> self,
        Continuation<TA, Try<TB>> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.IsFailure()
                ? self.Exception()
                : continuation(self.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Try<TA> self,
        Continuation<TA, Task<Try<TB>>> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.IsFailure()
                ? self.Exception()
                : await continuation(self.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Task<Try<TA>> self,
        Continuation<TA, Task<Try<TB>>> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;
            return result.IsFailure()
                ? result.Exception()
                : await continuation(result.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TB>> ProvideAsync<TA, TB>(
        Task<Try<TA>> self,
        Continuation<TA, Try<TB>> continuation)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;
            return result.IsFailure()
                ? result.Exception()
                : continuation(result.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }


    public static async Task<Try<TA>> Async<TA>(Task<TA> asyncTask)
        where TA : notnull
    {
        try
        {
            var result = await asyncTask;
            return result;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TA>> Async<TA>(Task<Try<TA>> asyncTask)
        where TA : notnull
    {
        try
        {
            var result = await asyncTask;
            return result;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}