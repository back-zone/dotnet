using back.zone.monads.Types;

namespace back.zone.monads.TryMonad;

public static class OrElseExtensions
{
    public static async Task<Try<TU>> OrElseAsync<TA, TU>(
        this Task<Try<TA>> self,
        TU other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            var current = await self.ConfigureAwait(false);
            return current.OrElse(other);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TU>> OrElseAsync<TA, TU>(
        this Task<Try<TA>> self,
        Task<TU> other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            var current = await self.ConfigureAwait(false);
            return await current.OrElseAsync(other).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Task<Try<TA>> self,
        TU other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            var current = await self.ConfigureAwait(false);
            return current.GetOrElse(other);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("#failed_to_get_or_else_async#", e);
        }
    }

    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Task<Try<TA>> self,
        Task<TU> other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            var current = await self.ConfigureAwait(false);
            return await current.GetOrElseAsync(other).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("#failed_to_get_or_else_async#", e);
        }
    }

    public static async Task<Try<TU>> TransformAsync<TA, TU>(
        this Task<Try<TA>> self,
        Continuation<TA, Try<TU>> continuation
    )
        where TA : notnull
        where TU : TA
    {
        return await Try.ProvideAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Try<TU>> TransformAsync<TA, TU>(
        this Task<Try<TA>> self,
        Continuation<TA, Task<Try<TU>>> continuation
    )
        where TA : notnull
        where TU : TA
    {
        return await Try.ProvideAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Try<TA>> TransformErrorAsync<TA>(
        this Task<Try<TA>> self,
        Continuation<Exception, Exception> continuation
    )
        where TA : notnull
    {
        try
        {
            var current = await self.ConfigureAwait(false);
            return current.TransformError(continuation);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("#failed_to_transform_error_async#", e);
        }
    }

    public static async Task<Try<TA>> TransformErrorAsync<TA>(
        this Task<Try<TA>> self,
        Continuation<Exception, Task<Exception>> continuation
    )
        where TA : notnull
    {
        try
        {
            var current = await self.ConfigureAwait(false);
            return await current.TransformErrorAsync(continuation).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("#failed_to_transform_error_async#", e);
        }
    }
}