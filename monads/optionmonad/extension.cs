namespace back.zone.monads.optionmonad;

public static class extension
{
    /// <summary>
    ///     Applies a flat mapping operation to the current Option asynchronously.
    ///     If the current Option is Some, the provided asynchronous flat mapper function is applied to its value,
    ///     resulting in a new Option. If the current Option is None, the result is a None Option.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the value in the resulting Option.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="asyncFlatMapper">
    ///     A function that takes the value from the current Option and returns a new Option wrapped in a Task.
    ///     This function should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     A new Option of type B.
    ///     If the current Option is Some, the result is Some with the value obtained by awaiting the Task returned by applying
    ///     the asynchronous flat mapper function.
    ///     If the current Option is None, the result is None.
    /// </returns>
    public static async Task<Option<B>> flatMapAsync<A, B>(
        this Task<Option<A>> optionTask,
        Func<A, Task<Option<B>>> asyncFlatMapper
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.flatMapAsync(asyncFlatMapper);
    }

    /// <summary>
    ///     Applies an asynchronous mapping function to the value inside the Option if it is Some.
    ///     If the Option is None, the result is a None Option.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the value in the resulting Option.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="asyncMapper">
    ///     A function that takes the value from the current Option and returns a new value wrapped in a Task.
    ///     This function should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     A new Option of type B.
    ///     If the current Option is Some, the result is Some with the value obtained by awaiting the Task returned by applying
    ///     the asynchronous mapping function.
    ///     If the current Option is None, the result is None.
    /// </returns>
    public static async Task<Option<B>> mapAsync<A, B>(
        this Task<Option<A>> optionTask,
        Func<A, Task<B>> asyncMapper
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.mapAsync(asyncMapper);
    }

