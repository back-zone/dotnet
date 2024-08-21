using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace back.zone.core.Monads.OptionMonad;

/// <summary>
///     Represents a container that may or may not hold a value.
/// </summary>
/// <typeparam name="TA">The type of the value.</typeparam>
public readonly struct Option<TA>
{
    private readonly TA? _value;

    /// <summary>
    ///     Gets a value indicating whether this instance holds a value.
    /// </summary>
    public bool IsSome => _value is not null;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Option{TA}" /> struct.
    /// </summary>
    /// <param name="value">The value to be wrapped in the Option.</param>
    public Option(TA value)
    {
        _value = value;
    }

    /// <summary>
    ///     Attempts to retrieve the value from the Option.
    /// </summary>
    /// <param name="value">When this method returns true, contains the value from the Option; otherwise, contains null.</param>
    /// <returns>True if the Option holds a value; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue([NotNullWhen(true)] out TA? value)
    {
        value = _value;
        return IsSome;
    }

    /// <summary>
    ///     Implicitly converts a value of type <typeparamref name="TA" /> to an Option.
    /// </summary>
    /// <param name="value">The value to be wrapped in the Option.</param>
    /// <returns>An Option containing the provided value.</returns>
    public static implicit operator Option<TA>(TA? value)
    {
        return value is not null
            ? new Option<TA>(value)
            : Option.None<TA>();
    }
}