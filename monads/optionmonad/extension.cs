namespace monads.optionmonad;

public static class extension
{
    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns None.
    ///     The function is expected to return a Task that represents an Option.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="asyncFlatMapper">
    ///     The function to apply to the value inside the Option. This function is expected to return
    ///     a Task that represents an Option.
    /// </param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, or if the function throws an exception, returns None.
    /// </returns>
    public static async Task<Option<B>> flatMapAsync<A, B>(
        this Task<Option<A>> optionTask,
        Func<A, Task<Option<B>>> asyncFlatMapper)
    {
        var currentTask = await optionTask;

        return await currentTask.flatMapAsync(asyncFlatMapper);
    }

    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns None.
    ///     The function is expected to return a Task that represents an Option.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="asyncMapper">
    ///     The function to apply to the value inside the Option. This function is expected to return
    ///     a Task that represents an Option.
    /// </param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, or if the function throws an exception, returns None.
    /// </returns>
    public static async Task<Option<B>> mapAsync<A, B>(
        this Task<Option<A>> optionTask,
        Func<A, Task<B>> asyncMapper)
    {
        var currentTask = await optionTask;
        return await currentTask.mapAsync(asyncMapper);
    }

    /// <summary>
    ///     Returns a new Option with the same value as the current Option, but with a different type.
    ///     The new Option will hold the provided value <paramref name="b" /> instead of the original value.
    ///     The provided value <paramref name="b" /> is expected to be a Task that will produce the new value.
    /// </summary>
    /// <typeparam name="B">The type of the new Option.</typeparam>
    /// <param name="b">
    ///     A Task that will produce the value to be held in the new Option.
    ///     This Task should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     A new Option of type <typeparamref name="B" />.
    ///     If the current Option is Some, the new Option will also be Some, holding the value awaited from the provided Task.
    ///     If the current Option is None, the new Option will also be None.
    /// </returns>
    public static async Task<Option<B>> asThisAsync<A, B>(
        this Task<Option<A>> optionTask,
        Task<B> b)
    {
        var currentTask = await optionTask;
        return await currentTask.asThisAsync(b);
    }

    /// <summary>
    ///     Combines the current Option with another Option using a specified asynchronous zipper function.
    ///     If both Options are Some, the asynchronous zipper function is applied to their values, resulting in a new Some
    ///     Option.
    ///     If either Option is None, the result is a None Option.
    /// </summary>
    /// <typeparam name="B">The type of the value in the other Option.</typeparam>
    /// <typeparam name="C">The type of the value in the resulting Option.</typeparam>
    /// <param name="other">The other Option to combine with the current Option.</param>
    /// <param name="asyncZipper">
    ///     A function that takes the values from the current Option and the other Option, and returns a Task that will produce
    ///     a new value of type C.
    ///     This function should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     A new Option of type C.
    ///     If both the current Option and the other Option are Some, the result is Some with the value obtained by awaiting
    ///     the Task returned by applying
    ///     the asynchronous zipper function.
    ///     If either the current Option or the other Option is None, the result is None.
    /// </returns>
    public static async Task<Option<C>> zipWithAsync<A, B, C>(
        this Task<Option<A>> optionTask,
        Option<B> other, Func<A, B, Task<C>> asyncZipper)
    {
        var currentTask = await optionTask;
        return await currentTask.zipWithAsync(other, asyncZipper);
    }

    /// <summary>
    ///     Combines the current Option with another Option, discarding the value from the current Option and returning the
    ///     value from the other Option. If either Option is None, the result is a None Option.
    /// </summary>
    /// <typeparam name="B">The type of the value in the other Option.</typeparam>
    /// <param name="other">The other Option to combine with the current Option.</param>
    /// <returns>
    ///     A new Option of type B.
    ///     If the other Option is Some, the result is Some with the value from the other Option.
    ///     If the current Option is None, the result is None.
    /// </returns>
    public static async Task<Option<B>> zipRightAsync<A, B>(
        this Task<Option<A>> optionTask,
        Option<B> other)
    {
        var currentTask = await optionTask;
        return await currentTask.zipRightAsync(other);
    }

