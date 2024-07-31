using monads.eithermonad.subtypes;

namespace monads.eithermonad;

public abstract class Either<L, R>
{
    public abstract bool IsLeft();

    public bool IsRight()
    {
        return !IsLeft();
    }

    public abstract L GetLeft();

    public abstract R GetRight();

    public static implicit operator Either<L, R>(L left)
    {
        return new Left<L, R>(left);
    }
}