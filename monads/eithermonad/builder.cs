using monads.eithermonad.subtypes;

namespace monads.eithermonad;

/// <summary>
///     Provides methods to create instances of the Either monad.
/// </summary>
public static class either
{
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