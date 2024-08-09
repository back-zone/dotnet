using back.zone.monads.Types;

namespace back.zone.monads.TryMonad;

public static class FoldExtensions
{
    public static async Task<TB> FoldAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<Exception, TB> failureHandler,
        Continuation<TA, TB> successHandler
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;

            return result.Fold(failureHandler, successHandler);
        }
        catch (Exception e)
        {
            return failureHandler(e);
        }
    }

    public static async Task<TB> FoldAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<Exception, Task<TB>> failureHandler,
        Continuation<TA, Task<TB>> successHandler
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;

            return await result.FoldAsync(failureHandler, successHandler);
        }
        catch (Exception e)
        {
            return await failureHandler(e);
        }
    }

    public static async Task<TB> FoldAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<Exception, TB> failureHandler,
        Continuation<TA, Task<TB>> successHandler
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;

            return await result.FoldAsync(failureHandler, successHandler);
        }
        catch (Exception e)
        {
            return failureHandler(e);
        }
    }

    public static async Task<TB> FoldAsync<TA, TB>(
        this Task<Try<TA>> self,
        Continuation<Exception, Task<TB>> failureHandler,
        Continuation<TA, TB> successHandler
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var result = await self;

            return await result.FoldAsync(failureHandler, successHandler);
        }
        catch (Exception e)
        {
            return await failureHandler(e);
        }
    }
}