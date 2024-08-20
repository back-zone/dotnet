using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace back.zone.core.Monads.EitherMonad;

public readonly struct Either<TL, TR>
    where TL : notnull
    where TR : notnull
{
    private readonly TR? _right;
    private readonly TL? _left;
    public bool IsRight => _right is not null;

    /// <summary>
    ///     Constructs a new instance of the <see cref="Either{TL, TR}" /> struct representing a left value.
    /// </summary>
    /// <param name="left">The left value to be represented.</param>
    /// <remarks>
    ///     This constructor is used when the operation results in a failure or an error.
    ///     The <see cref="IsRight" /> property will return false, indicating that this instance represents a left value.
    /// </remarks>
    public Either(TL left)
    {
        _right = default;
        _left = left;
    }

    /// <summary>
    ///     Constructs a new instance of the <see cref="Either{TL, TR}" /> struct representing a right value.
    /// </summary>
    /// <param name="right">The right value to be represented.</param>
    /// <remarks>
    ///     This constructor is used when the operation results in a successful outcome.
    ///     The <see cref="IsRight" /> property will return true, indicating that this instance represents a right value.
    /// </remarks>
    public Either(TR right)
    {
        _right = right;
        _left = default;
    }


    /// <summary>
    ///     Attempts to retrieve the right value from the <see cref="Either{TL, TR}" /> instance.
    /// </summary>
    /// <param name="right">
    ///     When this method returns true, contains the right value of the <see cref="Either{TL, TR}" /> instance.
    ///     When this method returns false, contains the default value for the type <typeparamref name="TR" />.
    /// </param>
    /// <returns>
    ///     true if the <see cref="Either{TL, TR}" /> instance represents a right value; otherwise, false.
    /// </returns>
    /// <remarks>
    ///     This method is useful when you need to check if the <see cref="Either{TL, TR}" /> instance represents a right value
    ///     before accessing its value.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetRight([NotNullWhen(true)] out TR? right)
    {
        right = _right;
        return IsRight;
    }

    /// <summary>
    ///     Attempts to retrieve the left value from the <see cref="Either{TL, TR}" /> instance.
    /// </summary>
    /// <param name="left">
    ///     When this method returns true, contains the left value of the <see cref="Either{TL, TR}" /> instance.
    ///     When this method returns false, contains the default value for the type <typeparamref name="TL" />.
    /// </param>
    /// <returns>
    ///     true if the <see cref="Either{TL, TR}" /> instance represents a left value; otherwise, false.
    /// </returns>
    /// <remarks>
    ///     This method is useful when you need to check if the <see cref="Either{TL, TR}" /> instance represents a left value
    ///     before accessing its value.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetLeft([NotNullWhen(true)] out TL? left)
    {
        left = _left;
        return !IsRight;
    }

    /// <summary>
    ///     Implicitly converts a right value to an instance of the <see cref="Either{TL, TR}" /> struct.
    /// </summary>
    /// <param name="right">The right value to be represented in the <see cref="Either{TL, TR}" /> struct.</param>
    /// <returns>
    ///     A new instance of the <see cref="Either{TL, TR}" /> struct representing the right value.
    ///     The <see cref="Either{TL, TR}" /> property will return true for this instance.
    /// </returns>
    /// <remarks>
    ///     This implicit conversion operator is useful when you want to create an instance of the
    ///     <see cref="Either{TL, TR}" /> struct
    ///     from a right value without explicitly calling the constructor.
    /// </remarks>
    public static implicit operator Either<TL, TR>(TR right)
    {
        return new Either<TL, TR>(right);
    }

    /// <summary>
    ///     Implicitly converts a left value to an instance of the <see cref="Either{TL, TR}" /> struct.
    /// </summary>
    /// <param name="left">The left value to be represented in the <see cref="Either{TL, TR}" /> struct.</param>
    /// <returns>
    ///     A new instance of the <see cref="Either{TL, TR}" /> struct representing the left value.
    ///     The <see cref="Either{TL, TR}" /> property will return false for this instance.
    /// </returns>
    /// <remarks>
    ///     This implicit conversion operator is useful when you want to create an instance of the
    ///     <see cref="Either{TL, TR}" /> struct
    ///     from a left value without explicitly calling the constructor.
    /// </remarks>
    public static implicit operator Either<TL, TR>(TL left)
    {
        return new Either<TL, TR>(left);
    }
}