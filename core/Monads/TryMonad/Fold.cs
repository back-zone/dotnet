using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryFold
{
    /// <summary>
    ///     Folds a Try monad into a single value by applying either a success or failure continuation.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result after folding.</typeparam>
    /// <param name="self">The Try monad to fold.</param>
    /// <param name="failureCont">The continuation to apply if the Try monad contains an exception.</param>
    /// <param name="successCont">The continuation to apply if the Try monad contains a value.</param>
    /// <returns>
    ///     The result of applying either the success or failure continuation, or throws an
    ///     InvalidOperationException if the Try monad is in an invalid state.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the Try monad contains neither a value nor an exception, or when an exception
    ///     occurs during the execution of the success continuation.
    /// </exception>
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

    /// <summary>
    ///     Asynchronously folds a Try monad into a single value by applying either a success or failure continuation.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result after folding.</typeparam>
    /// <param name="self">The Try monad to fold.</param>
    /// <param name="failureCont">The asynchronous continuation to apply if the Try monad contains an exception.</param>
    /// <param name="successCont">The asynchronous continuation to apply if the Try monad contains a value.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the value of type TB
    ///     obtained by applying either the success or failure continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the Try monad contains neither a value nor an exception, or when an exception
    ///     occurs during the execution of the success continuation.
    /// </exception>
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

    /// <summary>
    ///     Asynchronously folds a Try monad into a single value by applying either a success or failure continuation.
    ///     This overload handles a synchronous failure continuation and an asynchronous success continuation.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result after folding.</typeparam>
    /// <param name="self">The Try monad to fold.</param>
    /// <param name="failureCont">The synchronous continuation to apply if the Try monad contains an exception.</param>
    /// <param name="successCont">The asynchronous continuation to apply if the Try monad contains a value.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the value of type TB
    ///     obtained by applying either the success or failure continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the Try monad contains neither a value nor an exception, or when an exception
    ///     occurs during the execution of the success continuation.
    /// </exception>
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

    /// <summary>
    ///     Asynchronously folds a Try monad into a single value by applying either a success or failure continuation.
    ///     This overload handles an asynchronous failure continuation and a synchronous success continuation.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result after folding.</typeparam>
    /// <param name="self">The Try monad to fold.</param>
    /// <param name="failureCont">The asynchronous continuation to apply if the Try monad contains an exception.</param>
    /// <param name="successCont">The synchronous continuation to apply if the Try monad contains a value.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the value of type TB
    ///     obtained by applying either the success or failure continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the Try monad contains neither a value nor an exception, or when an exception
    ///     occurs during the execution of the success continuation.
    /// </exception>
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

    /// <summary>
    ///     Asynchronously folds a Task of Try monad into a single value by applying either a success or failure continuation.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result after folding.</typeparam>
    /// <param name="selfAsync">The Task of Try monad to fold.</param>
    /// <param name="failureCont">The continuation to apply if the Try monad contains an exception.</param>
    /// <param name="successCont">The continuation to apply if the Try monad contains a value.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the value of type TB
    ///     obtained by applying either the success or failure continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the Try monad contains neither a value nor an exception, or when an exception
    ///     occurs during the execution of the success continuation.
    /// </exception>
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

    /// <summary>
    ///     Asynchronously folds a Task of Try monad into a single value by applying either a success or failure continuation.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result after folding.</typeparam>
    /// <param name="selfAsync">The Task of Try monad to fold.</param>
    /// <param name="failureCont">The asynchronous continuation to apply if the Try monad contains an exception.</param>
    /// <param name="successCont">The asynchronous continuation to apply if the Try monad contains a value.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the value of type TB
    ///     obtained by applying either the success or failure continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the Try monad contains neither a value nor an exception, or when an exception
    ///     occurs during the execution of the success continuation.
    /// </exception>
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

    /// <summary>
    ///     Asynchronously folds a Task of Try monad into a single value by applying either a success or failure continuation.
    ///     This overload handles a synchronous failure continuation and an asynchronous success continuation.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result after folding.</typeparam>
    /// <param name="selfAsync">The Task of Try monad to fold.</param>
    /// <param name="failureCont">The synchronous continuation to apply if the Try monad contains an exception.</param>
    /// <param name="successCont">The asynchronous continuation to apply if the Try monad contains a value.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the value of type TB
    ///     obtained by applying either the success or failure continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the Try monad contains neither a value nor an exception, or when an exception
    ///     occurs during the execution of the success continuation.
    /// </exception>
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

    /// <summary>
    ///     Asynchronously folds a Task of Try monad into a single value by applying either a success or failure continuation.
    ///     This overload handles an asynchronous failure continuation and a synchronous success continuation.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result after folding.</typeparam>
    /// <param name="selfAsync">The Task of Try monad to fold.</param>
    /// <param name="failureCont">The asynchronous continuation to apply if the Try monad contains an exception.</param>
    /// <param name="successCont">The synchronous continuation to apply if the Try monad contains a value.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the value of type TB
    ///     obtained by applying either the success or failure continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the Try monad contains neither a value nor an exception, or when an exception
    ///     occurs during the execution of the success continuation.
    /// </exception>
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