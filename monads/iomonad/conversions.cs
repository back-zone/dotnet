using monads.optionmonad;
using monads.result;

namespace monads.iomonad;

public static class conversions
{
    /// <summary>
    ///     Converts an IO monad to an Option monad.
    /// </summary>
    /// <typeparam name="A">The type of the value contained in the IO monad.</typeparam>
    /// <param name="io">The IO monad to convert.</param>
    /// <returns>An Option monad containing the value from the IO monad if successful, or None if an error occurred.</returns>
    public static Option<A> asOption<A>(this IO<A> io)
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
    public static async Task<Option<A>> asOptionAsync<A>(this Task<IO<A>> ioTask)
    {
        return await ioTask.foldAsync(
            _ => option.none<A>(),
            async a => await Task.FromResult(option.some(a))
        );
    }
}