namespace monads.eithermonad.subtypes;

/// <summary>
///     Represents a Left value in the Either monad.
///     This class is a subtype of the Either class and is used to hold a value of type L.
/// </summary>
/// <typeparam name="L">The type of the left value.</typeparam>
/// <typeparam name="R">The type of the right value.</typeparam>
public sealed class Left<L, R> : Either<L, R>
    where L : notnull
    where R : notnull
{
    private readonly L _left;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Left{L, R}" /> class with the specified left value.
    /// </summary>
    /// <param name="left">The left value.</param>
    internal Left(L left)
    {
        _left = left;
    }

    /// <summary>
    ///     Determines whether this instance represents a left value.
    /// </summary>
    /// <returns><c>true</c> if this instance is a left value; otherwise, <c>false</c>.</returns>
    public override bool IsLeft()
    {
        return true;
    }

    /// <summary>
    ///     Attempts to retrieve the right value.
    ///     If this instance represents a left value, an InvalidOperationException is thrown.
    /// </summary>
    /// <returns>The right value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when this instance represents a left value.</exception>
    public override R GetRight()
    {
        throw new InvalidOperationException("This Either is a left.");
    }

    /// <summary>
    ///     Retrieves the left value.
    /// </summary>
    /// <returns>The left value.</returns>
    public override L GetLeft()
    {
        return _left;
    }

    /// <summary>
    ///     Deconstructs the Left instance into its left value.
    /// </summary>
    /// <param name="left">The left value.</param>
    public void Deconstruct(out L left)
    {
        left = _left;
    }

    /// <summary>
    ///     Implicitly converts a left value to a Left instance.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <returns>A Left instance containing the left value.</returns>
    public static implicit operator Left<L, R>(L left)
    {
        return Of(left);
    }

    /// <summary>
    ///     Creates a new Left instance with the specified left value.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <returns>A new Left instance containing the left value.</returns>
    internal static Left<L, R> Of(L left)
    {
        return new Left<L, R>(left);
    }
}