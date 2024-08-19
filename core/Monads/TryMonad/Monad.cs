using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace back.zone.core.Monads.TryMonad;

public readonly struct Try<TA> where TA : notnull
{
    private readonly TA? _value;
    private readonly Exception? _exception;
    public bool IsSuccess => _exception is null;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Try{TA}" /> struct with a successful result.
    /// </summary>
    /// <param name="value">The value to be stored in the Try monad. This represents a successful operation.</param>
    /// <remarks>
    ///     This constructor creates a Try instance that represents a successful operation.
    ///     The provided value is stored, and no exception is associated with this instance.
    /// </remarks>
    public Try(TA value)
    {
        _value = value;
        _exception = null;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Try{TA}" /> struct with a failure result.
    /// </summary>
    /// <param name="exception">The exception that caused the failure. This represents an unsuccessful operation.</param>
    /// <remarks>
    ///     This constructor creates a Try instance that represents a failed operation.
    ///     The provided exception is stored, and no value is associated with this instance.
    ///     The <see cref="IsSuccess" /> property will return false for instances created with this constructor.
    /// </remarks>
    public Try(Exception exception)
    {
        _value = default;
        _exception = exception;
    }


    /// <summary>
    ///     Attempts to retrieve the value stored in the Try monad.
    /// </summary>
    /// <param name="value">
    ///     When this method returns true, contains the value stored in the Try monad.
    ///     When this method returns false, contains the default value for type TA.
    /// </param>
    /// <returns>
    ///     true if the Try monad represents a successful operation and contains a value; otherwise, false.
    /// </returns>
    /// <remarks>
    ///     This method is useful when you need to check if the Try monad contains a value before accessing it.
    ///     If the Try monad represents a successful operation, the method sets the 'value' parameter to the stored value and
    ///     returns true.
    ///     If the Try monad represents a failed operation, the method sets the 'value' parameter to the default value for type
    ///     TA and returns false.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue([NotNullWhen(true)] out TA? value)
    {
        value = _value;
        return IsSuccess;
    }


    /// <summary>
    ///     Attempts to retrieve the exception stored in the Try monad.
    /// </summary>
    /// <param name="exception">
    ///     When this method returns true, contains the exception stored in the Try monad.
    ///     When this method returns false, contains a null value.
    /// </param>
    /// <returns>
    ///     true if the Try monad represents a failed operation and contains an exception; otherwise, false.
    /// </returns>
    /// <remarks>
    ///     This method is useful when you need to check if the Try monad contains an exception before accessing it.
    ///     If the Try monad represents a failed operation, the method sets the 'exception' parameter to the stored exception
    ///     and
    ///     returns true.
    ///     If the Try monad represents a successful operation, the method sets the 'exception' parameter to null and returns
    ///     false.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetException([NotNullWhen(true)] out Exception? exception)
    {
        exception = _exception;
        return !IsSuccess;
    }

    public static implicit operator Try<TA>(TA value)
    {
        return new Try<TA>(value);
    }

    public static implicit operator Try<TA>(Exception exception)
    {
        return new Try<TA>(exception);
    }
}