namespace monads.eithermonad.subtypes;

/// <summary>
///     Represents a right-sided value in an Either monad.
/// </summary>
/// <typeparam name="L">The type of the left-side value.</typeparam>
/// <typeparam name="R">The type of the right-side value.</typeparam>
public sealed class Right<L, R> : Either<L, R>
    where L : notnull
    where R : notnull
{
    private readonly R _right;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Right{L, R}" /> class.
    /// </summary>
    /// <param name="right">The right-side value.</param>
    internal Right(R right)
    {
        _right = right;
    }

    /// <summary>
    ///     Determines whether this instance represents a left-side value.
    /// </summary>
    /// <returns><c>false</c> since this instance represents a right-side value.</returns>
    public override bool IsLeft()
    {
        return false;
    }

    /// <summary>
    ///     Gets the left-side value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when this instance represents a right-side value.</exception>
    public override L GetLeft()
    {
        throw new InvalidOperationException("This Either is a right.");
    }

    /// <summary>
    ///     Gets the right-side value.
    /// </summary>
    /// <returns>The right-side value.</returns>
    public override R GetRight()
    {
        return _right;
    }

    /// <summary>
    ///     Deconstructs the right-side value.
    /// </summary>
    /// <param name="right">The right-side value.</param>
    public void Deconstruct(out R right)
    {
        right = _right;
    }

    /// <summary>
    ///     Implicitly converts a right-side value to a <see cref="Right{L, R}" /> instance.
    /// </summary>
    /// <param name="right">The right-side value.</param>
    public static implicit operator Right<L, R>(R right)
    {
        return Of(right);
    }

    /// <summary>
    ///     Creates a new <see cref="Right{L, R}" /> instance from a right-side value.
    /// </summary>
    /// <param name="right">The right-side value.</param>
    /// <returns>A new <see cref="Right{L, R}" /> instance.</returns>
    internal static Right<L, R> Of(R right)
    {
        return new Right<L, R>(right);
    }
}