namespace monads.optionmonad.subtypes;

/// <summary>
///     Represents a subtype of Option that represents a lack of a value.
/// </summary>
/// <typeparam name="A">The type of the value that this Option may contain.</typeparam>
public sealed class None<A> : Option<A> where A : notnull
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="None{A}" /> class.
    ///     This constructor is internal and should not be called directly.
    /// </summary>
    internal None()
    {
    }

    /// <summary>
    ///     Determines whether this Option contains a value.
    /// </summary>
    /// <returns>False, as this Option represents a lack of a value.</returns>
    protected override bool IsSome()
    {
        return false;
    }

    /// <summary>
    ///     Attempts to retrieve the value from this Option.
    /// </summary>
    /// <returns>
    ///     Throws an <see cref="InvalidOperationException" />, as this Option represents a lack of a value.
    /// </returns>
    protected override A GetA()
    {
        throw new InvalidOperationException("This Option is none.");
    }

    /// <summary>
    ///     Deconstructs this Option into its constituent parts.
    ///     This method does not have any meaningful return values, as this Option represents a lack of a value.
    /// </summary>
    public void Deconstruct()
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="None{A}" /> class.
    /// </summary>
    /// <returns>A new instance of the <see cref="None{A}" /> class.</returns>
    internal static None<A> Of()
    {
        return new None<A>();
    }
}