using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

internal static class TryRuntime
{
    /// <summary>
    ///     Executes a given continuation function with the provided value and returns a Try monad.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result value.</typeparam>
    /// <param name="value">The input value to be passed to the continuation function.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static Try<TB> RunValue<TA, TB>(
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

    /// <summary>
    ///     Executes a given continuation function with the provided value and returns a Try monad.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result value.</typeparam>
    /// <param name="value">The input value to be passed to the continuation function.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static Try<TB> RunValue<TA, TB>(
        TA value,
        Continuation<TA, Try<TB>> continuation
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

    /// <summary>
    ///     Executes a given continuation function with the provided asynchronous value and returns a Try monad.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result value.</typeparam>
    /// <param name="asyncValue">The asynchronous input value to be passed to the continuation function.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(await asyncValue.ConfigureAwait(false));
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided asynchronous value and returns a Try monad.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result value.</typeparam>
    /// <param name="asyncValue">The asynchronous input value to be passed to the continuation function.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunAsyncValue<TA, TB>(
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

    /// <summary>
    ///     Executes a given continuation function with the provided asynchronous value and returns a Try monad.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result value.</typeparam>
    /// <param name="asyncValue">The asynchronous input value to be passed to the continuation function.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunValue<TA, TB>(
        Task<TA> asyncValue,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(await asyncValue.ConfigureAwait(false));
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided asynchronous value and returns a Try monad.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result value.</typeparam>
    /// <param name="asyncValue">The asynchronous input value to be passed to the continuation function.</param>
    /// <param name="continuation">
    ///     The function to be executed with the input value. This function should return a Task of Try
    ///     monad.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the continuation function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        Continuation<TA, Task<Try<TB>>> continuation
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

    /// <summary>
    ///     Executes a given effect function and continuation function, returning a Try monad.
    ///     If the effect function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the effect function.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="effect">The effect function to be executed.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the effect function.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the effect function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static Try<TB> RunEffect<TA, TB>(
        Effect<TA> effect,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(effect());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given effect function and continuation function, returning a Try monad.
    ///     If the effect function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the effect function.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="effect">The effect function to be executed.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the effect function.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the effect function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static Try<TB> RunEffect<TA, TB>(
        Effect<TA> effect,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return continuation(effect());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given effect function and continuation function, returning a Try monad.
    ///     If the effect function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the effect function.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="effect">The effect function to be executed.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the effect function.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the effect function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunEffectAsync<TA, TB>(
        Effect<TA> effect,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(effect()).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given effect function and continuation function, returning a Try monad.
    ///     If the effect function throws an exception, it will be caught and returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the effect function.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="effect">The effect function to be executed.</param>
    /// <param name="continuation">
    ///     The continuation function to be executed with the result of the effect function. This
    ///     function should return a Task of Try monad.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the effect function throws an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunEffectAsync<TA, TB>(
        Effect<TA> effect,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return await continuation(effect()).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided Try monad value and returns a Try monad.
    ///     If the Try monad contains a value, the continuation function is executed with the value.
    ///     If the Try monad contains an exception, the exception is returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="current">The Try monad containing the input value.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the Try monad.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the Try monad contains an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static Try<TB> RunTry<TA, TB>(
        Try<TA> current,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return current.TryGetValue(out var value)
                ? continuation(value)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided Try monad value and returns a Try monad.
    ///     If the Try monad contains a value, the continuation function is executed with the value.
    ///     If the Try monad contains an exception, the exception is returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="current">The Try monad containing the input value.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the Try monad.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the Try monad contains an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunTryAsync<TA, TB>(
        Try<TA> current,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided Try monad value and returns a Try monad.
    ///     If the Try monad contains a value, the continuation function is executed with the value.
    ///     If the Try monad contains an exception, the exception is returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="currentAsync">The asynchronous Try monad containing the input value.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the Try monad.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the Try monad contains an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunTryAsync<TA, TB>(
        Task<Try<TA>> currentAsync,
        Continuation<TA, Task<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided Try monad value and returns a Try monad.
    ///     If the Try monad contains a value, the continuation function is executed with the value.
    ///     If the Try monad contains an exception, the exception is returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="currentAsync">The asynchronous Try monad containing the input value.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the Try monad.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the Try monad contains an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunTry<TA, TB>(
        Task<Try<TA>> currentAsync,
        Continuation<TA, TB> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetValue(out var value)
                ? continuation(value)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided Try monad value and returns a Try monad.
    ///     If the Try monad contains a value, the continuation function is executed with the value.
    ///     If the Try monad contains an exception, the exception is returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="current">The Try monad containing the input value.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the Try monad.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the Try monad contains an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static Try<TB> RunTry<TA, TB>(
        Try<TA> current,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return current.TryGetValue(out var value)
                ? continuation(value)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided Try monad value and returns a Try monad.
    ///     If the Try monad contains a value, the continuation function is executed with the value.
    ///     If the Try monad contains an exception, the exception is returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="current">The Try monad containing the input value.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the Try monad.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the Try monad contains an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunTryAsync<TA, TB>(
        Try<TA> current,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided Try monad value and returns a Try monad.
    ///     If the Try monad contains a value, the continuation function is executed with the value.
    ///     If the Try monad contains an exception, the exception is returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="currentAsync">The asynchronous Try monad containing the input value.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the Try monad.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the Try monad contains an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunTryAsync<TA, TB>(
        Task<Try<TA>> currentAsync,
        Continuation<TA, Task<Try<TB>>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Executes a given continuation function with the provided Try monad value and returns a Try monad.
    ///     If the Try monad contains a value, the continuation function is executed with the value.
    ///     If the Try monad contains an exception, the exception is returned as a failed Try monad.
    /// </summary>
    /// <typeparam name="TA">The type of the input value for the Try monad.</typeparam>
    /// <typeparam name="TB">The type of the result value for the continuation function.</typeparam>
    /// <param name="currentAsync">The asynchronous Try monad containing the input value.</param>
    /// <param name="continuation">The continuation function to be executed with the result of the Try monad.</param>
    /// <returns>
    ///     A Try monad containing the result of the continuation function if it executes successfully.
    ///     If the Try monad contains an exception, it will be caught and returned as a failed Try monad.
    /// </returns>
    public static async Task<Try<TB>> RunTry<TA, TB>(
        Task<Try<TA>> currentAsync,
        Continuation<TA, Try<TB>> continuation
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            var current = await currentAsync.ConfigureAwait(false);

            return current.TryGetValue(out var value)
                ? continuation(value)
                : current.TryGetException(out var ex)
                    ? ex
                    : new Exception("#try_monad_internal_run_error#");
        }
        catch (Exception e)
        {
            return e;
        }
    }
}