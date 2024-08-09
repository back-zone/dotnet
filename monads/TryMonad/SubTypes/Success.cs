namespace back.zone.monads.TryMonad.SubTypes;

public sealed class Success<TA> : Try<TA> where TA : notnull
{
    private readonly TA _value;

    public Success(TA value)
    {
        _value = value;
    }

    public override bool IsSuccess()
    {
        return true;
    }

    public override Exception Exception()
    {
        throw new InvalidOperationException("#exception_called_on_success#");
    }

    public override TA Value()
    {
        return _value;
    }

    public static implicit operator Success<TA>(TA value)
    {
        return new Success<TA>(value);
    }

    public void Deconstruct(out TA value)
    {
        value = _value;
    }
}