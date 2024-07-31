using monads.iomonad;

namespace monads.optionmonad;

public static class conversions
{
    /// <summary>
    ///     Converts an Option to an IO monad.
    /// </summary>
    /// <typeparam name="A">The type of the Option's value.</typeparam>
    /// <param name="option">The Option to convert.</param>
    /// <returns>
    ///     An IO monad representing the Option's value.
    ///     If the Option is Some, the IO will contain the value.
    ///     If the Option is None, the IO will fail with the message "Option is None".
    /// </returns>
    public static IO<A> toIO<A>(this Option<A> option)
    {
        return option.fold(
            io.fail<A>("Option is None"),
            io.succeed
        );
    }

    /// <summary>
    ///     Converts a Task of Option to an IO monad asynchronously.
    /// </summary>
    /// <typeparam name="A">The type of the Option's value.</typeparam>
    /// <param name="optionTask">The Task of Option to convert.</param>
    /// <returns>
    ///     An IO monad representing the Option's value.
    ///     If the Option is Some, the IO will contain the value.
    ///     If the Option is None, the IO will fail with the message "Option is None".
    ///     The conversion is performed asynchronously.
    /// </returns>
    public static async Task<IO<A>> toIOAsync<A>(this Task<Option<A>> optionTask)
    {
        var currentOption = await optionTask;

        return currentOption.toIO();
    }
}