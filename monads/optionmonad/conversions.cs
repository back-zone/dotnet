using back.zone.monads.eithermonad;
using back.zone.monads.iomonad;
using monads.iomonad;

namespace back.zone.monads.optionmonad;

public static class conversions
{
    /// <summary>
    ///     Converts an Option to an IO.
    ///     If the Option is None, the resulting IO will fail with a specified error message.
    /// </summary>
    /// <typeparam name="A">The type of the Option's value.</typeparam>
    /// <param name="option">The Option to convert.</param>
    /// <returns>
    ///     An IO. If the Option is Some, the resulting IO will succeed with the Option's value.
    ///     If the Option is None, the resulting IO will fail with the error message "Option is None".
    /// </returns>
    public static IO<A> toIO<A>(
        this Option<A> option
    )
        where A : notnull
    {
        return option.fold(
            io.fail<A>(new NullReferenceException("Option is None")),
            io.succeed
        );
    }

    /// <summary>
    ///     Converts a Task that returns an Option to a Task that returns an IO.
    ///     If the Option is None, the resulting IO will fail with a specified error message.
    /// </summary>
    /// <typeparam name="A">The type of the Option's value.</typeparam>
    /// <param name="optionTask">The Task that returns an Option.</param>
    /// <returns>
    ///     A Task that returns an IO. If the Option is Some, the resulting IO will succeed with the Option's value. If
    ///     the Option is None, the resulting IO will fail.
    /// </returns>
    public static async Task<IO<A>> toIOAsync<A>(
        this Task<Option<A>> optionTask
    )
        where A : notnull
    {
        var currentOption = await optionTask;

        return currentOption.toIO();
    }

    /// <summary>
    ///     Converts an Option to an Either.
    ///     If the Option is None, the resulting Either will be Left with a specified error message.
    /// </summary>
    /// <typeparam name="A">The type of the Option's value.</typeparam>
    /// <param name="option">The Option to convert.</param>
    /// <returns>
    ///     An Either. If the Option is Some, the resulting Either will be Right with the Option's value.
    ///     If the Option is None, the resulting Either will be Left with a NullReferenceException with the error message
    ///     "Option is None".
    /// </returns>
    public static Either<NullReferenceException, A> toEither<A>(
        this Option<A> option
    )
        where A : notnull
    {
        return option.fold(
            either.left<NullReferenceException, A>(new NullReferenceException("Option is None")),
            either.right<NullReferenceException, A>
        );
    }

    /// <summary>
    ///     Converts a Task that returns an Option to a Task that returns an Either.
    ///     If the Option is None, the resulting Either will be Left with a specified error message.
    /// </summary>
    /// <typeparam name="A">The type of the Option's value.</typeparam>
    /// <param name="optionTask">The Task that returns an Option.</param>
    /// <returns>
    ///     A Task that returns an Either. If the Option is Some, the resulting Either will be Right with the Option's value.
    ///     If the Option is None, the resulting Either will be Left with a NullReferenceException with the error message
    ///     "Option is None".
    /// </returns>
    public static async Task<Either<NullReferenceException, A>> toEitherAsync<A>(
        this Task<Option<A>> optionTask
    )
        where A : notnull
    {
        var currentOption = await optionTask;

        return currentOption.toEither();
    }
}