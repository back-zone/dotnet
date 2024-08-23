using back.zone.core.Monads.EitherMonad;
using back.zone.core.Monads.OptionMonad;

namespace back.zone.core.Monads.TryMonad;

public static class TryConversions
{
    /// <summary>
    ///     Converts a Try monad result into an Either monad result.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="tryResult">The Try monad result to convert.</param>
    /// <returns>
    ///     An Either monad result.
    ///     If the Try monad result is successful, the Either result will be Right with the value.
    ///     If the Try monad result is failure, the Either result will be Left with the exception.
    /// </returns>
    public static Either<Exception, TA> ToEither<TA>(
        this Try<TA> tryResult
    )
        where TA : notnull
    {
        return tryResult
            .Fold(
                Either.Left<Exception, TA>,
                Either.Right<Exception, TA>
            );
    }

    /// <summary>
    ///     Converts an asynchronous Try monad result into an Either monad result.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="taskResult">The asynchronous Try monad result to convert.</param>
    /// <returns>
    ///     An asynchronous Either monad result.
    ///     If the Try monad result is successful, the Either result will be Right with the value.
    ///     If the Try monad result is failure, the Either result will be Left with the exception.
    /// </returns>
    public static async Task<Either<Exception, TA>> ToEitherAsync<TA>(
        this Task<Try<TA>> taskResult
    )
        where TA : notnull
    {
        return (await taskResult).ToEither();
    }

    /// <summary>
    ///     Converts a Try monad result into an Option monad result.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="tryResult">The Try monad result to convert.</param>
    /// <returns>
    ///     An Option monad result.
    ///     If the Try monad result is successful, the Option result will be Some with the value.
    ///     If the Try monad result is failure, the Option result will be None.
    /// </returns>
    public static Option<TA> ToOption<TA>(
        this Try<TA> tryResult
    )
        where TA : notnull
    {
        return tryResult
            .Fold(
                _ => Option.None<TA>(),
                Option.Some
            );
    }

    /// <summary>
    ///     Converts an asynchronous Try monad result into an Option monad result.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="taskResult">The asynchronous Try monad result to convert.</param>
    /// <returns>
    ///     An asynchronous Option monad result.
    ///     If the Try monad result is successful, the Option result will be Some with the value.
    ///     If the Try monad result is failure, the Option result will be None.
    /// </returns>
    public static async Task<Option<TA>> ToOptionAsync<TA>(
        this Task<Try<TA>> taskResult
    )
        where TA : notnull
    {
        return (await taskResult).ToOption();
    }
}