    /// <summary>
    ///     Applies a transformation to the value inside the Option if it is Some, otherwise returns the provided Task of B.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the value in the resulting Option.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="bTask">
    ///     A Task that will produce the value to be returned if the Option is None.
    ///     This Task should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     If the Option is Some, the result is Some with the original value.
    ///     If the Option is None, the result is Some with the awaited value from the provided Task <paramref name="b" />.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<Option<B>> asThisAsync<A, B>(
        this Task<Option<A>> optionTask,
        Task<B> bTask
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.asThisAsync(bTask);
    }

    /// <summary>
    ///     Combines the current Option with another Option using an asynchronous zipper function.
    ///     If either Option is None, the result is a None Option.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the value in the other Option.</typeparam>
    /// <typeparam name="C">The type of the value in the resulting Option.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="other">The other Option to combine with the current Option.</param>
    /// <param name="asyncZipper">
    ///     An asynchronous function that takes the value from the current Option and the value from the other Option,
    ///     and returns a new value wrapped in a Task. This function should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     A new Option of type C.
    ///     If both the current Option and the other Option are Some, the result is Some with the value obtained by awaiting
    ///     the Task returned by applying
    ///     the asynchronous zipper function to the values from both Options.
    ///     If either the current Option or the other Option is None, the result is None.
    /// </returns>
    public static async Task<Option<C>> zipWithAsync<A, B, C>(
        this Task<Option<A>> optionTask,
        Option<B> other,
        Func<A, B, Task<C>> asyncZipper
    )
        where A : notnull
        where B : notnull
        where C : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.zipWithAsync(other, asyncZipper);
    }


    /// <summary>
    ///     Combines the current Option with another Option, discarding the value from the current Option and returning the
    ///     value from the other Option. If either Option is None, the result is a None Option.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the value in the other Option.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="other">The other Option to combine with the current Option.</param>
    /// <returns>
    ///     A new Option of type B.
    ///     If the other Option is Some, the result is Some with the value from the other Option.
    ///     If the current Option is None, the result is None.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<Option<B>> zipRightAsync<A, B>(
        this Task<Option<A>> optionTask,
        Option<B> other
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.zipRightAsync(other);
    }

    /// <summary>
    ///     Combines the current Option with another Option, returning a tuple containing their values.
    ///     If either Option is None, the result is a None Option.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the value in the other Option.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="other">The other Option to combine with the current Option.</param>
    /// <returns>
    ///     A new Option of type (A, B).
    ///     If both the current Option and the other Option are Some, the result is Some with a tuple containing their values.
    ///     If either the current Option or the other Option is None, the result is None.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<Option<(A, B)>> zipAsync<A, B>(
        this Task<Option<A>> optionTask,
        Option<B> other
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.zipAsync(other);
    }

    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns the provided Option.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the value in the resulting Option.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="other">The Option to apply if the current Option is Some.</param>
    /// <returns>
    ///     A new Option of type B.
    ///     If the current Option is Some, the result is the provided Option <paramref name="other" />.
    ///     If the current Option is None, the result is None.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<Option<B>> andThenAsync<A, B>(
        this Task<Option<A>> optionTask,
        Option<B> other
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.andThenAsync(other);
    }


    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns the provided Task of B.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="none">
    ///     A Task that will produce the value to be returned if the Option is None.
    ///     This Task should not throw any exceptions.
    /// </param>
    /// <param name="some">
    ///     A function to apply to the value inside the Option if it is Some.
    ///     This function should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, the awaited value from the provided Task <paramref name="none" />.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<B> foldAsync<A, B>(
        this Task<Option<A>> optionTask,
        Task<B> none,
        Func<A, Task<B>> some
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.foldAsync(none, some);
    }


    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns the provided default value.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="none">The default value to be returned if the Option is None.</param>
    /// <param name="some">
    ///     A function to apply to the value inside the Option if it is Some.
    ///     This function should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, the provided default value <paramref name="none" />.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<B> foldAsync<A, B>(
        this Task<Option<A>> optionTask,
        B none,
        Func<A, Task<B>> some
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.foldAsync(none, some);
    }

    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns the provided default value.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="none">The default value to be returned if the Option is None.</param>
    /// <param name="some">
    ///     A function to apply to the value inside the Option if it is Some.
    ///     This function should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, the provided default value <paramref name="none" />.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<B> foldAsync<A, B>(
        this Task<Option<A>> optionTask,
        Task<B> none,
        Func<A, B> some
    )
        where A : notnull
        where B : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.foldAsync(none, some);
    }


    /// <summary>
    ///     Returns the current Option if it is Some, otherwise returns the provided Task of Option.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="U">
    ///     The type of the value in the provided Task of Option.
    ///     This type must be a subtype of the type in the current Option.
    /// </typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="otherAsync">
    ///     A Task that will produce an Option to be returned if the current Option is None.
    ///     This Task should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     If the current Option is Some, the result is the current Option.
    ///     If the current Option is None, the result is the awaited Option from the provided Task
    ///     <paramref name="otherAsync" />.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<Option<U>> orElseAsync<A, U>(
        this Task<Option<A>> optionTask,
        Task<Option<U>> otherAsync
    )
        where A : notnull
        where U : A
    {
        var currentTask = await optionTask;
        return await currentTask.orElseAsync(otherAsync);
    }

    /// <summary>
    ///     Retrieves the value from the Option if it is Some, otherwise returns the provided default value.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="A">The type of the value in the current Option.</typeparam>
    /// <typeparam name="U">
    ///     The type of the default value to be returned if the Option is None.
    ///     This type must be a subtype of the type in the current Option.
    /// </typeparam>
    /// <param name="optionTask">The current Option wrapped in a Task.</param>
    /// <param name="u">The default value to be returned if the Option is None.</param>
    /// <returns>
    ///     If the Option is Some, the result is the value from the Option.
    ///     If the Option is None, the result is the provided default value <paramref name="u" />.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<U> getOrElse<A, U>(
        this Task<Option<A>> optionTask,
        U u
    )
        where A : notnull
        where U : A
    {
        var currentTask = await optionTask;
        return currentTask.getOrElse(u);
    }

    /// <summary>
    ///     Asynchronously filters the Option based on the provided predicate function.
    /// </summary>
    /// <typeparam name="A">The type of the value in the Option.</typeparam>
    /// <param name="optionTask">The Option wrapped in a Task.</param>
    /// <param name="predicateAsync">
    ///     An asynchronous function that takes a value of type A and returns a boolean.
    ///     This function will be applied to the value inside the Option if it is Some.
    /// </param>
    /// <returns>
    ///     A new Option of type A.
    ///     If the original Option is Some and the predicate function returns true when applied to the value,
    ///     the result is Some with the original value.
    ///     If the original Option is Some and the predicate function returns false when applied to the value,
    ///     the result is None.
    ///     If the original Option is None, the result is None.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<Option<A>> filterAsync<A>(
        this Task<Option<A>> optionTask,
        Func<A, Task<bool>> predicateAsync
    )
        where A : notnull
    {
        var currentTask = await optionTask;
        return await currentTask.filterAsync(predicateAsync);
    }

    /// <summary>
    ///     Asynchronously transforms the Option based on the provided transformer function.
    /// </summary>
    /// <typeparam name="A">The type of the value in the Option.</typeparam>
    /// <typeparam name="U">The type of the value in the resulting Option.</typeparam>
    /// <param name="optionTask">The Option wrapped in a Task.</param>
    /// <param name="transformerAsync">
    ///     An asynchronous function that takes a value of type A and returns an Option of type U.
    ///     This function will be applied to the value inside the Option if it is Some.
    /// </param>
    /// <returns>
    ///     A new Option of type U.
    ///     If the original Option is Some, the result is the Option returned by applying the transformer function to the
    ///     value.
    ///     If the original Option is None, the result is None.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<Option<U>> transformAsync<A, U>(
        this Task<Option<A>> optionTask,
        Func<A, Task<Option<U>>> transformerAsync
    )
        where A : notnull
        where U : A
    {
        var currentTask = await optionTask;
        return await currentTask.transformAsync(transformerAsync);
    }
}