    /// <summary>
    ///     Combines the current Option with another Option, producing a tuple containing their values.
    ///     If either Option is None, the result is a None Option.
    /// </summary>
    /// <typeparam name="B">The type of the value in the other Option.</typeparam>
    /// <param name="other">The other Option to combine with the current Option.</param>
    /// <returns>
    ///     A new Option of type (A, B).
    ///     If both the current Option and the other Option are Some, the result is Some with a tuple containing their values.
    ///     If either the current Option or the other Option is None, the result is None.
    /// </returns>
    public static async Task<Option<(A, B)>> zipAsync<A, B>(
        this Task<Option<A>> optionTask,
        Option<B> other)
    {
        var currentTask = await optionTask;
        return await currentTask.zipAsync(other);
    }

    /// <summary>
    ///     Combines the current Option with another Option, discarding the value from the current Option and returning the
    ///     value from the other Option. If either Option is None, the result is a None Option.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="B">The type of the value in the other Option.</typeparam>
    /// <param name="other">The other Option to combine with the current Option.</param>
    /// <returns>
    ///     A new Option of type B.
    ///     If the other Option is Some, the result is Some with the value from the other Option.
    ///     If the current Option is None, the result is None.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<Option<B>> andThenAsync<A, B>(
        this Task<Option<A>> optionTask,
        Option<B> other)
    {
        var currentTask = await optionTask;
        return await currentTask.andThenAsync(other);
    }

    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns the provided default value.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="none">
    ///     A Task that will produce the default value to be returned if the Option is None. This Task should
    ///     not throw any exceptions.
    /// </param>
    /// <param name="some">
    ///     A function to apply to the value inside the Option if it is Some. This function should not throw any
    ///     exceptions.
    /// </param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, the awaited value from the provided Task <paramref name="none" />.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<B> foldAsync<A, B>(
        this Task<Option<A>> optionTask,
        Task<B> none, Func<A, Task<B>> some)
    {
        var currentTask = await optionTask;
        return await currentTask.foldAsync(none, some);
    }

    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns the provided default value.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="none">
    ///     A default value to be returned if the Option is None.
    /// </param>
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
        B none, Func<A, Task<B>> some)
    {
        var currentTask = await optionTask;
        return await currentTask.foldAsync(none, some);
    }

    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns the provided default value.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="none">
    ///     A Task that will produce the default value to be returned if the Option is None. This Task should not throw any
    ///     exceptions.
    /// </param>
    /// <param name="some">
    ///     A function to apply to the value inside the Option if it is Some. This function should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, the awaited value from the provided Task <paramref name="none" />.
    ///     The result is represented as a Task.
    /// </returns>
    public static async Task<B> foldAsync<A, B>(
        this Task<Option<A>> optionTask,
        Task<B> none, Func<A, B> some)
    {
        var currentTask = await optionTask;
        return await currentTask.foldAsync(none, some);
    }

    /// <summary>
    ///     Returns the current Option if it contains a value, otherwise returns a new Option with the provided value.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <param name="a">The value to be returned if the current Option is None.</param>
    /// <returns>
    ///     A new Option of type A.
    ///     If the current Option is Some, the result is the current Option.
    ///     If the current Option is None, the result is a new Some Option with the provided value.
    /// </returns>
    public static async Task<Option<A>> orElseAsync<A>(
        this Task<Option<A>> optionTask,
        A a)
    {
        var currentTask = await optionTask;
        return await currentTask.orElseAsync(a);
    }

    /// <summary>
    ///     Retrieves the value held by the current Option. If the Option is None, the provided default value is returned.
    ///     This method is asynchronous and returns a Task that represents the result.
    /// </summary>
    /// <param name="a">The default value to be returned if the current Option is None.</param>
    /// <returns>
    ///     A Task that will produce the value held by the current Option if it is Some, otherwise the provided default value
    ///     <paramref name="a" />.
    /// </returns>
    public static async Task<A> getOrElseAsync<A>(
        this Task<Option<A>> optionTask,
        A a)
    {
        var currentTask = await optionTask;
        return await currentTask.getOrElseAsync(a);
    }
}