using back.zone.core.Monads.EitherMonad;
using back.zone.core.Monads.TryMonad;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionConversions
{
    public static Try<TA> ToTry<TA>(
        this Option<TA> option
    )
        where TA : notnull
    {
        return option.Fold(
            () => Try.Fail<TA>(new NullReferenceException()),
            Try.Succeed
        );
    }

    public static async Task<Try<TA>> ToTryAsync<TA>(
        this Task<Option<TA>> taskOption
    )
        where TA : notnull
    {
        try
        {
            return (await taskOption.ConfigureAwait(false)).ToTry();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Either<NullReferenceException, TA> ToEither<TA>(
        this Option<TA> option
    )
        where TA : notnull
    {
        return option.Fold(
            () => Either.Left<NullReferenceException, TA>(new NullReferenceException()),
            Either.Right<NullReferenceException, TA>
        );
    }

    public static async Task<Either<NullReferenceException, TA>> ToEitherAsync<TA>(
        this Task<Option<TA>> taskOption
    )
        where TA : notnull
    {
        try
        {
            return (await taskOption.ConfigureAwait(false)).ToEither();
        }
        catch (Exception)
        {
            return Either.Left<NullReferenceException, TA>(new NullReferenceException());
        }
    }
}