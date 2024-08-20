using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionUnZip
{
    /// <summary>
    ///     Unzips a tuple-wrapped option into a new option using a provided continuation function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="self">The option containing a tuple.</param>
    /// <param name="continuation">A function that takes the tuple as input and returns a new value of type TC.</param>
    /// <returns>
    ///     An option containing the result of applying the continuation function to the tuple,
    ///     or Option.None if the input option is None or if an exception occurs during the execution.
    /// </returns>
    public static Option<TC> UnZip<TA, TB, TC>(
        this Option<(TA, TB)> self,
        OptionalContinuation<(TA, TB), TC> continuation
    )
    {
        try
        {
            return self.TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Unzips a tuple-wrapped option into a new option using an asynchronous continuation function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="self">The option containing a tuple.</param>
    /// <param name="continuation">
    ///     An asynchronous function that takes the tuple as input and returns a new value of type TC.
    ///     This function should be designed to handle asynchronous operations.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an option containing the result of applying the continuation function to the
    ///     tuple,
    ///     or Option.None if the input option is None or if an exception occurs during the execution.
    /// </returns>
    public static async Task<Option<TC>> UnZipAsync<TA, TB, TC>(
        this Option<(TA, TB)> self,
        OptionalContinuation<(TA, TB), Task<TC>> continuation
    )
    {
        try
        {
            return self.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Unzips a tuple-wrapped option into a new option using an asynchronous continuation function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="selfAsync">
    ///     An asynchronous task that returns an option containing a tuple.
    /// </param>
    /// <param name="continuation">
    ///     An asynchronous function that takes the tuple as input and returns a new value of type TC.
    ///     This function should be designed to handle asynchronous operations.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an option containing the result of applying the continuation function to the
    ///     tuple,
    ///     or Option.None if the input option is None or if an exception occurs during the execution.
    /// </returns>
    public static async Task<Option<TC>> UnZipAsync<TA, TB, TC>(
        this Task<Option<(TA, TB)>> selfAsync,
        OptionalContinuation<(TA, TB), Task<TC>> continuation
    )
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Unzips a tuple-wrapped option into a new option using a provided continuation function.
    ///     This function is designed to work with asynchronous tasks and options.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="selfAsync">
    ///     An asynchronous task that returns an option containing a tuple.
    /// </param>
    /// <param name="continuation">
    ///     A function that takes the tuple as input and returns a new value of type TC.
    ///     This function should be designed to handle synchronous operations.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an option containing the result of applying the continuation function to the
    ///     tuple,
    ///     or Option.None if the input option is None or if an exception occurs during the execution.
    /// </returns>
    public static async Task<Option<TC>> UnZip<TA, TB, TC>(
        this Task<Option<(TA, TB)>> selfAsync,
        OptionalContinuation<(TA, TB), TC> continuation
    )
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? continuation(value)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Unzips a tuple-wrapped option into a new option using a provided zipper function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="self">The option containing a tuple.</param>
    /// <param name="zipper">
    ///     A function that takes the tuple's first and second items as input and returns a new value of type TC.
    ///     This function should be designed to handle synchronous operations.
    /// </param>
    /// <returns>
    ///     An option containing the result of applying the zipper function to the tuple's items,
    ///     or Option.None if the input option is None or if an exception occurs during the execution.
    /// </returns>
    public static Option<TC> UnZip<TA, TB, TC>(
        this Option<(TA, TB)> self,
        Zipper<TA, TB, TC> zipper
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? zipper(value.Item1, value.Item2)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Unzips a tuple-wrapped option into a new option using an asynchronous zipper function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="self">The option containing a tuple.</param>
    /// <param name="zipper">
    ///     A function that takes the tuple's first and second items as input and returns a new value of type TC.
    ///     This function should be designed to handle asynchronous operations.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an option containing the result of applying the zipper function to the tuple's
    ///     items,
    ///     or Option.None if the input option is None or if an exception occurs during the execution.
    /// </returns>
    public static async Task<Option<TC>> UnZipAsync<TA, TB, TC>(
        this Option<(TA, TB)> self,
        Zipper<TA, TB, Task<TC>> zipper
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? await zipper(value.Item1, value.Item2).ConfigureAwait(false)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Unzips a tuple-wrapped option into a new option using an asynchronous zipper function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="selfAsync">An asynchronous task that returns an option containing a tuple.</param>
    /// <param name="zipper">
    ///     A function that takes the tuple's first and second items as input and returns a new value of type TC.
    ///     This function should be designed to handle asynchronous operations.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an option containing the result of applying the zipper function to the tuple's
    ///     items, or Option.None if the input option is None or if an exception occurs during the execution.
    /// </returns>
    public static async Task<Option<TC>> UnZipAsync<TA, TB, TC>(
        this Task<Option<(TA, TB)>> selfAsync,
        Zipper<TA, TB, Task<TC>> zipper
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? await zipper(value.Item1, value.Item2).ConfigureAwait(false)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Unzips a tuple-wrapped option into a new option using a provided zipper function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="selfAsync">An asynchronous task that returns an option containing a tuple.</param>
    /// <param name="zipper">
    ///     A function that takes the tuple's first and second items as input and returns a new value of type TC.
    ///     This function should be designed to handle synchronous operations.
    /// </param>
    /// <returns>
    ///     An asynchronous task that returns an option containing the result of applying the zipper function to the tuple's
    ///     items, or Option.None if the input option is None or if an exception occurs during the execution.
    /// </returns>
    public static async Task<Option<TC>> UnZip<TA, TB, TC>(
        this Task<Option<(TA, TB)>> selfAsync,
        Zipper<TA, TB, TC> zipper
    )
        where TA : notnull
        where TB : notnull
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? zipper(value.Item1, value.Item2)
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }
}