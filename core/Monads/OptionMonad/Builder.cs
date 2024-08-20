using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class Option
{
    /// <summary>
    ///     Creates an instance of <see cref="Option{TA}" /> containing the specified value.
    /// </summary>
    /// <typeparam name="TA">The type of the value.</typeparam>
    /// <param name="value">The value to be wrapped in an <see cref="Option{TA}" />.</param>
    /// <returns>An instance of <see cref="Option{TA}" /> containing the specified value.</returns>
    public static Option<TA> Some<TA>(TA value)
    {
        return value;
    }

    /// <summary>
    ///     Creates an instance of <see cref="Option{TA}" /> representing a lack of value.
    /// </summary>
    /// <typeparam name="TA">The type of the value.</typeparam>
    /// <returns>An instance of <see cref="Option{TA}" /> representing a lack of value.</returns>
    public static Option<TA> None<TA>()
    {
        return default;
    }

    /// <summary>
    ///     Executes the provided effect and returns an instance of <see cref="Option{TA}" /> containing the result.
    ///     If the effect throws an exception, it returns an instance of <see cref="Option{TA}" /> representing a lack of
    ///     value.
    /// </summary>
    /// <typeparam name="TA">The type of the value returned by the effect.</typeparam>
    /// <param name="effect">The effect to be executed.</param>
    /// <returns>
    ///     An instance of <see cref="Option{TA}" /> containing the result of the effect if it does not throw an exception.
    ///     Otherwise, an instance of <see cref="Option{TA}" /> representing a lack of value.
    /// </returns>
    public static Option<TA> Effect<TA>(OptionalEffect<TA> effect)
    {
        try
        {
            return effect();
        }
        catch (Exception)
        {
            return None<TA>();
        }
    }

    /// <summary>
    ///     Executes the provided asynchronous task and returns an instance of <see cref="Option{TA}" /> containing the result.
    ///     If the asynchronous task throws an exception, it returns an instance of <see cref="Option{TA}" /> representing a
    ///     lack of
    ///     value.
    /// </summary>
    /// <typeparam name="TA">The type of the value returned by the asynchronous task.</typeparam>
    /// <param name="asyncTask">The asynchronous task to be executed.</param>
    /// <returns>
    ///     An instance of <see cref="Option{TA}" /> containing the result of the asynchronous task if it does not throw an
    ///     exception.
    ///     Otherwise, an instance of <see cref="Option{TA}" /> representing a lack of value.
    /// </returns>
    public static async Task<Option<TA>> AsyncValue<TA>(ValueTask<TA> asyncTask)
    {
        try
        {
            return await asyncTask.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return None<TA>();
        }
    }

    /// <summary>
    ///     Executes the provided asynchronous effect and returns an instance of <see cref="Option{TA}" /> containing the
    ///     result.
    ///     If the asynchronous effect throws an exception, it returns an instance of <see cref="Option{TA}" /> representing a
    ///     lack of value.
    /// </summary>
    /// <typeparam name="TA">The type of the value returned by the asynchronous effect.</typeparam>
    /// <param name="asyncEffect">The asynchronous effect to be executed.</param>
    /// <returns>
    ///     An instance of <see cref="Option{TA}" /> containing the result of the asynchronous effect if it does not throw an
    ///     exception.
    ///     Otherwise, an instance of <see cref="Option{TA}" /> representing a lack of value.
    /// </returns>
    public static async Task<Option<TA>> Async<TA>(
        OptionalEffect<Task<TA>> asyncEffect
    )
    {
        try
        {
            return await asyncEffect().ConfigureAwait(false);
        }
        catch (Exception)
        {
            return None<TA>();
        }
    }

    /// <summary>
    ///     Executes the provided asynchronous task that returns an instance of <see cref="Option{TA}" /> and returns a new
    ///     instance of <see cref="Option{TA}" /> containing the result.
    ///     If the asynchronous task throws an exception, it returns an instance of <see cref="Option{TA}" /> representing a
    ///     lack of value.
    /// </summary>
    /// <typeparam name="TA">The type of the value returned by the asynchronous task.</typeparam>
    /// <param name="asyncTask">The asynchronous task to be executed.</param>
    /// <returns>
    ///     An instance of <see cref="Option{TA}" /> containing the result of the asynchronous task if it does not throw an
    ///     exception.
    ///     Otherwise, an instance of <see cref="Option{TA}" /> representing a lack of value.
    /// </returns>
    public static async Task<Option<TA>> Async<TA>(Task<Option<TA>> asyncTask)
    {
        try
        {
            return (await asyncTask.ConfigureAwait(false)).TryGetValue(out var value)
                ? value
                : None<TA>();
        }
        catch (Exception)
        {
            return None<TA>();
        }
    }
}