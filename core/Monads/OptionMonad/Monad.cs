using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace back.zone.core.Monads.OptionMonad;

public readonly struct Option<TA>
{
    private readonly TA? _value;
    public bool IsSome => _value is not null;

    public Option(TA value)
    {
        _value = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue([NotNullWhen(true)] out TA? value)
    {
        value = _value;
        return IsSome;
    }

    public static implicit operator Option<TA>(TA value)
    {
        return new Option<TA>(value);
    }
}