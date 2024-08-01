namespace back.zone.monads.iomonad.subtypes;

/// <summary>
///     Represents a successful computation in the IO monad.
/// </summary>
/// <typeparam name="A">The type of the result.</typeparam>
public sealed class Success<A> : IO<A> where A : notnull
{
    private readonly A _a;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Success{A}" /> class.
    /// </summary>
    /// <param name="a">The result of the successful computation.</param>
    internal Success(A a)
    {
        _a = a;
    }

    /// <inheritdoc />
    protected override bool IsSuccess()
    {
        return true;
    }

    /// <inheritdoc />
    protected override Exception GetException()
    {
        throw new InvalidOperationException("This Result is a success.");
    }

    /// <inheritdoc />
    protected override A GetA()
    {
        return _a;
    }

    /// <summary>
    ///     Deconstructs the <see cref="Success{A}" /> instance into its components.
    /// </summary>
    /// <param name="a">The result of the successful computation.</param>
    public void Deconstruct(out A a)
    {
        a = _a;
    }

    /// <summary>
    ///     Implicitly converts a value of type <typeparamref name="A" /> to a <see cref="Success{A}" /> instance.
    /// </summary>
    /// <param name="a">The value to convert.</param>
    /// <returns>A <see cref="Success{A}" /> instance containing the given value.</returns>
    public static implicit operator Success<A>(A a)
    {
        return new Success<A>(a);
    }

    /// <summary>
    ///     Creates a new <see cref="Success{A}" /> instance containing the given value.
    /// </summary>
    /// <param name="a">The value to wrap in a <see cref="Success{A}" /> instance.</param>
    /// <returns>A <see cref="Success{A}" /> instance containing the given value.</returns>
    internal static Success<A> Of(A a)
    {
        return new Success<A>(a);
    }
}