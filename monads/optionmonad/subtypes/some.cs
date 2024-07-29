namespace monads.optionmonad.subtypes;

/// <summary>
///     Represents a non-empty option, containing a value of type <typeparamref name="A" />.
/// </summary>
/// <typeparam name="A">The type of the value contained in the option.</typeparam>
public sealed class Some<A> : Option<A>
{
    private readonly A _a;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Some{A}" /> class with the specified value.
    /// </summary>
    /// <param name="a">The value to be contained in the option.</param>
    internal Some(A a)
    {
        _a = a;
    }

    /// <inheritdoc />
    protected override bool IsSome()
    {
        return true;
    }

    /// <inheritdoc />
    protected override A GetA()
    {
        return _a;
    }

    /// <summary>
    ///     Deconstructs the <see cref="Some{A}" /> instance into its constituent parts.
    /// </summary>
    /// <param name="a">The value contained in the option.</param>
    public void Deconstruct(out A a)
    {
        a = _a;
    }

    /// <summary>
    ///     Implicitly converts a value of type <typeparamref name="A" /> to a <see cref="Some{A}" /> instance.
    /// </summary>
    /// <param name="a">The value to be converted.</param>
    /// <returns>A new <see cref="Some{A}" /> instance containing the specified value.</returns>
    public static implicit operator Some<A>(A a)
    {
        return new Some<A>(a);
    }
}