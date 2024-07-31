using monads.iomonad;
using monads.optionmonad;

namespace monads.eithermonad;

public static class eitherconversions
{
    /// <summary>
    ///     Converts an Either instance to an Option instance.
    /// </summary>
    /// <typeparam name="L">The type of the left side of the Either.</typeparam>
    /// <typeparam name="R">The type of the right side of the Either.</typeparam>
    /// <param name="either">The Either instance to convert.</param>
    /// <returns>An Option instance containing the right side of the Either if it exists, otherwise None.</returns>
    /// <remarks>
    ///     The Either type represents a computation that can result in either a left or right value.
    ///     The Option type represents a computation that can result in a value or absence of a value.
    ///     This function converts an Either instance to an Option instance by discarding the left side if it exists.
    /// </remarks>
    public static Option<R> toOption<L, R>(
        this Either<L, R> either
    )
        where L : notnull
        where R : notnull
    {
        return either.fold(_ => option.none<R>(), option.some);
    }

    /// <summary>
    ///     Converts an asynchronous Either instance to an Option instance.
    /// </summary>
    /// <typeparam name="L">The type of the left side of the Either.</typeparam>
    /// <typeparam name="R">The type of the right side of the Either.</typeparam>
    /// <param name="eitherTask">The asynchronous Either instance to convert.</param>
    /// <returns>An asynchronous Option instance containing the right side of the Either if it exists, otherwise None.</returns>
    /// <remarks>
    ///     The Either type represents a computation that can result in either a left or right value.
    ///     The Option type represents a computation that can result in a value or absence of a value.
    ///     This function converts an asynchronous Either instance to an asynchronous Option instance by discarding the left
    ///     side if it exists.
    /// </remarks>
    public static async Task<Option<R>> toOptionAsync<L, R>(
        this Task<Either<L, R>> eitherTask
    )
        where L : notnull
        where R : notnull
    {
        var currentTask = await eitherTask;
        return currentTask.toOption();
    }

    /// <summary>
    ///     Converts an Either instance to an IO instance.
    /// </summary>
    /// <typeparam name="L">The type of the left side of the Either. Must be an Exception type.</typeparam>
    /// <typeparam name="R">The type of the right side of the Either. Must not be null.</typeparam>
    /// <param name="either">The Either instance to convert.</param>
    /// <returns>
    ///     An IO instance that will either fail with the left side of the Either if it exists, or succeed with the right
    ///     side.
    /// </returns>
    /// <remarks>
    ///     The Either type represents a computation that can result in either a left or right value.
    ///     The IO type represents a computation that can result in a value or an exception.
    ///     This function converts an Either instance to an IO instance by mapping the left side to an IO failure and the right
    ///     side to an IO success.
    /// </remarks>
    public static IO<R> toIO<L, R>(
        this Either<L, R> either
    )
        where L : Exception
        where R : notnull
    {
        return either.fold(io.fail<R>, io.succeed);
    }

    /// <summary>
    ///     Converts an asynchronous Either instance to an IO instance.
    /// </summary>
    /// <typeparam name="L">The type of the left side of the Either. Must be an Exception type.</typeparam>
    /// <typeparam name="R">The type of the right side of the Either. Must not be null.</typeparam>
    /// <param name="eitherTask">The asynchronous Either instance to convert.</param>
    /// <returns>
    ///     An IO instance that will either fail with the left side of the Either if it exists, or succeed with the right side.
    /// </returns>
    /// <remarks>
    ///     The Either type represents a computation that can result in either a left or right value.
    ///     The IO type represents a computation that can result in a value or an exception.
    ///     This function converts an Either instance to an IO instance by mapping the left side to an IO failure and the right
    ///     side to an IO success.
    /// </remarks>
    public static async Task<IO<R>> toIOAsync<L, R>(
        this Task<Either<L, R>> eitherTask
    )
        where L : Exception
        where R : notnull
    {
        var currentTask = await eitherTask;
        return currentTask.toIO();
    }
}