using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryFold
{
    public static TB Fold<TA, TB>(
        this Try<TA> self,
        Continuation<Exception, TB> failureCont,
        Continuation<TA, TB> successCont)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? successCont(value)
                : self.TryGetException(out var exception)
                    ? failureCont(exception)
                    : throw new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return failureCont(new InvalidOperationException("#exception_in_fold_success#", e));
        }
    }

    public static async Task<TB> FoldAsync<TA, TB>(
        this Try<TA> self,
        Func<Exception, Task<TB>> failureCont,
        Func<TA, Task<TB>> successCont)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? await successCont(value).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? await failureCont(exception).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return await failureCont(new InvalidOperationException("#exception_in_fold_async_success#", e))
                .ConfigureAwait(false);
        }
    }

    public static async Task<TB> FoldAsync<TA, TB>(
        this Try<TA> self,
        Func<Exception, TB> failureCont,
        Func<TA, Task<TB>> successCont)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? await successCont(value)
                : self.TryGetException(out var exception)
                    ? failureCont(exception)
                    : throw new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return failureCont(new InvalidOperationException("#exception_in_fold_async_success#", e));
        }
    }

    public static async Task<TB> FoldAsync<TA, TB>(
        this Try<TA> self,
        Func<Exception, Task<TB>> failureCont,
        Func<TA, TB> successCont)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? successCont(value)
                : self.TryGetException(out var exception)
                    ? await failureCont(exception).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return await failureCont(new InvalidOperationException("#exception_in_fold_async_success#", e))
                .ConfigureAwait(false);
        }
    }

    public static async Task<TB> Fold<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Continuation<Exception, TB> failureCont,
        Continuation<TA, TB> successCont)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? successCont(value)
                : self.TryGetException(out var exception)
                    ? failureCont(exception)
                    : throw new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return failureCont(new InvalidOperationException("#exception_in_fold_success#", e));
        }
    }

    public static async Task<TB> FoldAsync<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Func<Exception, Task<TB>> failureCont,
        Func<TA, Task<TB>> successCont)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? await successCont(value).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? await failureCont(exception).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return await failureCont(new InvalidOperationException("#exception_in_fold_async_success#", e))
                .ConfigureAwait(false);
        }
    }

    public static async Task<TB> FoldAsync<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Func<Exception, TB> failureCont,
        Func<TA, Task<TB>> successCont)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? await successCont(value)
                : self.TryGetException(out var exception)
                    ? failureCont(exception)
                    : throw new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return failureCont(new InvalidOperationException("#exception_in_fold_async_success#", e));
        }
    }

    public static async Task<TB> FoldAsync<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Func<Exception, Task<TB>> failureCont,
        Func<TA, TB> successCont)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? successCont(value)
                : self.TryGetException(out var exception)
                    ? await failureCont(exception).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return await failureCont(new InvalidOperationException("#exception_in_fold_async_success#", e))
                .ConfigureAwait(false);
        }
    }
}