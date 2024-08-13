using back.zone.monads.Types;

namespace back.zone.monads.TryMonad;

public abstract class Try<TA>
    where TA : notnull
{
    public abstract bool IsSuccess();

    public bool IsFailure()
    {
        return !IsSuccess();
    }

    public abstract Exception Exception();

    public abstract TA Value();

    public static implicit operator Try<TA>(TA value)
    {
        return Try.Succeed(value);
    }

    public static implicit operator Try<TA>(Exception exception)
    {
        return Try.Fail<TA>(exception);
    }

    public Try<TB> FlatMap<TB>(
        Continuation<TA, Try<TB>> continuation
    )
        where TB : notnull
    {
        return Try.Provide(this, continuation);
    }

    public async Task<Try<TB>> FlatMapAsync<TB>(
        Continuation<TA, Task<Try<TB>>> asyncContinuation
    )
        where TB : notnull
    {
        return await Try.ProvideAsync(this, asyncContinuation).ConfigureAwait(false);
    }

    public Try<TB> Map<TB>(
        Continuation<TA, TB> continuation
    )
        where TB : notnull
    {
        return Try.Provide(this, continuation);
    }

    public async Task<Try<TB>> MapAsync<TB>(
        Continuation<TA, Task<TB>> asyncContinuation
    )
        where TB : notnull
    {
        return await Try.ProvideAsync(this, asyncContinuation).ConfigureAwait(false);
    }

    public Try<TC> ZipWith<TB, TC>(Try<TB> other, Continuation<TA, TB, TC> zipper)
        where TB : notnull
        where TC : notnull
    {
        try
        {
            if (IsFailure()) return Exception();
            if (other.IsFailure()) return other.Exception();

            var b = other.Value();

            return zipper(Value(), b);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<Try<TC>> ZipWithAsync<TB, TC>(
        Task<Try<TB>> otherAsync,
        Continuation<TA, TB, Task<TC>> asyncZipper
    )
        where TB : notnull
        where TC : notnull
    {
        try
        {
            if (IsFailure()) return Exception();

            var other = await otherAsync.ConfigureAwait(false);
            if (other.IsFailure()) return other.Exception();

            return await asyncZipper(Value(), other.Value()).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public Try<TB> ZipRight<TB>(Try<TB> other)
        where TB : notnull
    {
        return ZipWith(other, (_, b) => b);
    }

    public async Task<Try<TB>> ZipRightAsync<TB>(
        Task<Try<TB>> otherAsync
    )
        where TB : notnull
    {
        return await ZipWithAsync(otherAsync, (_, b) => Task.FromResult(b)).ConfigureAwait(false);
    }

    public Try<(TA, TB)> Zip<TB>(Try<TB> other)
        where TB : notnull
    {
        return ZipWith(other, (a, b) => (a, b));
    }

    public async Task<Try<(TA, TB)>> ZipAsync<TB>(
        Task<Try<TB>> otherAsync
    )
        where TB : notnull
    {
        return await ZipWithAsync(otherAsync, (a, b) => Task.FromResult((a, b))).ConfigureAwait(false);
    }

    public TB Fold<TB>(
        Continuation<Exception, TB> failureHandler,
        Continuation<TA, TB> successHandler)
        where TB : notnull
    {
        try
        {
            return IsFailure()
                ? failureHandler(Exception())
                : successHandler(Value());
        }
        catch (Exception e)
        {
            return failureHandler(new InvalidOperationException("#unexpected_exception_in_fold_method#", e));
        }
    }

    public async Task<TB> FoldAsync<TB>(
        Continuation<Exception, Task<TB>> failureHandler,
        Continuation<TA, Task<TB>> successHandler)
        where TB : notnull
    {
        try
        {
            return IsFailure()
                ? await failureHandler(Exception()).ConfigureAwait(false)
                : await successHandler(Value()).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return await failureHandler(new InvalidOperationException("#unexpected_exception_in_fold_method#", e))
                .ConfigureAwait(false);
        }
    }

    public async Task<TB> FoldAsync<TB>(
        Continuation<Exception, TB> failureHandler,
        Continuation<TA, Task<TB>> successHandler)
        where TB : notnull
    {
        try
        {
            return IsFailure()
                ? failureHandler(Exception())
                : await successHandler(Value()).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return failureHandler(new InvalidOperationException("#unexpected_exception_in_fold_method#", e));
        }
    }

    public async Task<TB> FoldAsync<TB>(
        Continuation<Exception, Task<TB>> failureHandler,
        Continuation<TA, TB> successHandler)
        where TB : notnull
    {
        try
        {
            return IsFailure()
                ? await failureHandler(Exception()).ConfigureAwait(false)
                : successHandler(Value());
        }
        catch (Exception e)
        {
            return await failureHandler(new InvalidOperationException("#unexpected_exception_in_fold_method#", e))
                .ConfigureAwait(false);
        }
    }

    public Try<TA> Recover(Continuation<Exception, TA> recoveryHandler)
    {
        try
        {
            return IsFailure()
                ? recoveryHandler(Exception())
                : this;
        }
        catch (Exception e)
        {
            return new InvalidOperationException("#failed_to_recover#", e);
        }
    }

    public async Task<Try<TA>> RecoverAsync(
        Continuation<Exception, Task<TA>> recoveryHandler
    )
    {
        try
        {
            return IsFailure()
                ? await recoveryHandler(Exception()).ConfigureAwait(false)
                : this;
        }
        catch (Exception e)
        {
            return new InvalidOperationException("#failed_to_recover#", e);
        }
    }

    public Try<TU> OrElse<TU>(TU other)
        where TU : TA
    {
        return IsFailure() ? other : (TU)Value();
    }

    public async Task<Try<TU>> OrElseAsync<TU>(Task<TU> other)
        where TU : TA
    {
        try
        {
            return IsFailure()
                ? await other.ConfigureAwait(false)
                : (TU)Value();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public TU GetOrElse<TU>(TU other)
        where TU : TA
    {
        return IsFailure() ? other : (TU)Value();
    }

    public async Task<TU> GetOrElseAsync<TU>(Task<TU> other)
        where TU : TA
    {
        try
        {
            return IsFailure()
                ? await other.ConfigureAwait(false)
                : (TU)Value();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("#failed_to_get_or_else_async#", e);
        }
    }

    public Try<TU> Transform<TU>(Continuation<TA, Try<TU>> transformation)
        where TU : TA
    {
        return FlatMap(transformation);
    }

    public async Task<Try<TU>> TransformAsync<TU>(
        Continuation<TA, Task<Try<TU>>> asyncTransformation
    )
        where TU : TA
    {
        return await FlatMapAsync(asyncTransformation).ConfigureAwait(false);
    }

    public Try<TA> TransformError(Continuation<Exception, Exception> transformation)
    {
        return IsFailure()
            ? transformation(Exception())
            : this;
    }

    public async Task<Try<TA>> TransformErrorAsync(
        Continuation<Exception, Task<Exception>> asyncTransformation
    )
    {
        try
        {
            return IsFailure()
                ? await asyncTransformation(Exception()).ConfigureAwait(false)
                : this;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("#failed_to_transform_error_async#", e);
        }
    }
}