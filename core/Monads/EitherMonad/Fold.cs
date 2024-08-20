using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherFold
{
    /// <summary>
    ///     Folds an Either into a single value using the provided handlers for left and right cases.
    /// </summary>
    /// <typeparam name="TL">Type of the left value.</typeparam>
    /// <typeparam name="TR">Type of the right value.</typeparam>
    /// <typeparam name="TF">Type of the result.</typeparam>
    /// <param name="self">The Either to fold.</param>
    /// <param name="leftHandler">A function to handle the left case.</param>
    /// <param name="rightHandler">A function to handle the right case.</param>
    /// <returns>The result of applying the appropriate handler to the Either's value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the Either does not contain a left or right value.</exception>
    public static TF Fold<TL, TR, TF>(
        this Either<TL, TR> self,
        Continuation<TL, TF> leftHandler,
        Continuation<TR, TF> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? rightHandler(right)
                : self.TryGetLeft(out var left)
                    ? leftHandler(left)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? leftHandler(left)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously folds an Either into a single value using the provided handlers for left and right cases.
    /// </summary>
    /// <typeparam name="TL">Type of the left value.</typeparam>
    /// <typeparam name="TR">Type of the right value.</typeparam>
    /// <typeparam name="TF">Type of the result.</typeparam>
    /// <param name="self">The Either to fold.</param>
    /// <param name="leftHandler">A function to handle the left case, returning a Task of TF.</param>
    /// <param name="rightHandler">A function to handle the right case, returning a Task of TF.</param>
    /// <returns>
    ///     The result of applying the appropriate handler to the Either's value.
    ///     If the Either contains a right value, the rightHandler is awaited and its result is returned.
    ///     If the Either contains a left value, the leftHandler is awaited and its result is returned.
    ///     If the Either does not contain a left or right value, an InvalidOperationException is thrown.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the Either does not contain a left or right value.</exception>
    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Either<TL, TR> self,
        Continuation<TL, Task<TF>> leftHandler,
        Continuation<TR, Task<TF>> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? await rightHandler(right).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? await leftHandler(left).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? await leftHandler(left).ConfigureAwait(false)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously folds an Either into a single value using the provided handlers for left and right cases.
    /// </summary>
    /// <typeparam name="TL">Type of the left value.</typeparam>
    /// <typeparam name="TR">Type of the right value.</typeparam>
    /// <typeparam name="TF">Type of the result.</typeparam>
    /// <param name="self">The Either to fold.</param>
    /// <param name="leftHandler">A function to handle the left case, returning a Task of TF.</param>
    /// <param name="rightHandler">A function to handle the right case, returning TF.</param>
    /// <returns>
    ///     The result of applying the appropriate handler to the Either's value.
    ///     If the Either contains a right value, the rightHandler is invoked and its result is returned.
    ///     If the Either contains a left value, the leftHandler is awaited and its result is returned.
    ///     If the Either does not contain a left or right value, an InvalidOperationException is thrown.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the Either does not contain a left or right value.</exception>
    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Either<TL, TR> self,
        Continuation<TL, Task<TF>> leftHandler,
        Continuation<TR, TF> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? rightHandler(right)
                : self.TryGetLeft(out var left)
                    ? await leftHandler(left).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? await leftHandler(left).ConfigureAwait(false)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously folds an Either into a single value using the provided handlers for left and right cases.
    /// </summary>
    /// <typeparam name="TL">Type of the left value.</typeparam>
    /// <typeparam name="TR">Type of the right value.</typeparam>
    /// <typeparam name="TF">Type of the result.</typeparam>
    /// <param name="self">The Either to fold.</param>
    /// <param name="leftHandler">A function to handle the left case, returning TF.</param>
    /// <param name="rightHandler">A function to handle the right case, returning a Task of TF.</param>
    /// <returns>
    ///     The result of applying the appropriate handler to the Either's value.
    ///     If the Either contains a right value, the rightHandler is awaited and its result is returned.
    ///     If the Either contains a left value, the leftHandler is invoked and its result is returned.
    ///     If the Either does not contain a left or right value, an InvalidOperationException is thrown.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the Either does not contain a left or right value.</exception>
    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Either<TL, TR> self,
        Continuation<TL, TF> leftHandler,
        Continuation<TR, Task<TF>> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? await rightHandler(right).ConfigureAwait(false)
                : self.TryGetLeft(out var left)
                    ? leftHandler(left)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? leftHandler(left)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously folds an Either into a single value using the provided handlers for left and right cases.
    /// </summary>
    /// <typeparam name="TL">Type of the left value.</typeparam>
    /// <typeparam name="TR">Type of the right value.</typeparam>
    /// <typeparam name="TF">Type of the result.</typeparam>
    /// <param name="self">The Task of Either to fold.</param>
    /// <param name="leftHandler">A function to handle the left case, returning a Task of TF.</param>
    /// <param name="rightHandler">A function to handle the right case, returning a Task of TF.</param>
    /// <returns>
    ///     The result of applying the appropriate handler to the Either's value.
    ///     If the Either contains a right value, the rightHandler is awaited and its result is returned.
    ///     If the Either contains a left value, the leftHandler is awaited and its result is returned.
    ///     If the Either does not contain a left or right value, an InvalidOperationException is thrown.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the Either does not contain a left or right value.</exception>
    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, Task<TF>> leftHandler,
        Continuation<TR, Task<TF>> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? await rightHandler(right).ConfigureAwait(false)
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? await leftHandler(left).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? await leftHandler(left).ConfigureAwait(false)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously folds an Either into a single value using the provided handlers for left and right cases.
    /// </summary>
    /// <typeparam name="TL">Type of the left value.</typeparam>
    /// <typeparam name="TR">Type of the right value.</typeparam>
    /// <typeparam name="TF">Type of the result.</typeparam>
    /// <param name="self">The Task of Either to fold.</param>
    /// <param name="leftHandler">A function to handle the left case, returning a Task of TF.</param>
    /// <param name="rightHandler">A function to handle the right case, returning TF.</param>
    /// <returns>
    ///     The result of applying the appropriate handler to the Either's value.
    ///     If the Either contains a right value, the rightHandler is invoked and its result is returned.
    ///     If the Either contains a left value, the leftHandler is awaited and its result is returned.
    ///     If the Either does not contain a left or right value, an InvalidOperationException is thrown.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the Either does not contain a left or right value.</exception>
    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, Task<TF>> leftHandler,
        Continuation<TR, TF> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? rightHandler(right)
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? await leftHandler(left).ConfigureAwait(false)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.Result.TryGetLeft(out var left)
                ? await leftHandler(left).ConfigureAwait(false)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously folds an Either into a single value using the provided handlers for left and right cases.
    /// </summary>
    /// <typeparam name="TL">Type of the left value.</typeparam>
    /// <typeparam name="TR">Type of the right value.</typeparam>
    /// <typeparam name="TF">Type of the result.</typeparam>
    /// <param name="self">The Task of Either to fold.</param>
    /// <param name="leftHandler">A function to handle the left case, returning TF.</param>
    /// <param name="rightHandler">A function to handle the right case, returning a Task of TF.</param>
    /// <returns>
    ///     The result of applying the appropriate handler to the Either's value.
    ///     If the Either contains a right value, the rightHandler is invoked and its result is returned.
    ///     If the Either contains a left value, the leftHandler is invoked and its result is returned.
    ///     If the Either does not contain a left or right value, an InvalidOperationException is thrown.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the Either does not contain a left or right value.</exception>
    public static async Task<TF> FoldAsync<TL, TR, TF>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, TF> leftHandler,
        Continuation<TR, Task<TF>> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? await rightHandler(right).ConfigureAwait(false)
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? leftHandler(left)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.Result.TryGetLeft(out var left)
                ? leftHandler(left)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously folds an Either into a single value using the provided handlers for left and right cases.
    /// </summary>
    /// <typeparam name="TL">Type of the left value.</typeparam>
    /// <typeparam name="TR">Type of the right value.</typeparam>
    /// <typeparam name="TF">Type of the result.</typeparam>
    /// <param name="self">The Task of Either to fold.</param>
    /// <param name="leftHandler">A function to handle the left case, returning TF.</param>
    /// <param name="rightHandler">A function to handle the right case, returning TF.</param>
    /// <returns>
    ///     The result of applying the appropriate handler to the Either's value.
    ///     If the Either contains a right value, the rightHandler is invoked and its result is returned.
    ///     If the Either contains a left value, the leftHandler is invoked and its result is returned.
    ///     If the Either does not contain a left or right value, an InvalidOperationException is thrown.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the Either does not contain a left or right value.</exception>
    public static async Task<TF> Fold<TL, TR, TF>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, TF> leftHandler,
        Continuation<TR, TF> rightHandler
    )
        where TL : notnull
        where TR : notnull
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? rightHandler(right)
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? leftHandler(left)
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.Result.TryGetLeft(out var left)
                ? leftHandler(left)
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Folds an Either with the same type for both left and right values into a single value.
    /// </summary>
    /// <typeparam name="TF">Type of the left and right values.</typeparam>
    /// <param name="self">The Either to fold.</param>
    /// <returns>
    ///     The right value if the Either contains a right value.
    ///     The left value if the Either contains a left value.
    ///     Throws an InvalidOperationException if the Either does not contain a left or right value.
    /// </returns>
    public static TF Fold<TF>(
        this Either<TF, TF> self
    )
        where TF : notnull
    {
        return self.TryGetRight(out var right)
            ? right
            : self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
    }

    /// <summary>
    ///     Asynchronously folds an Either with the same type for both left and right values into a single value.
    /// </summary>
    /// <typeparam name="TF">Type of the left and right values.</typeparam>
    /// <param name="self">The Task of Either to fold.</param>
    /// <returns>
    ///     The result of applying the appropriate handler to the Either's value.
    ///     If the Either contains a right value, the right value is returned.
    ///     If the Either contains a left value, the left value is returned.
    ///     If the Either does not contain a left or right value, an InvalidOperationException is thrown.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the Either does not contain a left or right value.</exception>
    public static async Task<TF> FoldAsync<TF>(
        this Task<Either<TF, TF>> self
    )
        where TF : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).Fold();
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }
}