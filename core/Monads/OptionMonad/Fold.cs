using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionFold
{
    /// <summary>
    ///     Applies a function to the value inside an Option, or a default value if the Option is None.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the Option.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="option">The Option to apply the function to.</param>
    /// <param name="none">The function to apply if the Option is None.</param>
    /// <param name="some">The function to apply if the Option is Some.</param>
    /// <returns>
    ///     The result of applying the function to the value inside the Option, or the default value if the Option is
    ///     None.
    /// </returns>
    /// <exception cref="Exception">
    ///     Any exception thrown by the provided functions will be caught and the default value will be
    ///     returned.
    /// </exception>
    public static TB Fold<TA, TB>(
        this Option<TA> option,
        Effect<TB> none,
        Continuation<TA, TB> some)
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return option.TryGetValue(out var a)
                ? some(a)
                : none();
        }
        catch (Exception)
        {
            return none();
        }
    }

    /// <summary>
    ///     Applies a function to the value inside an Option, or an asynchronous default value if the Option is None.
    ///     If the provided functions throw an exception, the default value will be returned.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the Option.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="option">The Option to apply the function to.</param>
    /// <param name="none">An asynchronous function to apply if the Option is None.</param>
    /// <param name="some">A function to apply if the Option is Some.</param>
    /// <returns>
    ///     The result of applying the function to the value inside the Option, or the default value if the Option is None.
    ///     The result is obtained asynchronously.
    /// </returns>
    public static async Task<TB> FoldAsync<TA, TB>(
        this Option<TA> option,
        Effect<Task<TB>> none,
        Continuation<TA, TB> some
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return option.TryGetValue(out var a)
                ? some(a)
                : await none().ConfigureAwait(false);
        }
        catch (Exception)
        {
            return await none().ConfigureAwait(false);
        }
    }

    /// <summary>
    ///     Applies a function to the value inside an Option, or an asynchronous default value if the Option is None.
    ///     If the provided functions throw an exception, the default value will be returned.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the Option.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="option">The Option to apply the function to.</param>
    /// <param name="none">An asynchronous function to apply if the Option is None.</param>
    /// <param name="some">A function to apply if the Option is Some, which returns a Task.</param>
    /// <returns>
    ///     The result of applying the function to the value inside the Option, or the default value if the Option is None.
    ///     The result is obtained asynchronously.
    /// </returns>
    public static async Task<TB> FoldAsync<TA, TB>(
        this Option<TA> option,
        Effect<TB> none,
        Continuation<TA, Task<TB>> some
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return option.TryGetValue(out var a)
                ? await some(a).ConfigureAwait(false)
                : none();
        }
        catch (Exception)
        {
            return none();
        }
    }

    /// <summary>
    ///     Applies a function to the value inside an Option, or an asynchronous default value if the Option is None.
    ///     If the provided functions throw an exception, the default value will be returned.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the Option.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="option">The Option to apply the function to.</param>
    /// <param name="none">An asynchronous function to apply if the Option is None.</param>
    /// <param name="some">A function to apply if the Option is Some, which returns a Task.</param>
    /// <returns>
    ///     The result of applying the function to the value inside the Option, or the default value if the Option is None.
    ///     The result is obtained asynchronously.
    /// </returns>
    public static async Task<TB> FoldAsync<TA, TB>(
        this Option<TA> option,
        Effect<Task<TB>> none,
        Continuation<TA, Task<TB>> some
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return option.TryGetValue(out var a)
                ? await some(a).ConfigureAwait(false)
                : await none().ConfigureAwait(false);
        }
        catch (Exception)
        {
            return await none().ConfigureAwait(false);
        }
    }


    /// <summary>
    ///     Applies a function to the value inside an asynchronous Option, or a default value if the Option is None.
    ///     If the provided functions throw an exception, the default value will be returned.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the Option.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="optionAsync">The asynchronous Option to apply the function to.</param>
    /// <param name="none">A function to apply if the Option is None.</param>
    /// <param name="some">A function to apply if the Option is Some.</param>
    /// <returns>
    ///     The result of applying the function to the value inside the Option, or the default value if the Option is None.
    ///     The result is obtained asynchronously.
    /// </returns>
    public static async Task<TB> Fold<TA, TB>(
        this Task<Option<TA>> optionAsync,
        Effect<TB> none,
        Continuation<TA, TB> some
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return (await optionAsync.ConfigureAwait(false)).Fold(none, some);
        }
        catch (Exception)
        {
            return none();
        }
    }

    /// <summary>
    ///     Applies a function to the value inside an asynchronous Option, or an asynchronous default value if the Option is
    ///     None.
    ///     If the provided functions throw an exception, the default value will be returned.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the Option.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="optionAsync">The asynchronous Option to apply the function to.</param>
    /// <param name="none">An asynchronous function to apply if the Option is None.</param>
    /// <param name="some">A function to apply if the Option is Some.</param>
    /// <returns>
    ///     The result of applying the function to the value inside the Option, or the default value if the Option is None.
    ///     The result is obtained asynchronously.
    /// </returns>
    public static async Task<TB> FoldAsync<TA, TB>(
        this Task<Option<TA>> optionAsync,
        Effect<Task<TB>> none,
        Continuation<TA, TB> some
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return (await optionAsync.ConfigureAwait(false)).TryGetValue(out var a)
                ? some(a)
                : await none().ConfigureAwait(false);
        }
        catch (Exception)
        {
            return await none().ConfigureAwait(false);
        }
    }

    /// <summary>
    ///     Applies a function to the value inside an asynchronous Option, or an asynchronous default value if the Option is
    ///     None.
    ///     If the provided functions throw an exception, the default value will be returned.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the Option.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="optionAsync">The asynchronous Option to apply the function to.</param>
    /// <param name="none">An asynchronous function to apply if the Option is None.</param>
    /// <param name="some">A function to apply if the Option is Some, which returns a Task.</param>
    /// <returns>
    ///     The result of applying the function to the value inside the Option, or the default value if the Option is None.
    ///     The result is obtained asynchronously.
    /// </returns>
    public static async Task<TB> FoldAsync<TA, TB>(
        this Task<Option<TA>> optionAsync,
        Effect<TB> none,
        Continuation<TA, Task<TB>> some
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return (await optionAsync.ConfigureAwait(false)).TryGetValue(out var a)
                ? await some(a).ConfigureAwait(false)
                : none();
        }
        catch (Exception)
        {
            return none();
        }
    }

    /// <summary>
    ///     Applies a function to the value inside an asynchronous Option, or an asynchronous default value if the Option is
    ///     None.
    ///     If the provided functions throw an exception, the default value will be returned.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the Option.</typeparam>
    /// <typeparam name="TB">The type of the result.</typeparam>
    /// <param name="optionAsync">The asynchronous Option to apply the function to.</param>
    /// <param name="none">An asynchronous function to apply if the Option is None.</param>
    /// <param name="some">A function to apply if the Option is Some, which returns a Task.</param>
    /// <returns>
    ///     The result of applying the function to the value inside the Option, or the default value if the Option is None.
    ///     The result is obtained asynchronously.
    /// </returns>
    public static async Task<TB> Fold<TA, TB>(
        this Task<Option<TA>> optionAsync,
        Effect<Task<TB>> none,
        Continuation<TA, Task<TB>> some
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return (await optionAsync.ConfigureAwait(false)).TryGetValue(out var a)
                ? await some(a).ConfigureAwait(false)
                : await none().ConfigureAwait(false);
        }
        catch (Exception)
        {
            return await none().ConfigureAwait(false);
        }
    }
}