using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class Either
{
    /// <summary>
    ///     Creates a new instance of <see cref="Either{TL, TR}" /> representing a successful result.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either.</typeparam>
    /// <param name="right">The value to be wrapped in the Either.</param>
    /// <returns>A new instance of <see cref="Either{TL, TR}" /> with the given right value.</returns>
    public static Either<TL, TR> Right<TL, TR>(TR right)
        where TL : notnull
        where TR : notnull
    {
        return new Either<TL, TR>(right);
    }

    /// <summary>
    ///     Creates a new instance of <see cref="Either{TL, TR}" /> representing a failure result.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either. This type should be an exception or a similar error type.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either. This type should represent a successful result.</typeparam>
    /// <param name="left">The value to be wrapped in the Either. This value represents the failure result.</param>
    /// <returns>A new instance of <see cref="Either{TL, TR}" /> with the given left value.</returns>
    public static Either<TL, TR> Left<TL, TR>(TL left)
        where TL : notnull
        where TR : notnull
    {
        return new Either<TL, TR>(left);
    }

    /// <summary>
    ///     Executes the given effect and returns an Either instance representing the result.
    ///     If the effect throws an exception, it is captured and returned as the left side of the Either.
    ///     If the effect executes successfully, the right side of the Either contains the result.
    /// </summary>
    /// <typeparam name="TR">The type of the result of the effect.</typeparam>
    /// <param name="effect">The effect to be executed.</param>
    /// <returns>
    ///     An Either instance with the following characteristics:
    ///     - If the effect throws an exception, the left side contains the exception.
    ///     - If the effect executes successfully, the right side contains the result.
    /// </returns>
    public static Either<Exception, TR> Effect<TR>(
        Effect<TR> effect
    )
        where TR : notnull
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
    ///     Executes an asynchronous task and returns an Either instance representing the result.
    ///     If the task throws an exception, it is captured and returned as the left side of the Either.
    ///     If the task executes successfully, the right side of the Either contains the result.
    /// </summary>
    /// <typeparam name="TR">The type of the result of the asynchronous task.</typeparam>
    /// <param name="asyncTask">The asynchronous task to be executed.</param>
    /// <returns>
    ///     An Either instance with the following characteristics:
    ///     - If the task throws an exception, the left side contains the exception.
    ///     - If the task executes successfully, the right side contains the result.
    /// </returns>
    public static async Task<Either<Exception, TR>> Async<TR>(
        ValueTask<TR> asyncTask
    )
        where TR : notnull
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
    ///     Executes an asynchronous effect and returns an Either instance representing the result.
    ///     If the effect throws an exception, it is captured and returned as the left side of the Either.
    ///     If the effect executes successfully, the right side of the Either contains the result.
    /// </summary>
    /// <typeparam name="TR">The type of the result of the asynchronous effect.</typeparam>
    /// <param name="asyncEffect">The asynchronous effect to be executed.</param>
    /// <returns>
    ///     An Either instance with the following characteristics:
    ///     - If the effect throws an exception, the left side contains the exception.
    ///     - If the effect executes successfully, the right side contains the result.
    /// </returns>
    public static async Task<Either<Exception, TR>> Async<TR>(
        Effect<Task<TR>> asyncEffect
    )
        where TR : notnull
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
    ///     Executes an asynchronous operation that returns an <see cref="Either{TL, TR}" /> instance.
    ///     If the operation throws an exception, it is captured and returned as the left side of the Either.
    ///     If the operation executes successfully, the right side of the Either contains the result.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either. This type should be an exception or a similar error type.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either. This type should represent a successful result.</typeparam>
    /// <param name="asyncEither">The asynchronous operation to be executed.</param>
    /// <returns>
    ///     An Either instance with the following characteristics:
    ///     - If the operation throws an exception, the left side contains the exception.
    ///     - If the operation executes successfully, the right side contains the result.
    ///     If both left and right sides are empty, an exception with the message "#empty_lef_and_right#" is returned.
    /// </returns>
    public static async Task<Either<TL, TR>> Async<TL, TR>(
        Task<Either<TL, TR>> asyncEither
    )
        where TL : Exception
        where TR : notnull
    {
        try
        {
            var current = await asyncEither.ConfigureAwait(false);

            return current.TryGetRight(out var right)
                ? right
                : current.TryGetLeft(out var left)
                    ? left
                    : (TL)new Exception("#empty_lef_and_right#");
        }
        catch (Exception e)
        {
            return (TL)e;
        }
    }
}