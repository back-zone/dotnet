namespace back.zone.monads.eithermonad;

public static class eitherextensions
{
    /// <summary>
    ///     Asynchronously applies a function that returns an Either to the value inside the current Either, if it is in the
    ///     Right state.
    ///     If the current Either is in the Left state, the function returns the current Either without applying the function.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="L1">The type of the Left value in the returned Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="B">The type of the Right value in the returned Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="flatMapAsync">The function to apply to the Right value if the current Either is in the Right state.</param>
    /// <returns>
    ///     A new Either with the result of applying the function to the Right value if the current Either is in the Right
    ///     state, or the current Either if it is in the Left state.
    /// </returns>
    public static async Task<Either<L1, B>> flatMapAsync<L, L1, R, B>(
        this Task<Either<L, R>> eitherTask,
        Func<R, Task<Either<L1, B>>> flatMapAsync
    )
        where L : notnull
        where L1 : L
        where R : notnull
        where B : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.flatMapAsync(flatMapAsync);
    }

    /// <summary>
    ///     Asynchronously applies a function to the Right value of the current Either, if it is in the Right state.
    ///     If the current Either is in the Left state, the function returns the current Either without applying the function.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="B">The type of the Right value in the returned Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="mapAsync">The function to apply to the Right value if the current Either is in the Right state.</param>
    /// <returns>
    ///     A new Either with the result of applying the function to the Right value if the current Either is in the Right
    ///     state,
    ///     or the current Either if it is in the Left state.
    /// </returns>
    public static async Task<Either<L, B>> mapAsync<L, R, B>(
        this Task<Either<L, R>> eitherTask,
        Func<R, Task<B>> mapAsync
    )
        where L : notnull
        where R : notnull
        where B : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.mapAsync(mapAsync);
    }

    /// <summary>
    ///     Asynchronously applies a constant value to the Right value of the current Either, if it is in the Right state.
    ///     If the current Either is in the Left state, the function returns the current Either without applying the constant
    ///     value.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="B">The type of the constant value to apply.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="bTask">The Task that will provide the constant value to apply.</param>
    /// <returns>
    ///     A new Either with the constant value applied to the Right value if the current Either is in the Right state,
    ///     or the current Either if it is in the Left state.
    /// </returns>
    public static async Task<Either<L, B>> asThisAsync<L, R, B>(
        this Task<Either<L, R>> eitherTask,
        Task<B> bTask
    )
        where L : notnull
        where R : notnull
        where B : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.asThisAsync(bTask);
    }

    /// <summary>
    ///     Asynchronously applies a function that takes two values and returns a new value to the Right value of the current
    ///     Either,
    ///     if it is in the Right state. If the current Either is in the Left state, the function returns the current Either
    ///     without applying the function.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="L1">The type of the Left value in the returned Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="B">The type of the Right value in the second Either.</typeparam>
    /// <typeparam name="C">The type of the Right value in the returned Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="otherAsync">The second Either wrapped in a Task.</param>
    /// <param name="asyncZipper">The function to apply to the Right values if the current Either is in the Right state.</param>
    /// <returns>
    ///     A new Either with the result of applying the function to the Right values if the current Either is in the Right
    ///     state,
    ///     or the current Either if it is in the Left state.
    /// </returns>
    public static async Task<Either<L1, C>> zipWithAsync<L, L1, R, B, C>(
        this Task<Either<L, R>> eitherTask,
        Task<Either<L1, B>> otherAsync,
        Func<R, B, Task<C>> asyncZipper
    )
        where L : notnull
        where L1 : L
        where R : notnull
        where B : notnull
        where C : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.zipWithAsync(otherAsync, asyncZipper);
    }

    /// <summary>
    ///     Asynchronously combines the Right value of the current Either with the Right value of another Either,
    ///     if both Eithers are in the Right state. If either of the Eithers is in the Left state, the function returns
    ///     the current Either without combining the values.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="L1">The type of the Left value in the other Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="B">The type of the Right value in the other Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="otherAsync">The other Either wrapped in a Task.</param>
    /// <returns>
    ///     A new Either with the Right value of the other Either if both the current Either and the other Either are in the
    ///     Right state,
    ///     or the current Either if either of them is in the Left state.
    /// </returns>
    public static async Task<Either<L1, B>> zipRightAsync<L, L1, R, B>(
        this Task<Either<L, R>> eitherTask,
        Task<Either<L1, B>> otherAsync
    )
        where L : notnull
        where L1 : L
        where R : notnull
        where B : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.zipRightAsync(otherAsync);
    }

    /// <summary>
    ///     Asynchronously combines the Right value of the current Either with the Right value of another Either,
    ///     if both Eithers are in the Right state. If either of the Eithers is in the Left state, the function returns
    ///     the current Either without combining the values.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="L1">The type of the Left value in the other Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="B">The type of the Right value in the other Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="otherAsync">The other Either wrapped in a Task.</param>
    /// <returns>
    ///     A new Either with a tuple containing the Right values of both Eithers if both the current Either and the other
    ///     Either are in the
    ///     Right state, or the current Either if either of them is in the Left state.
    /// </returns>
    public static async Task<Either<L1, (R, B)>> zipAsync<L, L1, R, B>(
        this Task<Either<L, R>> eitherTask,
        Task<Either<L1, B>> otherAsync
    )
        where L : notnull
        where L1 : L
        where R : notnull
        where B : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.zipAsync(otherAsync);
    }

    /// <summary>
    ///     Asynchronously chains two Either tasks together. If the current Either is in the Right state, it applies the
    ///     function to the Right value and returns the result.
    ///     If the current Either is in the Left state, it returns the current Either without applying the function.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="L1">The type of the Left value in the returned Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="B">The type of the Right value in the returned Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="otherAsync">The Task that will provide the next Either to chain.</param>
    /// <returns>
    ///     A new Either with the result of applying the function to the Right value if the current Either is in the Right
    ///     state,
    ///     or the current Either if it is in the Left state.
    /// </returns>
    public static async Task<Either<L1, B>> andThenAsync<L, L1, R, B>(
        this Task<Either<L, R>> eitherTask,
        Task<Either<L1, B>> otherAsync
    )
        where L : notnull
        where L1 : L
        where R : notnull
        where B : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.andThenAsync(otherAsync);
    }

    /// <summary>
    ///     Asynchronously applies a left and right handler function to the current Either, depending on its state.
    ///     If the current Either is in the Left state, the left handler function is applied to the Left value.
    ///     If the current Either is in the Right state, the right handler function is applied to the Right value.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="U">The type of the result returned by the handler functions.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="leftHandler">The function to apply to the Left value if the current Either is in the Left state.</param>
    /// <param name="rightHandler">The function to apply to the Right value if the current Either is in the Right state.</param>
    /// <returns>
    ///     A new Task that will provide the result of applying the left or right handler function to the Left or Right value,
    ///     depending on the state of the current Either.
    /// </returns>
    public static async Task<U> foldAsync<L, R, U>(
        this Task<Either<L, R>> eitherTask,
        Func<L, Task<U>> leftHandler,
        Func<R, Task<U>> rightHandler
    )
        where L : notnull
        where R : notnull
        where U : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.foldAsync(leftHandler, rightHandler);
    }

    /// <summary>
    ///     Asynchronously applies a left and right handler function to the current Either, depending on its state.
    ///     If the current Either is in the Left state, the left handler function is applied to the Left value.
    ///     If the current Either is in the Right state, the right handler function is applied to the Right value.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="U">The type of the result returned by the handler functions.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="leftHandler">The function to apply to the Left value if the current Either is in the Left state.</param>
    /// <param name="rightHandler">The function to apply to the Right value if the current Either is in the Right state.</param>
    /// <returns>
    ///     A new Task that will provide the result of applying the left or right handler function to the Left or Right value,
    ///     depending on the state of the current Either.
    /// </returns>
    public static async Task<U> foldAsync<L, R, U>(
        this Task<Either<L, R>> eitherTask,
        Func<L, U> leftHandler,
        Func<R, Task<U>> rightHandler
    )
        where L : notnull
        where R : notnull
        where U : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.foldAsync(leftHandler, rightHandler);
    }

    /// <summary>
    ///     Asynchronously applies a left and right handler function to the current Either, depending on its state.
    ///     If the current Either is in the Left state, the left handler function is applied to the Left value.
    ///     If the current Either is in the Right state, the right handler function is applied to the Right value.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="U">The type of the result returned by the handler functions.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="leftHandler">The function to apply to the Left value if the current Either is in the Left state.</param>
    /// <param name="rightHandler">The function to apply to the Right value if the current Either is in the Right state.</param>
    /// <returns>
    ///     A new Task that will provide the result of applying the left or right handler function to the Left or Right value,
    ///     depending on the state of the current Either.
    /// </returns>
    public static async Task<U> foldAsync<L, R, U>(
        this Task<Either<L, R>> eitherTask,
        Func<L, Task<U>> leftHandler,
        Func<R, U> rightHandler
    )
        where L : notnull
        where R : notnull
        where U : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.foldAsync(leftHandler, rightHandler);
    }

    /// <summary>
    ///     Asynchronously applies a left and right handler function to the current Either, depending on its state.
    ///     If the current Either is in the Left state, the left handler function is applied to the Left value.
    ///     If the current Either is in the Right state, the right handler function is applied to the Right value.
    /// </summary>
    /// <typeparam name="U">The type of the Left and Right values in the current Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <returns>
    ///     A new Task that will provide the result of applying the left or right handler function to the Left or Right value,
    ///     depending on the state of the current Either.
    /// </returns>
    public static async Task<U> foldAsync<U>(
        this Task<Either<U, U>> eitherTask
    )
        where U : notnull
    {
        var currentTask = await eitherTask;
        return currentTask.fold(left => left, right => right);
    }

    /// <summary>
    ///     Applies a left and right handler function to the current Either, depending on its state.
    ///     If the current Either is in the Left state, the left handler function is applied to the Left value.
    ///     If the current Either is in the Right state, the right handler function is applied to the Right value.
    /// </summary>
    /// <typeparam name="U">The type of the Left and Right values in the current Either.</typeparam>
    /// <param name="either">The current Either.</param>
    /// <returns>
    ///     The result of applying the left or right handler function to the Left or Right value,
    ///     depending on the state of the current Either.
    /// </returns>
    public static U fold<U>(
        this Either<U, U> either
    )
        where U : notnull
    {
        return either.fold(left => left, right => right);
    }

    /// <summary>
    ///     Asynchronously chains two Either tasks together. If the current Either is in the Right state, it applies the
    ///     function to the Right value and returns the result. If the current Either is in the Left state, it returns the
    ///     current Either without applying the function.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="L1">The type of the Left value in the returned Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="R1">The type of the Right value in the returned Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="otherAsync">The Task that will provide the next Either to chain.</param>
    /// <returns>
    ///     A new Either with the result of applying the function to the Right value if the current Either is in the Right
    ///     state, or the current Either if it is in the Left state.
    /// </returns>
    public static async Task<Either<L1, R1>> orElseAsync<L, L1, R, R1>(
        this Task<Either<L, R>> eitherTask,
        Task<Either<L1, R1>> otherAsync
    )
        where L : notnull
        where R : notnull
        where L1 : L
        where R1 : R
    {
        var currentTask = await eitherTask;
        return await currentTask.orElseAsync(otherAsync);
    }

    /// <summary>
    ///     Asynchronously retrieves the Right value from the current Either.
    ///     If the current Either is in the Left state, the specified default value is returned.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="R1">The type of the default value to return if the current Either is in the Left state.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="r1">The default value to return if the current Either is in the Left state.</param>
    /// <returns>
    ///     A new Task that will provide the Right value if the current Either is in the Right state,
    ///     or the specified default value if the current Either is in the Left state.
    /// </returns>
    public static async Task<R1> getOrElse<L, R, R1>(
        this Task<Either<L, R>> eitherTask,
        R1 r1
    )
        where L : notnull
        where R : notnull
        where R1 : R
    {
        var currentTask = await eitherTask;
        return currentTask.getOrElse(r1);
    }

    /// <summary>
    ///     Asynchronously filters the Right values of the current Either based on the provided predicate.
    ///     If the predicate returns true for a Right value, it remains in the Either. If it returns false,
    ///     the Right value is removed from the Either.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="predicateAsync">The asynchronous predicate function to apply to each Right value.</param>
    /// <returns>
    ///     A new Task that will provide an Either with the filtered Right values.
    ///     The Left value remains unchanged if the current Either is in the Left state.
    /// </returns>
    public static async Task<Either<L, R>> filterAsync<L, R>(
        this Task<Either<L, R>> eitherTask,
        Func<R, Task<bool>> predicateAsync
    )
        where L : notnull
        where R : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.filterAsync(predicateAsync);
    }

    /// <summary>
    ///     Asynchronously transforms the Right values of the current Either based on the provided asynchronous transformer
    ///     function.
    ///     If the transformer function returns a new Either for a Right value, it replaces the original Right value in the
    ///     Either.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="L1">The type of the Left value in the returned Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <typeparam name="R1">The type of the Right value in the returned Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="asyncTransformer">The asynchronous transformer function to apply to each Right value.</param>
    /// <returns>
    ///     A new Task that will provide an Either with the transformed Right values.
    ///     The Left value remains unchanged if the current Either is in the Left state.
    /// </returns>
    public static async Task<Either<L1, R1>> transformAsync<L, L1, R, R1>(
        this Task<Either<L, R>> eitherTask,
        Func<R, Task<Either<L1, R1>>> asyncTransformer
    )
        where L : notnull
        where R : notnull
        where L1 : L
        where R1 : R
    {
        var currentTask = await eitherTask;
        return await currentTask.transformAsync(asyncTransformer);
    }

    /// <summary>
    ///     Asynchronously swaps the Left and Right values in the current Either.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <returns>
    ///     A new Task that will provide an Either with the swapped Left and Right values.
    ///     The Left value becomes the Right value, and the Right value becomes the Left value.
    /// </returns>
    public static async Task<Either<R, L>> swap<L, R>(
        this Task<Either<L, R>> eitherTask
    )
        where L : notnull
        where R : notnull
    {
        var currentTask = await eitherTask;
        return currentTask.swap();
    }

    /// <summary>
    ///     Asynchronously maps a function to the Left value of the current Either.
    ///     If the current Either is in the Left state, the provided asynchronous mapper function is applied to the Left value.
    ///     The result of the mapper function replaces the original Left value in the Either.
    ///     If the current Either is in the Right state, the Left value remains unchanged.
    /// </summary>
    /// <typeparam name="L">The type of the Left value in the current Either.</typeparam>
    /// <typeparam name="LN">The type of the Left value in the returned Either.</typeparam>
    /// <typeparam name="R">The type of the Right value in the current Either.</typeparam>
    /// <param name="eitherTask">The current Either wrapped in a Task.</param>
    /// <param name="asyncMapper">The asynchronous mapper function to apply to the Left value.</param>
    /// <returns>
    ///     A new Task that will provide an Either with the mapped Left value.
    ///     The Right value remains unchanged if the current Either is in the Right state.
    /// </returns>
    public static async Task<Either<LN, R>> mapLeftAsync<L, LN, R>(
        this Task<Either<L, R>> eitherTask,
        Func<L, Task<LN>> asyncMapper
    )
        where L : notnull
        where R : notnull
        where LN : notnull
    {
        var currentTask = await eitherTask;
        return await currentTask.mapLeftAsync(asyncMapper);
    }
}