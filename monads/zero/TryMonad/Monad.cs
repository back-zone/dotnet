using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace back.zone.monads.zero.TryMonad;

public readonly struct Try<TA> where TA : notnull
{
    private readonly TA? _value;
    private readonly Exception? _exception;
    public bool IsSuccess => _exception is null;

    public Try(TA value)
    {
        _value = value;
        _exception = null;
    }

    public Try(Exception exception)
    {
        _value = default;
        _exception = exception;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue([NotNullWhen(true)] out TA? value)
    {
        value = _value;
        return IsSuccess;
    }

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