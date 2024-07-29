using monads.iomonad;

namespace monads.result;

public static class ioextension
{
    /// <summary>
    ///     Asynchronously maps the inner value of an IO monad to a new value using an asynchronous mapper function.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <typeparam name="B">The type of the new value to be produced.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="asyncMapper">The asynchronous mapper function that takes an A and returns a Task of B.</param>
    /// <returns>An asynchronous task containing the new IO monad with the mapped value.</returns>
    public static async Task<IO<B>> mapAsync<A, B>(
        this Task<IO<A>> ioTask,
        Func<A, Task<B>> asyncMapper)
    {
        var currentTask = await ioTask;
        return await currentTask.mapAsync(asyncMapper);
    }

    /// <summary>
    ///     Asynchronously maps the inner value of an IO monad to a new IO monad using an asynchronous flat mapper function.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <typeparam name="B">The type of the new inner value to be produced.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="asyncFlatMapper">The asynchronous flat mapper function that takes an A and returns a Task of IO of B.</param>
    /// <returns>An asynchronous task containing the new IO monad with the mapped inner value.</returns>
    public static async Task<IO<B>> flatMapAsync<A, B>(
        this Task<IO<A>> ioTask,
        Func<A, Task<IO<B>>> asyncFlatMapper
    )
    {
        var currentTask = await ioTask;
        return await currentTask.flatMapAsync(asyncFlatMapper);
    }

    /// <summary>
    ///     Asynchronously maps the inner value of an IO monad to a new value using an asynchronous mapper function.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <typeparam name="B">The type of the new value to be produced.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="asyncMapper">The asynchronous mapper function that takes an A and returns a Task of B.</param>
    /// <returns>An asynchronous task containing the new IO monad with the mapped value.</returns>
    public static async Task<IO<B>> asThisAsync<A, B>(
        this Task<IO<A>> ioTask,
        Task<B> value
    )
    {
        var currentTask = await ioTask;
        return await currentTask.asThisAsync(value);
    }

    /// <summary>
    ///     Asynchronously maps the inner value of an IO monad to a new value using an asynchronous mapper function.
    ///     This function takes two tasks: one containing an IO monad of type A and another containing a function that maps A
    ///     to a Task of B.
    ///     It applies the mapper function to the inner value of the IO monad and returns a new IO monad containing the result.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <typeparam name="B">The type of the new value to be produced.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="asyncMapper">The asynchronous mapper function that takes an A and returns a Task of B.</param>
    /// <returns>An asynchronous task containing the new IO monad with the mapped value.</returns>
    public static async Task<IO<C>> zipWithAsync<A, B, C>(
        this Task<IO<A>> ioTask,
        Task<IO<B>> otherAsync, Func<A, B, Task<C>> zipper)
    {
        var currentTask = await ioTask;
        return await currentTask.zipWithAsync(otherAsync, zipper);
    }

    /// <summary>
    ///     Asynchronously combines the inner value of the first IO monad with the inner value of the second IO monad,
    ///     discarding the left value and returning a new IO monad containing the right value.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the first IO monad.</typeparam>
    /// <typeparam name="B">The type of the inner value of the second IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the first IO monad.</param>
    /// <param name="otherAsync">The asynchronous task containing the second IO monad.</param>
    /// <returns>An asynchronous task containing a new IO monad with the inner value of the second IO monad.</returns>
    public static async Task<IO<B>> zipRightAsync<A, B>(
        this Task<IO<A>> ioTask,
        Task<IO<B>> otherAsync)
    {
        var currentTask = await ioTask;
        return await currentTask.zipRightAsync(otherAsync);
    }

    /// <summary>
    ///     Asynchronously combines the inner values of two IO monads into a tuple.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the first IO monad.</typeparam>
    /// <typeparam name="B">The type of the inner value of the second IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the first IO monad.</param>
    /// <param name="otherAsync">The asynchronous task containing the second IO monad.</param>
    /// <returns>
    ///     An asynchronous task containing a new IO monad with a tuple of the inner values from the two input IO monads.
    /// </returns>
    public static async Task<IO<(A, B)>> zipAsync<A, B>(
        this Task<IO<A>> ioTask,
        Task<IO<B>> otherAsync)
    {
        var currentTask = await ioTask;
        return await currentTask.zipAsync(otherAsync);
    }

    /// <summary>
    ///     Asynchronously chains the execution of two IO monads.
    ///     The first IO monad is awaited and its inner value is used to execute the second IO monad.
    ///     The result of the second IO monad is then returned.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the first IO monad.</typeparam>
    /// <typeparam name="B">The type of the inner value of the second IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the first IO monad.</param>
    /// <param name="otherAsync">The asynchronous task containing the second IO monad.</param>
    /// <returns>An asynchronous task containing the result of the second IO monad.</returns>
    public static async Task<IO<B>> andThenAsync<A, B>(
        this Task<IO<A>> ioTask,
        Task<IO<B>> otherAsync)
    {
        var currentTask = await ioTask;
        return await currentTask.andThenAsync(otherAsync);
    }

    /// <summary>
    ///     Asynchronously folds the result of an IO monad into a new value using two asynchronous functions.
    ///     The first function is used when the IO monad contains a failure, and the second function is used when it contains a
    ///     success.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <typeparam name="B">The type of the new value to be produced.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="failureHandler">
    ///     An asynchronous function that takes an Exception and returns a Task of B.
    ///     This function is called when the IO monad contains a failure.
    /// </param>
    /// <param name="successHandler">
    ///     An asynchronous function that takes an A and returns a Task of B.
    ///     This function is called when the IO monad contains a success.
    /// </param>
    /// <returns>
    ///     An asynchronous task containing the new value produced by either the failureHandler or the successHandler,
    ///     depending on the result of the input IO monad.
    /// </returns>
    public static async Task<B> FoldAsync<A, B>(
        this Task<IO<A>> ioTask,
        Func<Exception, Task<B>> failureHandler, Func<A, Task<B>> successHandler)
    {
        var currentTask = await ioTask;
        return await currentTask.FoldAsync(failureHandler, successHandler);
    }

    /// <summary>
    ///     Asynchronously recovers from a failure in an IO monad by applying a recovery function.
    ///     If the input IO monad contains a failure, the recovery function is applied to the exception,
    ///     and the result is returned as a new IO monad containing a success.
    ///     If the input IO monad contains a success, it is returned as is.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="recoverAsync">
    ///     An asynchronous function that takes an Exception and returns a Task of A.
    ///     This function is called when the IO monad contains a failure.
    /// </param>
    /// <returns>
    ///     An asynchronous task containing the recovered IO monad.
    ///     If the input IO monad contained a failure, the returned IO monad will contain a success with the recovered value.
    ///     If the input IO monad contained a success, the returned IO monad will be the same as the input IO monad.
    /// </returns>
    public static async Task<IO<A>> recoverAsync<A>(
        this Task<IO<A>> ioTask,
        Func<Exception, Task<A>> recoverAsync)
    {
        var currentTask = await ioTask;
        return await currentTask.recoverAsync(recoverAsync);
    }
}