namespace monads.optionmonad;

/// <summary>
///     Represents an optional value. It can either hold a value of type <typeparamref name="A" /> or be empty.
/// </summary>
/// <typeparam name="A">The type of the optional value.</typeparam>
public abstract class Option<A>
{
    /// <summary>
    ///     Determines whether this instance holds a value.
    /// </summary>
    /// <returns><c>true</c> if this instance is some; otherwise, <c>false</c>.</returns>
    protected abstract bool IsSome();

    /// <summary>
    ///     Gets the value of this instance.
    /// </summary>
    /// <returns>The value of type <typeparamref name="A" /> if this instance is some; otherwise, throws an exception.</returns>
    /// <exception cref="System.InvalidOperationException">Thrown when the instance is empty (i.e., IsSome() returns false).</exception>
    protected abstract A GetA();
}