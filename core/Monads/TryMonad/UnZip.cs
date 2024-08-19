using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryUnZip
{
    /// <summary>
    ///     Unzips a Try containing a tuple into a Try of a result type using a provided continuation function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="self">The Try containing a tuple.</param>
    /// <param name="continuation">A function that takes the tuple as input and returns a Try of the result type.</param>
    /// <returns>
    ///     If the Try contains a tuple, the function applies the continuation function to the tuple and returns the result.
    ///     If the Try contains an exception, the function returns the exception.
    ///     If the Try does not contain a tuple or an exception, the function throws an InvalidOperationException.
    /// </returns>
    public static Try<TC> UnZip<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Continuation<(TA, TB), TC> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? continuation(value)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Unzips a Try containing a tuple into a Try of a result type using an asynchronous continuation function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="self">The Try containing a tuple.</param>
    /// <param name="continuation">An asynchronous function that takes the tuple as input and returns a Try of the result type.</param>
    /// <returns>
    ///     If the Try contains a tuple, the function applies the continuation function to the tuple and returns the result.
    ///     If the Try contains an exception, the function returns the exception.
    ///     If the Try does not contain a tuple or an exception, the function throws an InvalidOperationException.
    /// </returns>
    public static async Task<Try<TC>> UnZipAsync<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Continuation<(TA, TB), Task<TC>> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Unzips a Try containing a tuple into a Try of a result type using an asynchronous continuation function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="selfAsync">An asynchronous Task containing a Try of a tuple.</param>
    /// <param name="continuation">An asynchronous function that takes the tuple as input and returns a Try of the result type.</param>
    /// <returns>
    ///     If the Try contains a tuple, the function applies the continuation function to the tuple and returns the result.
    ///     If the Try contains an exception, the function returns the exception.
    ///     If the Try does not contain a tuple or an exception, the function throws an InvalidOperationException.
    /// </returns>
    public static async Task<Try<TC>> UnZipAsync<TA, TB, TC>(
        this Task<Try<(TA, TB)>> selfAsync,
        Continuation<(TA, TB), Task<TC>> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? await continuation(value).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Unzips a Try containing a tuple into a Try of a result type using a provided continuation function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="selfAsync">An asynchronous Task containing a Try of a tuple.</param>
    /// <param name="continuation">A function that takes the tuple as input and returns a Try of the result type.</param>
    /// <returns>
    ///     If the Try contains a tuple, the function applies the continuation function to the tuple and returns the result.
    ///     If the Try contains an exception, the function returns the exception.
    ///     If the Try does not contain a tuple or an exception, the function throws an InvalidOperationException.
    /// </returns>
    public static async Task<Try<TC>> UnZip<TA, TB, TC>(
        this Task<Try<(TA, TB)>> selfAsync,
        Continuation<(TA, TB), TC> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? continuation(value)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Unzips a Try containing a tuple into a Try of a result type using a provided zipper function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="self">The Try containing a tuple.</param>
    /// <param name="zipper">A function that takes the tuple's items as input and returns a Try of the result type.</param>
    /// <returns>
    ///     If the Try contains a tuple, the function applies the zipper function to the tuple's items and returns the result.
    ///     If the Try contains an exception, the function returns the exception.
    ///     If the Try does not contain a tuple or an exception, the function throws an InvalidOperationException.
    /// </returns>
    public static Try<TC> UnZip<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Zipper<TA, TB, TC> zipper
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? zipper(value.Item1, value.Item2)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Unzips a Try containing a tuple into a Try of a result type using an asynchronous zipper function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="self">The Try containing a tuple.</param>
    /// <param name="zipper">
    ///     An asynchronous function that takes the tuple's items as input and returns a Try of the result
    ///     type.
    /// </param>
    /// <returns>
    ///     If the Try contains a tuple, the function applies the zipper function to the tuple's items and returns the result.
    ///     If the Try contains an exception, the function returns the exception.
    ///     If the Try does not contain a tuple or an exception, the function throws an InvalidOperationException.
    /// </returns>
    public static async Task<Try<TC>> UnZipAsync<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Zipper<TA, TB, Task<TC>> zipper
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? await zipper(value.Item1, value.Item2).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Unzips a Try containing a tuple into a Try of a result type using an asynchronous zipper function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="selfAsync">An asynchronous Task containing a Try of a tuple.</param>
    /// <param name="zipper">
    ///     An asynchronous function that takes the tuple's items as input and returns a Try of the result type.
    /// </param>
    /// <returns>
    ///     If the Try contains a tuple, the function applies the zipper function to the tuple's items and returns the result.
    ///     If the Try contains an exception, the function returns the exception.
    ///     If the Try does not contain a tuple or an exception, the function throws an InvalidOperationException.
    /// </returns>
    public static async Task<Try<TC>> UnZipAsync<TA, TB, TC>(
        this Task<Try<(TA, TB)>> selfAsync,
        Zipper<TA, TB, Task<TC>> zipper
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? await zipper(value.Item1, value.Item2).ConfigureAwait(false)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Unzips a Try containing a tuple into a Try of a result type using a provided zipper function.
    /// </summary>
    /// <typeparam name="TA">Type of the first item in the tuple.</typeparam>
    /// <typeparam name="TB">Type of the second item in the tuple.</typeparam>
    /// <typeparam name="TC">Type of the result.</typeparam>
    /// <param name="selfAsync">An asynchronous Task containing a Try of a tuple.</param>
    /// <param name="zipper">A function that takes the tuple's items as input and returns a Try of the result type.</param>
    /// <returns>
    ///     If the Try contains a tuple, the function applies the zipper function to the tuple's items and returns the result.
    ///     If the Try contains an exception, the function returns the exception.
    ///     If the Try does not contain a tuple or an exception, the function throws an InvalidOperationException.
    /// </returns>
    public static async Task<Try<TC>> UnZip<TA, TB, TC>(
        this Task<Try<(TA, TB)>> selfAsync,
        Zipper<TA, TB, TC> zipper
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetValue(out var value)
                ? zipper(value.Item1, value.Item2)
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception e)
        {
            return e;
        }
    }
}