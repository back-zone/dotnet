using monads.eithermonad;
using monads.optionmonad;

namespace monads.iomonad;

public static class conversions
{
    /// <summary>
    ///     Converts an IO monad to an Option monad.
    /// </summary>
    /// <typeparam name="A">The type of the value contained in the IO monad.</typeparam>
    /// <param name="io">The IO monad to convert.</param>
    /// <returns>An Option monad containing the value from the IO monad if successful, or None if an error occurred.</returns>
    public static Option<A> toOption<A>(this IO<A> io)
    {
        return io.fold(_ => option.none<A>(), option.some);
    }

    /// <summary>
    ///     Converts an asynchronous IO monad to an Option monad.
    /// </summary>
    /// <typeparam name="A">The type of the value contained in the IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous IO monad to convert.</param>
    /// <returns>
    ///     An asynchronous Option monad containing the value from the IO monad if successful, or None if an error occurred.
    ///     The result is awaitable, allowing for asynchronous execution.
    /// </returns>
    public static async Task<Option<A>> toOptionAsync<A>(this Task<IO<A>> ioTask)
    {
        var currentTask = await ioTask;

        return currentTask.toOption();
    }

    /// <summary>
    ///     Converts an IO monad to an Either monad.
    /// </summary>
    /// <typeparam name="R">The type of the value contained in the IO monad.</typeparam>
    /// <param name="io">The IO monad to convert.</param>
    /// <returns>
    ///     An Either monad containing the value from the IO monad if successful, or an Exception wrapped in the Left side if
    ///     an error occurred.
    ///     The Right side of the Either monad contains the value, while the Left side contains the exception.
    /// </returns>
    public static Either<Exception, R> toEither<R>(this IO<R> io)
    {
        return io.fold(
            either.left<Exception, R>,
            either.right<Exception, R>
        );
    }

    /// <summary>
    ///     Converts an asynchronous IO monad to an Either monad.
    /// </summary>
    /// <typeparam name="R">The type of the value contained in the IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous IO monad to convert.</param>
    /// <returns>
    ///     An asynchronous Either monad containing the value from the IO monad if successful, or an Exception wrapped in the
    ///     Left side if
    ///     an error occurred. The Right side of the Either monad contains the value, while the Left side contains the
    ///     exception.
    ///     The result is awaitable, allowing for asynchronous execution.
    /// </returns>
    public static async Task<Either<Exception, R>> toEitherAsync<R>(this Task<IO<R>> ioTask)
    {
        var currentTask = await ioTask;

        return currentTask.toEither();
    }
}