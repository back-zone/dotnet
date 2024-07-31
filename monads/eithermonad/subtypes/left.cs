namespace monads.eithermonad.subtypes;

public sealed class Left<L, R> : Either<L, R>
{
    private readonly L _left;

    internal Left(L left)
    {
        _left = left;
    }

    public override bool IsLeft()
    {
        return true;
    }

    public override R GetRight()
    {
        throw new InvalidOperationException("This Either is a left.");
    }

    public override L GetLeft()
    {
        return _left;
    }

    public void Deconstruct(out L left)
    {
        left = _left;
    }

    public static implicit operator Left<L, R>(L left)
    {
        return new Left<L, R>(left);
    }

    internal static Left<L, R> Of(L left)
    {
        return new Left<L, R>(left);
    }
}