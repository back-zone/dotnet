using monads.eithermonad.subtypes;

namespace monads.eithermonad;

public static class either
{
    public static Either<L, R> left<L, R>(L left)
    {
        return new Left<L, R>(left);
    }

    public static Either<L, R> right<L, R>(R right)
    {
        return new Right<L, R>(right);
    }
}

internal readonly struct EitherBase<L, R>
{
    public bool IsLeft { get; }
    public bool IsRight { get; }
}