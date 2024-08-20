using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

internal static class OptionRuntime
{
    /// <summary>
    ///     Executes a given continuation function with a provided value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="value">The input value.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>An Option of the result of the continuation function. If an exception is thrown, returns None.</returns>
    public static Option<TB> RunValue<TA, TB>(
        TA value,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return continuation(value);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with a provided value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="value">The input value.</param>
    /// <param name="continuation">
    ///     The function to be executed with the input value. This function should return an Option of
    ///     type TB.
    /// </param>
    /// <returns>
    ///     An Option of the result of the continuation function. If an exception is thrown, returns None.
    ///     If the continuation function returns an Option of type TB, the result will be an Option of type TB.
    ///     If the continuation function returns a value of type TB, the result will be an Option of type TB, where the value
    ///     is wrapped in Some.
    /// </returns>
    public static Option<TB> RunValue<TA, TB>(
        TA value,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return continuation(value);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an asynchronously provided value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="asyncValue">An asynchronous task that provides the input value.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    /// </returns>
    public static async Task<Option<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return continuation(await asyncValue.ConfigureAwait(false));
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an asynchronously provided value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="asyncValue">An asynchronous task that provides the input value.</param>
    /// <param name="continuation">
    ///     The function to be executed with the input value. This function should return a Task of type
    ///     TB.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    ///     If the continuation function returns a Task of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        try
        {
            return await continuation(await asyncValue.ConfigureAwait(false)).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an asynchronously provided value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="asyncValue">An asynchronous task that provides the input value.</param>
    /// <param name="continuation">
    ///     The function to be executed with the input value. This function should return an Option of type TB.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    ///     If the continuation function returns an Option of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunValue<TA, TB>(
        Task<TA> asyncValue,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return continuation(await asyncValue.ConfigureAwait(false));
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an asynchronously provided value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="asyncValue">An asynchronous task that provides the input value.</param>
    /// <param name="continuation">
    ///     The function to be executed with the input value. This function should return a Task of type Option&lt;TB&gt;.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    ///     If the continuation function returns a Task of type Option&lt;TB&gt;, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunAsyncValue<TA, TB>(
        Task<TA> asyncValue,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        try
        {
            return await continuation(await asyncValue.ConfigureAwait(false)).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given effect function and continuation function with a provided value, returning an Option of the
    ///     result.
    ///     If the effect function or continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="effect">The effect function to be executed with the input value.</param>
    /// <param name="continuation">The function to be executed with the input value after the effect function.</param>
    /// <returns>
    ///     An Option of the result of the continuation function. If an exception is thrown during the effect function or
    ///     continuation function,
    ///     returns None.
    /// </returns>
    public static Option<TB> RunEffect<TA, TB>(
        OptionalEffect<TA> effect,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return continuation(effect());
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given effect function and continuation function with a provided value, returning an Option of the
    ///     result.
    ///     If the effect function or continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="effect">The effect function to be executed with the input value.</param>
    /// <param name="continuation">The function to be executed with the input value after the effect function.</param>
    /// <returns>
    ///     An Option of the result of the continuation function. If an exception is thrown during the effect function or
    ///     continuation function, returns None.
    ///     If the continuation function returns an Option of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static Option<TB> RunEffect<TA, TB>(
        OptionalEffect<TA> effect,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return continuation(effect());
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given effect function and continuation function with a provided value, returning an Option of the
    ///     result.
    ///     If the effect function or continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="effect">The effect function to be executed with the input value.</param>
    /// <param name="continuation">The function to be executed with the input value after the effect function.</param>
    /// <returns>
    ///     An Option of the result of the continuation function. If an exception is thrown during the effect function or
    ///     continuation function, returns None.
    ///     If the continuation function returns a Task of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunEffectAsync<TA, TB>(
        OptionalEffect<TA> effect,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        try
        {
            return await continuation(effect()).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given effect function and continuation function with a provided value, returning an Option of the
    ///     result.
    ///     If the effect function or continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="effect">The effect function to be executed with the input value.</param>
    /// <param name="continuation">
    ///     The function to be executed with the input value after the effect function. This function
    ///     should return a Task of type Option&lt;TB&gt;.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function. If an exception is thrown
    ///     during the effect function or continuation function,
    ///     returns None.
    ///     If the continuation function returns a Task of type Option&lt;TB&gt;, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunEffectAsync<TA, TB>(
        OptionalEffect<TA> effect,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        try
        {
            return await continuation(effect()).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an Option-wrapped value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="current">The Option-wrapped input value.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     An Option of the result of the continuation function. If an exception is thrown, returns None.
    ///     If the input Option is not in a Some state, returns None.
    ///     If the continuation function returns a value of type TB, the result will be an Option of type TB, where the value
    ///     is wrapped in Some.
    /// </returns>
    public static Option<TB> RunOption<TA, TB>(
        Option<TA> current,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return current.TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an Option-wrapped value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="current">The Option-wrapped input value.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     An Option of the result of the continuation function. If an exception is thrown, returns None.
    ///     If the input Option is not in a Some state, returns None.
    ///     If the continuation function returns an Option of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static Option<TB> RunOption<TA, TB>(
        Option<TA> current,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return current.TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an Option-wrapped value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="current">The Option-wrapped input value.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    ///     If the input Option is not in a Some state, returns None.
    ///     If the continuation function returns a Task of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Option<TA> current,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        try
        {
            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an Option-wrapped value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="current">The Option-wrapped input value.</param>
    /// <param name="continuation">The function to be executed with the input value.</param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    ///     If the input Option is not in a Some state, returns None.
    ///     If the continuation function returns an Option of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Task<Option<TA>> current,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return (await current.ConfigureAwait(false)).TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an Option-wrapped value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="currentAsync">The asynchronous Task-wrapped Option-wrapped input value.</param>
    /// <param name="continuation">The asynchronous function to be executed with the input value.</param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    ///     If the input Option is not in a Some state, returns None.
    ///     If the continuation function returns a Task of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Task<Option<TA>> currentAsync,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        try
        {
            return (await currentAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an Option-wrapped value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="currentAsync">The asynchronous Task-wrapped Option-wrapped input value.</param>
    /// <param name="continuation">The asynchronous function to be executed with the input value.</param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    ///     If the input Option is not in a Some state, returns None.
    ///     If the continuation function returns a value of type TB, the result will be an Option of type TB, where the value
    ///     is wrapped in Some.
    /// </returns>
    public static async Task<Option<TB>> RunOption<TA, TB>(
        Task<Option<TA>> currentAsync,
        OptionalContinuation<TA, TB> continuation
    )
    {
        try
        {
            return (await currentAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an Option-wrapped value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="currentAsync">The asynchronous Task-wrapped Option-wrapped input value.</param>
    /// <param name="continuation">The asynchronous function to be executed with the input value.</param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    ///     If the input Option is not in a Some state, returns None.
    ///     If the continuation function returns an Option of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunOption<TA, TB>(
        Task<Option<TA>> currentAsync,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        try
        {
            return (await currentAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an Option-wrapped value and returns an Option of the result.
    ///     If the continuation function throws an exception, returns None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="current">The Option-wrapped input value.</param>
    /// <param name="continuation">The asynchronous function to be executed with the input value.</param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    ///     If the input Option is not in a Some state, returns None.
    ///     If the continuation function returns an Option of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </returns>
    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Option<TA> current,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        try
        {
            return current.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }

    /// <summary>
    ///     Executes a given continuation function with an Option-wrapped value and returns an Option of the result.
    ///     If the input Option is not in a Some state, returns None.
    ///     If the continuation function returns an Option of type TB, the result will be an Option of type TB.
    ///     If the continuation function throws an exception, the result will be an Option of type TB, where the value is
    ///     wrapped in None.
    /// </summary>
    /// <typeparam name="TA">The type of the input value.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="currentAsync">The asynchronous Task-wrapped Option-wrapped input value.</param>
    /// <param name="continuation">The asynchronous function to be executed with the input value.</param>
    /// <returns>
    ///     An asynchronous task that returns an Option of the result of the continuation function.
    ///     If an exception is thrown, returns None.
    /// </returns>
    public static async Task<Option<TB>> RunOptionAsync<TA, TB>(
        Task<Option<TA>> currentAsync,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        try
        {
            return (await currentAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TB>();
        }
        catch (Exception)
        {
            return Option.None<TB>();
        }
    }
}