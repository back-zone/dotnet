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

    public Either(TL left)
    {
        _right = default;
        _left = left;
    }

    public Either(TR right)
    {
        _right = right;
        _left = default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetRight([NotNullWhen(true)] out TR? right)
    {
        right = _right;
        return IsRight;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetLeft([NotNullWhen(true)] out TL? left)
    {
        left = _left;
        return !IsRight;
    }

    public static implicit operator Either<TL, TR>(TR right)
    {
        return new Either<TL, TR>(right);
    }

    public static implicit operator Either<TL, TR>(TL left)
    {
        return new Either<TL, TR>(left);
    }
}