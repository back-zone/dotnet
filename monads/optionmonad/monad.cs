namespace monads.optionmonad;

/// <summary>
///     Represents an optional value. It can either hold a value of type <typeparamref name="A" /> or be empty.
/// </summary>
/// <typeparam name="A">The type of the optional value.</typeparam>
public abstract class Option<A>
{
    /// <summary>
    ///     Determines whether this instance holds a value.
    /// </summary>
    /// <returns><c>true</c> if this instance is some; otherwise, <c>false</c>.</returns>
    protected abstract bool IsSome();

    protected bool IsNone()
    {
        return !IsSome();
    }

    /// <summary>
    ///     Gets the value of this instance.
    /// </summary>
    /// <returns>The value of type <typeparamref name="A" /> if this instance is some; otherwise, throws an exception.</returns>
    /// <exception cref="System.InvalidOperationException">Thrown when the instance is empty (i.e., IsSome() returns false).</exception>
    protected abstract A GetA();

    public static implicit operator Option<A>(A a)
    {
        return option.some(a);
    }

    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns None.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="flatMapper">The function to apply to the value inside the Option.</param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, or if the function throws an exception, returns None.
    /// </returns>
    public Option<B> flatMap<B>(Func<A, Option<B>> flatMapper)
    {
        if (IsNone()) return option.none<B>();

        try
        {
            var result = flatMapper(GetA());
            return result;
        }
        catch (Exception _)
        {
            return option.none<B>();
        }
    }

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
    public async Task<Option<B>> flatMapAsync<B>(Func<A, Task<Option<B>>> asyncFlatMapper)
    {
        return IsSome()
            ? await option.ofAsync(asyncFlatMapper(GetA()))
            : option.none<B>();
    }

    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns None.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="mapper">The function to apply to the value inside the Option.</param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, returns None.
    /// </returns>
    public Option<B> map<B>(Func<A, B> mapper)
    {
        return IsSome()
            ? flatMap(a => option.some(mapper(a)))
            : option.none<B>();
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
    public async Task<Option<B>> mapAsync<B>(Func<A, Task<B>> asyncMapper)
    {
        return IsSome()
            ? await flatMapAsync(a => option.ofAsync(asyncMapper(a)))
            : option.none<B>();
    }

    /// <summary>
    ///     Returns a new Option with the same value as the current Option, but with a different type.
    ///     The new Option will hold the provided value <paramref name="b" /> instead of the original value.
    /// </summary>
    /// <typeparam name="B">The type of the new Option.</typeparam>
    /// <param name="b">The value to be held in the new Option.</param>
    /// <returns>A new Option of type <typeparamref name="B" /> with the value <paramref name="b" />.</returns>
    public Option<B> asThis<B>(B b)
    {
        return map(_ => b);
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
    public async Task<Option<B>> asThisAsync<B>(Task<B> b)
    {
        return await mapAsync(_ => b);
    }

    /// <summary>
    ///     Combines the current Option with another Option using a specified zipper function.
    ///     If both Options are Some, the zipper function is applied to their values, resulting in a new Some Option.
    ///     If either Option is None, the result is a None Option.
    /// </summary>
    /// <typeparam name="B">The type of the value in the other Option.</typeparam>
    /// <typeparam name="C">The type of the value in the resulting Option.</typeparam>
    /// <param name="other">The other Option to combine with the current Option.</param>
    /// <param name="zipper">
    ///     A function that takes the values from the current Option and the other Option, and returns a new value of type C.
    /// </param>
    /// <returns>
    ///     A new Option of type C.
    ///     If both the current Option and the other Option are Some, the result is Some with the value obtained by applying
    ///     the zipper function.
    ///     If either the current Option or the other Option is None, the result is None.
    /// </returns>
    public Option<C> zipWith<B, C>(Option<B> other, Func<A, B, C> zipper)
    {
        return flatMap(a => other.map(b => zipper(a, b)));
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
    public async Task<Option<C>> zipWithAsync<B, C>(Option<B> other, Func<A, B, Task<C>> asyncZipper)
    {
        return await flatMapAsync(a => other.mapAsync(b => asyncZipper(a, b)));
    }

    /// <summary>
    ///     Combines the current Option with another Option, discarding the value from the current Option and returning the
    ///     value from the other Option.
    ///     If either Option is None, the result is a None Option.
    /// </summary>
    /// <typeparam name="B">The type of the value in the other Option.</typeparam>
    /// <param name="other">The other Option to combine with the current Option.</param>
    /// <returns>
    ///     A new Option of type B.
    ///     If the other Option is Some, the result is Some with the value from the other Option.
    ///     If the current Option is None, the result is None.
    /// </returns>
    public Option<B> zipRight<B>(Option<B> other)
    {
        return zipWith(other, (_, b) => b);
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
    public async Task<Option<B>> zipRightAsync<B>(Option<B> other)
    {
        return await zipWithAsync(other, (_, b) => Task.FromResult(b));
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
    public Option<(A, B)> zip<B>(Option<B> other)
    {
        return zipWith(other, (a, b) => (a, b));
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
    public async Task<Option<(A, B)>> zipAsync<B>(Option<B> other)
    {
        return await zipWithAsync(other, (a, b) => Task.FromResult((a, b)));
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
    public Option<B> andThen<B>(Option<B> other)
    {
        return zipRight(other);
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
    public async Task<Option<B>> andThenAsync<B>(Option<B> other)
    {
        return await zipRightAsync(other);
    }

    /// <summary>
    ///     Applies a function to the value inside the Option if it is Some, otherwise returns the provided default value.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="none">The default value to be returned if the Option is None.</param>
    /// <param name="some">The function to apply to the value inside the Option if it is Some.</param>
    /// <returns>
    ///     If the Option is Some, the result of applying the function to the value.
    ///     If the Option is None, the provided default value <paramref name="none" />.
    /// </returns>
    public B fold<B>(B none, Func<A, B> some)
    {
        return IsSome() ? some(GetA()) : none;
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
    public async Task<B> foldAsync<B>(Task<B> none, Func<A, Task<B>> some)
    {
        return IsSome() ? await some(GetA()) : await none;
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
    public async Task<B> foldAsync<B>(B none, Func<A, Task<B>> some)
    {
        return IsSome() ? await some(GetA()) : none;
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
    public async Task<B> foldAsync<B>(Task<B> none, Func<A, B> some)
    {
        return IsSome() ? some(GetA()) : await none;
    }

    /// <summary>
    ///     Returns the current Option if it contains a value, otherwise returns a new Option with the provided value.
    /// </summary>
    /// <param name="a">The value to be returned if the current Option is None.</param>
    /// <returns>
    ///     A new Option of type A.
    ///     If the current Option is Some, the result is the current Option.
    ///     If the current Option is None, the result is a new Some Option with the provided value.
    /// </returns>
    public Option<A> orElse(A a)
    {
        return IsSome() ? this : option.some(a);
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
    public async Task<Option<A>> orElseAsync(A a)
    {
        return IsSome() ? this : option.some(a);
    }

    /// <summary>
    ///     Retrieves the value held by the current Option. If the Option is None, the provided default value is returned.
    /// </summary>
    /// <param name="a">The default value to be returned if the current Option is None.</param>
    /// <returns>
    ///     The value held by the current Option if it is Some, otherwise the provided default value <paramref name="a" />.
    /// </returns>
    public A getOrElse(A a)
    {
        return IsSome() ? GetA() : a;
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
    public async Task<A> getOrElseAsync(A a)
    {
        return IsSome() ? GetA() : await Task.FromResult(a);
    }
}