namespace back.zone.monads.TryMonad.SubTypes;

public sealed class Failure<TA> : Try<TA> where TA : notnull
{
    private readonly Exception _exception;

    public Failure(Exception exception)
    {
        _exception = exception;
    }

    public override bool IsSuccess()
    {
        return false;
    }

    public override Exception Exception()
    {
        return _exception;
    }

    public override TA Value()
    {
        throw new InvalidOperationException("#value_called_on_failure#");
    }

    public static implicit operator Failure<TA>(Exception exception)
    {
        return new Failure<TA>(exception);
    }

    public void Deconstruct(out Exception exception)
    {
        exception = _exception;
    }
}