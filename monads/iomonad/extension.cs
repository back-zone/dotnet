namespace back.zone.monads.iomonad;

public static class ioextensions
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
        Func<A, Task<B>> asyncMapper
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await ioTask;
        return await currentTask.mapAsync(asyncMapper);
    }

    public static async Task<IO<B>> map<A, B>(
        this Task<IO<A>> ioTask,
        Func<A, B> mapper
    )
    {
        var currentTask = await ioTask;
        return currentTask.map(mapper);
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
        where A : notnull
        where B : notnull
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
    /// <param name="bTask">The asynchronous mapper function that takes an A and returns a Task of B.</param>
    /// <returns>An asynchronous task containing the new IO monad with the mapped value.</returns>
    public static async Task<IO<B>> asThisAsync<A, B>(
        this Task<IO<A>> ioTask,
        Task<B> bTask
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await ioTask;
        return await currentTask.asThisAsync(bTask);
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
    /// <param name="otherAsync">The asynchronous mapper function that takes an A and returns a Task of B.</param>
    /// <param name="zipper">The asynchronous zip function that takes an A and B and returns a Task of C.</param>
    /// <returns>An asynchronous task containing the new IO monad with the mapped value.</returns>
    public static async Task<IO<C>> zipWithAsync<A, B, C>(
        this Task<IO<A>> ioTask,
        Task<IO<B>> otherAsync, Func<A, B, Task<C>> zipper
    )
        where A : notnull
        where B : notnull
        where C : notnull
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
        Task<IO<B>> otherAsync
    )
        where A : notnull
        where B : notnull
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
        Task<IO<B>> otherAsync
    )
        where A : notnull
        where B : notnull
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
        Task<IO<B>> otherAsync
    )
        where A : notnull
        where B : notnull
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
        Func<Exception, Task<B>> failureHandler,
        Func<A, Task<B>> successHandler
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await ioTask;
        return await currentTask.foldAsync(failureHandler, successHandler);
    }

    /// <summary>
    ///     Asynchronously folds the result of an IO monad into a new value using sync-async functions.
    ///     The first function is used when the IO monad contains a failure, and the second function is used when it contains a
    ///     success.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <typeparam name="B">The type of the new value to be produced.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="failureHandler">
    ///     A function that takes an Exception and returns a B.
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
    public static async Task<B> foldAsync<A, B>(
        this Task<IO<A>> ioTask,
        Func<Exception, B> failureHandler,
        Func<A, Task<B>> successHandler
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await ioTask;
        return await currentTask.foldAsync(failureHandler, successHandler);
    }

    /// <summary>
    ///     Asynchronously folds the result of an IO monad into a new value using sync-async functions.
    ///     The first function is used when the IO monad contains a failure, and the second function is used when it contains a
    ///     success.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <typeparam name="B">The type of the new value to be produced.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="failureHandler">
    ///     A function that takes an Exception and returns a B.
    ///     This function is called when the IO monad contains a failure.
    /// </param>
    /// <param name="successHandler">
    ///     A synchronous function that takes an A and returns a B.
    ///     This function is called when the IO monad contains a success.
    /// </param>
    /// <returns>
    ///     An asynchronous task containing the new value produced by either the failureHandler or the successHandler,
    ///     depending on the result of the input IO monad.
    /// </returns>
    public static async Task<B> foldAsync<A, B>(
        this Task<IO<A>> ioTask,
        Func<Exception, Task<B>> failureHandler,
        Func<A, B> successHandler
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await ioTask;
        return await currentTask.foldAsync(failureHandler, successHandler);
    }

    public static async Task<B> foldAsync<A, B>(
        this Task<IO<A>> ioTask,
        Func<Exception, B> failureHandler,
        Func<A, B> successHandler
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await ioTask;
        return currentTask.fold(failureHandler, successHandler);
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
        Func<Exception, Task<A>> recoverAsync
    )
        where A : notnull
    {
        var currentTask = await ioTask;
        return await currentTask.recoverAsync(recoverAsync);
    }

    /// <summary>
    ///     Asynchronously attempts to retrieve the value from the input IO monad.
    ///     If the input IO monad contains a failure, it returns the result of the otherAsync task.
    ///     If the input IO monad contains a success, it returns the same IO monad.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <typeparam name="U">The type of the inner value of the otherAsync task.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="otherAsync">The asynchronous task containing the IO monad to be returned in case of failure.</param>
    /// <returns>
    ///     An asynchronous task containing the result of either the input IO monad or the otherAsync task,
    ///     depending on the result of the input IO monad.
    /// </returns>
    public static async Task<IO<U>> orElseAsync<A, U>(
        this Task<IO<A>> ioTask,
        Task<IO<U>> otherAsync
    )
        where A : notnull
        where U : A
    {
        var currentTask = await ioTask;
        return await currentTask.orElseAsync(otherAsync);
    }

    /// <summary>
    ///     Asynchronously retrieves the value from the input IO monad.
    ///     If the input IO monad contains a failure, it returns the specified default value.
    ///     If the input IO monad contains a success, it returns the same IO monad.
    /// </summary>
    /// <typeparam name="U">The type of the inner value of the input IO monad and the default value.</typeparam>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="u">The default value to be returned in case of failure.</param>
    /// <returns>
    ///     An asynchronous task containing the inner value of the input IO monad if it contains a success.
    ///     If the input IO monad contains a failure, it returns the specified default value.
    /// </returns>
    public static async Task<U> getOrElse<U, A>(
        this Task<IO<A>> ioTask,
        U u
    )
        where A : notnull
        where U : A
    {
        var currentTask = await ioTask;
        return currentTask.getOrElse(u);
    }

    /// <summary>
    ///     Asynchronously filters the inner value of an IO monad using an asynchronous predicate function.
    ///     If the predicate function returns true for the inner value, the IO monad is returned as is.
    ///     If the predicate function returns false for the inner value, a new IO monad containing a failure is returned.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="predicateAsync">
    ///     The asynchronous predicate function that takes an A and returns a Task of bool.
    ///     This function is used to filter the inner value of the IO monad.
    /// </param>
    /// <returns>
    ///     An asynchronous task containing the filtered IO monad.
    ///     If the predicate function returns true for the inner value, the returned IO monad will be the same as the input IO
    ///     monad.
    ///     If the predicate function returns false for the inner value, a new IO monad containing a failure will be returned.
    /// </returns>
    public static async Task<IO<A>> filterAsync<A>(
        this Task<IO<A>> ioTask,
        Func<A, Task<bool>> predicateAsync
    )
        where A : notnull
    {
        var currentTask = await ioTask;
        return await currentTask.filterAsync(predicateAsync);
    }

    /// <summary>
    ///     Asynchronously transforms the inner value of an IO monad using an asynchronous transformer function.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <typeparam name="U">The type of the inner value of the returned IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="transformerAsync">
    ///     The asynchronous transformer function that takes an A and returns a Task of IO of U.
    ///     This function is used to transform the inner value of the IO monad.
    /// </param>
    /// <returns>
    ///     An asynchronous task containing the transformed IO monad.
    ///     If the transformer function returns a new IO monad for the inner value, the returned IO monad will contain the
    ///     transformed value.
    ///     If the transformer function does not return a new IO monad, the returned IO monad will be the same as the input IO
    ///     monad.
    /// </returns>
    public static async Task<IO<U>> transformAsync<A, U>(
        this Task<IO<A>> ioTask,
        Func<A, Task<IO<U>>> transformerAsync
    )
        where A : notnull
        where U : A
    {
        var currentTask = await ioTask;
        return await currentTask.transformAsync(transformerAsync);
    }

    /// <summary>
    ///     Asynchronously transforms the error of an IO monad using an asynchronous transformer function.
    /// </summary>
    /// <typeparam name="A">The type of the inner value of the input IO monad.</typeparam>
    /// <param name="ioTask">The asynchronous task containing the input IO monad.</param>
    /// <param name="errorTransformerAsync">
    ///     The asynchronous transformer function that takes an Exception and returns a Task of Exception.
    ///     This function is used to transform the error of the IO monad.
    /// </param>
    /// <returns>
    ///     An asynchronous task containing the transformed IO monad.
    ///     If the error transformer function returns a new Exception for the error, the returned IO monad will contain the
    ///     transformed error.
    ///     If the error transformer function does not return a new Exception, the returned IO monad will be the same as the
    ///     input IO monad.
    /// </returns>
    public static async Task<IO<A>> transformErrorAsync<A>(
        this Task<IO<A>> ioTask,
        Func<Exception, Task<Exception>> errorTransformerAsync
    )
        where A : notnull
    {
        var currentTask = await ioTask;
        return await currentTask.transformErrorAsync(errorTransformerAsync);
    }
}