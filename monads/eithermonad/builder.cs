using back.zone.monads.eithermonad.subtypes;

namespace back.zone.monads.eithermonad;

/// <summary>
///     Provides methods to create instances of the Either monad.
/// </summary>
public static class either
{
    /// <summary>
    ///     Creates a new instance of the Either monad based on the result of the provided function.
    ///     If the function throws an exception, it will be caught and returned as a left value.
    ///     If the function returns a non-null value, it will be wrapped in a right value.
    /// </summary>
    /// <typeparam name="R">The type of the right value.</typeparam>
    /// <param name="builder">The function to execute and get the result.</param>
    /// <returns>
    ///     An instance of Either monad with a left value if the function throws an exception,
    ///     or a right value if the function returns a non-null value.
    /// </returns>
    public static Either<Exception, R> of<R>(Func<R> builder)
    {
        try
        {
            var product = builder();

            return product is null
                ? left<Exception, R>(new NullReferenceException("The builder function returned null."))
                : right<Exception, R>(product);
        }
        catch (Exception ex)
        {
            return left<Exception, R>(ex);
        }
    }

    /// <summary>
    ///     Creates a new instance of the Either monad with the result of the provided function and exception recovery.
    ///     If the function throws an exception, it will be caught and passed to the recover function to create a left value.
    ///     If the function returns a non-null value, it will be wrapped in a right value.
    /// </summary>
    /// <typeparam name="L">The type of the left value. Must be a non-null reference type.</typeparam>
    /// <typeparam name="R">The type of the right value. Must be a non-null reference type.</typeparam>
    /// <param name="builder">The function to execute and get the result.</param>
    /// <param name="recover">The function to create a left value from an exception.</param>
    /// <returns>
    ///     An instance of Either monad with a left value if the function throws an exception,
    ///     or a right value if the function returns a non-null value.
    /// </returns>
    public static Either<L, R> of<L, R>(
        Func<R> builder,
        Func<Exception, L> recover
    )
        where L : notnull
    {
        try
        {
            var product = builder();

            return product is null
                ? left<L, R>(recover(new NullReferenceException("The builder function returned null.")))
                : right<L, R>(product);
        }
        catch (Exception e)
        {
            return left<L, R>(recover(e));
        }
    }

    /// <summary>
    ///     Creates a new instance of the Either monad with the result of an asynchronous operation.
    ///     If the asynchronous operation throws an exception, it will be caught and returned as a left value.
    ///     If the asynchronous operation completes successfully and returns a non-null value, it will be wrapped in a right
    ///     value.
    /// </summary>
    /// <typeparam name="R">The type of the right value.</typeparam>
    /// <param name="asyncBuilder">The asynchronous operation to execute and get the result.</param>
    /// <returns>
    ///     An instance of Either monad with a left value if the asynchronous operation throws an exception,
    ///     or a right value if the asynchronous operation completes successfully and returns a non-null value.
    /// </returns>
    public static async Task<Either<Exception, R>> ofAsync<R>(Task<R> asyncBuilder)
    {
        try
        {
            var product = await asyncBuilder;

            return product is null
                ? left<Exception, R>(
                    new NullReferenceException("The asynchronous builder function returned null.")
                )
                : right<Exception, R>(product);
        }
        catch (Exception ex)
        {
            return left<Exception, R>(ex);
        }
    }

    /// <summary>
    ///     Creates a new instance of the Either monad with the result of an asynchronous operation.
    ///     If the asynchronous operation throws an exception, it will be caught and returned as a left value.
    ///     If the asynchronous operation completes successfully and returns a non-null value, it will be wrapped in a right
    ///     value.
    /// </summary>
    /// <typeparam name="L">The type of the left value. Must be an exception type.</typeparam>
    /// <typeparam name="R">The type of the right value. Must be a non-null reference type.</typeparam>
    /// <param name="asyncEither">The asynchronous operation to execute and get the result.</param>
    /// <returns>
    ///     An instance of Either monad with a left value if the asynchronous operation throws an exception,
    ///     or a right value if the asynchronous operation completes successfully and returns a non-null value.
    /// </returns>
    public static async Task<Either<L, R>> ofAsync<L, R>(
        Task<Either<L, R>> asyncEither
    )
        where L : Exception
        where R : notnull
    {
        try
        {
            var product = await asyncEither;

            return product;
        }
        catch (Exception e)
        {
            return left<L, R>((L)e);
        }
    }

    /// <summary>
    ///     Creates a new instance of the Either monad with a left value.
    /// </summary>
    /// <typeparam name="L">The type of the left value.</typeparam>
    /// <typeparam name="R">The type of the right value.</typeparam>
    /// <param name="left">The left value.</param>
    /// <returns>An instance of Either with the left value.</returns>
    public static Either<L, R> left<L, R>(
        L left
    )
        where L : notnull
        where R : notnull
    {
        return Left<L, R>.Of(left);
    }

    /// <summary>
    ///     Creates a new instance of the Either monad with a right value.
    /// </summary>
    /// <typeparam name="L">The type of the left value.</typeparam>
    /// <typeparam name="R">The type of the right value.</typeparam>
    /// <param name="right">The right value.</param>
    /// <returns>An instance of Either with the right value.</returns>
    public static Either<L, R> right<L, R>(
        R right
    )
        where L : notnull
        where R : notnull
    {
        return Right<L, R>.Of(right);
    }
}