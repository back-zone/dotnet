using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherUnZip
{
    /// <summary>
    ///     Unzips a tuple from an Either instance and applies a continuation function to the unzipped values.
    ///     If the Either instance contains a right value, the continuation function is applied to the unzipped values.
    ///     If the Either instance contains a left value, the left value is returned as is.
    ///     If an exception occurs during the unzipping or continuation function execution, an InvalidOperationException is
    ///     thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either instance.</typeparam>
    /// <typeparam name="TR">The type of the first value in the tuple.</typeparam>
    /// <typeparam name="TR1">The type of the second value in the tuple.</typeparam>
    /// <typeparam name="TR2">The type of the result of the continuation function.</typeparam>
    /// <param name="self">The Either instance containing the tuple.</param>
    /// <param name="continuation">The function to apply to the unzipped values.</param>
    /// <returns>
    ///     If the Either instance contains a right value, the result of applying the continuation function to the unzipped
    ///     values.
    ///     If the Either instance contains a left value, the left value.
    ///     If an exception occurs during the unzipping or continuation function execution, an InvalidOperationException is
    ///     thrown.
    /// </returns>
    public static Either<TL, TR2> UnZip<TL, TR, TR1, TR2>(
        this Either<TL, (TR, TR1)> self,
        Continuation<(TR, TR1), TR2> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? continuation(right)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    /// <summary>
    ///     Unzips a tuple from an Either instance and applies an asynchronous continuation function to the unzipped values.
    ///     If the Either instance contains a right value, the continuation function is applied to the unzipped values.
    ///     If the Either instance contains a left value, the left value is returned as is.
    ///     If an exception occurs during the unzipping or continuation function execution, an InvalidOperationException is
    ///     thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either instance.</typeparam>
    /// <typeparam name="TR">The type of the first value in the tuple.</typeparam>
    /// <typeparam name="TR1">The type of the second value in the tuple.</typeparam>
    /// <typeparam name="TR2">The type of the result of the continuation function.</typeparam>
    /// <param name="self">The Either instance containing the tuple.</param>
    /// <param name="continuation">The asynchronous function to apply to the unzipped values.</param>
    /// <returns>
    ///     If the Either instance contains a right value, the result of applying the continuation function to the unzipped
    ///     values.
    ///     If the Either instance contains a left value, the left value.
    ///     If an exception occurs during the unzipping or continuation function execution, an InvalidOperationException is
    ///     thrown.
    /// </returns>
    public static async Task<Either<TL, TR2>> UnZipAsync<TL, TR, TR1, TR2>(
        this Either<TL, (TR, TR1)> self,
        Continuation<(TR, TR1), Task<TR2>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? await continuation(right).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    /// <summary>
    ///     Unzips a tuple from an asynchronous Either instance and applies an asynchronous continuation function to the
    ///     unzipped values.
    ///     If the Either instance contains a right value, the continuation function is applied to the unzipped values.
    ///     If the Either instance contains a left value, the left value is returned as is.
    ///     If an exception occurs during the unzipping or continuation function execution, an InvalidOperationException is
    ///     thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either instance.</typeparam>
    /// <typeparam name="TR">The type of the first value in the tuple.</typeparam>
    /// <typeparam name="TR1">The type of the second value in the tuple.</typeparam>
    /// <typeparam name="TR2">The type of the result of the continuation function.</typeparam>
    /// <param name="selfAsync">The asynchronous Either instance containing the tuple.</param>
    /// <param name="continuation">The asynchronous function to apply to the unzipped values.</param>
    /// <returns>
    ///     If the Either instance contains a right value, the result of applying the continuation function to the unzipped
    ///     values.
    ///     If the Either instance contains a left value, the left value.
    ///     If an exception occurs during the unzipping or continuation function execution, an InvalidOperationException is
    ///     thrown.
    /// </returns>
    public static async Task<Either<TL, TR2>> UnZipAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, (TR, TR1)>> selfAsync,
        Continuation<(TR, TR1), Task<TR2>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetRight(out var right)
                ? await continuation(right).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    /// <summary>
    ///     Unzips a tuple from an asynchronous Either instance and applies a continuation function to the unzipped values.
    ///     If the Either instance contains a right value, the continuation function is applied to the unzipped values.
    ///     If the Either instance contains a left value, the left value is returned as is.
    ///     If an exception occurs during the unzipping or continuation function execution, an InvalidOperationException is
    ///     thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either instance.</typeparam>
    /// <typeparam name="TR">The type of the first value in the tuple.</typeparam>
    /// <typeparam name="TR1">The type of the second value in the tuple.</typeparam>
    /// <typeparam name="TR2">The type of the result of the continuation function.</typeparam>
    /// <param name="selfAsync">The asynchronous Either instance containing the tuple.</param>
    /// <param name="continuation">The function to apply to the unzipped values.</param>
    /// <returns>
    ///     If the Either instance contains a right value, the result of applying the continuation function to the unzipped
    ///     values.
    ///     If the Either instance contains a left value, the left value.
    ///     If an exception occurs during the unzipping or continuation function execution, an InvalidOperationException is
    ///     thrown.
    /// </returns>
    public static async Task<Either<TL, TR2>> UnZip<TL, TR, TR1, TR2>(
        this Task<Either<TL, (TR, TR1)>> selfAsync,
        Continuation<(TR, TR1), TR2> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetRight(out var right)
                ? continuation(right)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    /// <summary>
    ///     Unzips a tuple from an Either instance and applies a zipper function to the unzipped values.
    ///     If the Either instance contains a right value, the zipper function is applied to the unzipped values.
    ///     If the Either instance contains a left value, the left value is returned as is.
    ///     If an exception occurs during the unzipping or zipper function execution, an InvalidOperationException is thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either instance.</typeparam>
    /// <typeparam name="TR">The type of the first value in the tuple.</typeparam>
    /// <typeparam name="TR1">The type of the second value in the tuple.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">The Either instance containing the tuple.</param>
    /// <param name="zipper">The function to apply to the unzipped values.</param>
    /// <returns>
    ///     If the Either instance contains a right value, the result of applying the zipper function to the unzipped values.
    ///     If the Either instance contains a left value, the left value.
    ///     If an exception occurs during the unzipping or zipper function execution, an InvalidOperationException is thrown.
    /// </returns>
    public static Either<TL, TR2> UnZip<TL, TR, TR1, TR2>(
        this Either<TL, (TR, TR1)> self,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? zipper(right.Item1, right.Item2)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    /// <summary>
    ///     Unzips a tuple from an Either instance and applies an asynchronous zipper function to the unzipped values.
    ///     If the Either instance contains a right value, the zipper function is applied to the unzipped values.
    ///     If the Either instance contains a left value, the left value is returned as is.
    ///     If an exception occurs during the unzipping or zipper function execution, an InvalidOperationException is thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either instance.</typeparam>
    /// <typeparam name="TR">The type of the first value in the tuple.</typeparam>
    /// <typeparam name="TR1">The type of the second value in the tuple.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">The Either instance containing the tuple.</param>
    /// <param name="zipper">The asynchronous function to apply to the unzipped values.</param>
    /// <returns>
    ///     If the Either instance contains a right value, the result of applying the zipper function to the unzipped values.
    ///     If the Either instance contains a left value, the left value.
    ///     If an exception occurs during the unzipping or zipper function execution, an InvalidOperationException is thrown.
    /// </returns>
    public static async Task<Either<TL, TR2>> UnZipAsync<TL, TR, TR1, TR2>(
        this Either<TL, (TR, TR1)> self,
        Zipper<TR, TR1, Task<TR2>> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? await zipper(right.Item1, right.Item2).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    /// <summary>
    ///     Unzips a tuple from an asynchronous Either instance and applies an asynchronous zipper function to the unzipped
    ///     values.
    ///     If the Either instance contains a right value, the zipper function is applied to the unzipped values.
    ///     If the Either instance contains a left value, the left value is returned as is.
    ///     If an exception occurs during the unzipping or zipper function execution, an InvalidOperationException is thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either instance.</typeparam>
    /// <typeparam name="TR">The type of the first value in the tuple.</typeparam>
    /// <typeparam name="TR1">The type of the second value in the tuple.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="selfAsync">The asynchronous Either instance containing the tuple.</param>
    /// <param name="zipper">The asynchronous function to apply to the unzipped values.</param>
    /// <returns>
    ///     If the Either instance contains a right value, the result of applying the zipper function to the unzipped values.
    ///     If the Either instance contains a left value, the left value.
    ///     If an exception occurs during the unzipping or zipper function execution, an InvalidOperationException is thrown.
    /// </returns>
    public static async Task<Either<TL, TR2>> UnZipAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, (TR, TR1)>> selfAsync,
        Zipper<TR, TR1, Task<TR2>> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetRight(out var right)
                ? await zipper(right.Item1, right.Item2).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }

    /// <summary>
    ///     Unzips a tuple from an asynchronous Either instance and applies a zipper function to the unzipped values.
    ///     If the Either instance contains a right value, the zipper function is applied to the unzipped values.
    ///     If the Either instance contains a left value, the left value is returned as is.
    ///     If an exception occurs during the unzipping or zipper function execution, an InvalidOperationException is thrown.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either instance.</typeparam>
    /// <typeparam name="TR">The type of the first value in the tuple.</typeparam>
    /// <typeparam name="TR1">The type of the second value in the tuple.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="selfAsync">The asynchronous Either instance containing the tuple.</param>
    /// <param name="zipper">The asynchronous function to apply to the unzipped values.</param>
    /// <returns>
    ///     If the Either instance contains a right value, the result of applying the zipper function to the unzipped values.
    ///     If the Either instance contains a left value, the left value.
    ///     If an exception occurs during the unzipping or zipper function execution, an InvalidOperationException is thrown.
    /// </returns>
    public static async Task<Either<TL, TR2>> UnZip<TL, TR, TR1, TR2>(
        this Task<Either<TL, (TR, TR1)>> selfAsync,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            var self = await selfAsync.ConfigureAwait(false);

            return self.TryGetRight(out var right)
                ? zipper(right.Item1, right.Item2)
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
        catch (Exception)
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_value_or_exception_in_zip#");
        }
    }
}