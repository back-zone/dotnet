using back.zone.monads.Types;

namespace back.zone.monads.TryMonad;

public static class RecoverExtensions
{
    public static async Task<Try<TA>> RecoverAsync<TA>(
        this Task<Try<TA>> self,
        Continuation<Exception, TA> continuation)
        where TA : notnull
    {
        try
        {
            var current = await self;

            return current.Recover(continuation);
        }
        catch (Exception e)
        {
            return new InvalidOperationException("#failed_to_recover_async#", e);
        }
    }

    public static async Task<Try<TA>> RecoverAsync<TA>(
        this Task<Try<TA>> self,
        Continuation<Exception, Task<TA>> continuation)
        where TA : notnull
    {
        try
        {
            var current = await self;

            return await current.RecoverAsync(continuation);
        }
        catch (Exception e)
        {
            return new InvalidOperationException("#failed_to_recover_async#", e);
        }
    }
}