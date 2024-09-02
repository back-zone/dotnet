using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class Try
{
    /// <summary>
    ///     Creates a successful Try monad containing the specified value.
    /// </summary>
    /// <typeparam name="TA">The type of the value to be wrapped in the Try monad.</typeparam>
    /// <param name="value">The value to be wrapped in the Try monad.</param>
    /// <returns>A Try monad representing a successful operation containing the specified value.</returns>
    public static Try<TA> Succeed<TA>(
        TA value
    )
        where TA : notnull
    {
        return value;
    }

    /// <summary>
    ///     Creates a failed Try monad containing the specified exception.
    /// </summary>
    /// <typeparam name="TA">
    ///     The type parameter for the Try monad. This type is not used in the failure case but is required
    ///     for type consistency.
    /// </typeparam>
    /// <param name="exception">The exception to be wrapped in the Try monad, representing the failure.</param>
    /// <returns>A Try monad representing a failed operation containing the specified exception.</returns>
    public static Try<TA> Fail<TA>(
        Exception exception
    )
        where TA : notnull
    {
        return exception;
    }

    /// <summary>
    ///     Creates a Try monad by executing the provided effect function.
    /// </summary>
    /// <typeparam name="TA">The type of the value to be wrapped in the Try monad.</typeparam>
    /// <param name="effect">A function that represents an effect to be executed and wrapped in a Try monad.</param>
    /// <returns>
    ///     A Try monad representing either a successful operation containing the result of the effect,
    ///     or a failed operation containing any exception thrown during the effect's execution.
    /// </returns>
    public static Try<TA> Effect<TA>(
        Effect<TA> effect
    )
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

    /// <summary>
    ///     Creates a Try monad by executing the provided asynchronous task.
    /// </summary>
    /// <typeparam name="TA">The type of the value to be wrapped in the Try monad.</typeparam>
    /// <param name="asyncTask">
    ///     A ValueTask that represents an asynchronous operation to be executed and wrapped in a Try
    ///     monad.
    /// </param>
    /// <returns>
    ///     A Task that represents the asynchronous operation, which when completed, returns a Try monad.
    ///     The Try monad will contain either:
    ///     - A successful result with the value from the asyncTask, if it completes without throwing an exception.
    ///     - A failure result with the caught exception, if the asyncTask throws an exception during execution.
    /// </returns>
    public static async Task<Try<TA>> AsyncValue<TA>(
        ValueTask<TA> asyncTask
    )
        where TA : notnull
    {
        try
        {
            return await asyncTask.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Creates a Try monad by executing the provided asynchronous Task.
    /// </summary>
    /// <typeparam name="TA">The type of the value to be wrapped in the Try monad.</typeparam>
    /// <param name="asyncTask">
    ///     An asynchronous Task that represents an operation to be executed and wrapped in a Try monad.
    /// </param>
    /// <returns>
    ///     A Task that represents the asynchronous operation, which when completed, returns a Try monad.
    ///     The Try monad will contain either:
    ///     - A successful result with the value from the asyncTask, if it completes without throwing an exception.
    ///     - A failure result with the caught exception, if the asyncTask throws an exception during execution.
    /// </returns>
    public static async Task<Try<TA>> Async<TA>(Task<TA> asyncTask)
        where TA : notnull
    {
        try
        {
            return await asyncTask.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Creates a Try monad by executing the provided asynchronous effect function.
    /// </summary>
    /// <typeparam name="TA">The type of the value to be wrapped in the Try monad.</typeparam>
    /// <param name="asyncEffect">An asynchronous function that represents an effect to be executed and wrapped in a Try monad.</param>
    /// <returns>
    ///     A Task that represents the asynchronous operation, which when completed, returns a Try monad.
    ///     The Try monad will contain either:
    ///     - A successful result with the value from the asyncEffect, if it completes without throwing an exception.
    ///     - A failure result with the caught exception, if the asyncEffect throws an exception during execution.
    /// </returns>
    public static async Task<Try<TA>> Async<TA>(
        Effect<Task<TA>> asyncEffect
    )
        where TA : notnull
    {
        try
        {
            return await asyncEffect().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Creates a Try monad by executing the provided asynchronous Task that returns a Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the value wrapped in the Try monad.</typeparam>
    /// <param name="asyncTry">
    ///     An asynchronous Task that returns a Try monad. This Task represents an operation that may succeed or fail.
    /// </param>
    /// <returns>
    ///     A Task that represents the asynchronous operation, which when completed, returns a Try monad.
    ///     The Try monad will contain either:
    ///     - The original Try monad if the asyncTry Task completes successfully and the inner Try is successful.
    ///     - A failure result with the exception from the inner Try if it was in a failed state.
    ///     - A failure result with any exception caught during the execution of the asyncTry Task.
    /// </returns>
    public static async Task<Try<TA>> Async<TA>(
        Task<Try<TA>> asyncTry
    )
        where TA : notnull
    {
        try
        {
            var current = await asyncTry.ConfigureAwait(false);

            return current.TryGetException(out var ex)
                ? ex
                : current;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}