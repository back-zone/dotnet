namespace monads.eithermonad.subtypes;

public sealed class Right<L, R> : Either<L, R>
{
    private readonly R _right;

    internal Right(R right)
    {
        _right = right;
    }

    public override bool IsLeft()
    {
        return false;
    }

    public override L GetLeft()
    {
        throw new InvalidOperationException("This Either is a right.");
    }

    public override R GetRight()
    {
        return _right;
    }

    public void Deconstruct(out R right)
    {
        right = _right;
    }

    public static implicit operator Right<L, R>(R right)
    {
        return new Right<L, R>(right);
    }

    internal static Right<L, R> Of(R right)
    {
        return new Right<L, R>(right);
    }
}