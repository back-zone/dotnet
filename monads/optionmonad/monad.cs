namespace back.zone.monads.optionmonad;

/// <summary>
///     Represents an optional value. It can either hold a value of type <typeparamref name="A" /> or be empty.
/// </summary>
/// <typeparam name="A">The type of the optional value.</typeparam>
public abstract class Option<A> where A : notnull
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
    public Option<B> flatMap<B>(
        Func<A, Option<B>> flatMapper
    )
        where B : notnull
    {
        if (IsNone()) return option.none<B>();

        try
        {
            var result = flatMapper(GetA());
            return result;
        }
        catch (Exception)
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
    public async Task<Option<B>> flatMapAsync<B>(
        Func<A, Task<Option<B>>> asyncFlatMapper
    )
        where B : notnull
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
    public Option<B> map<B>(
        Func<A, B> mapper
    )
        where B : notnull
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
    public async Task<Option<B>> mapAsync<B>(
        Func<A, Task<B>> asyncMapper
    )
        where B : notnull
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
    public Option<B> asThis<B>(
        B b
    )
        where B : notnull
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
    public async Task<Option<B>> asThisAsync<B>(
        Task<B> b
    )
        where B : notnull
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
    public Option<C> zipWith<B, C>(
        Option<B> other,
        Func<A, B, C> zipper
    )
        where B : notnull
        where C : notnull
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
    public async Task<Option<C>> zipWithAsync<B, C>(
        Option<B> other,
        Func<A, B, Task<C>> asyncZipper
    )
        where B : notnull
        where C : notnull
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
    public Option<B> zipRight<B>(
        Option<B> other
    )
        where B : notnull
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
    public async Task<Option<B>> zipRightAsync<B>(
        Option<B> other
    )
        where B : notnull
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
    public Option<(A, B)> zip<B>(
        Option<B> other
    )
        where B : notnull
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
    public async Task<Option<(A, B)>> zipAsync<B>(
        Option<B> other
    )
        where B : notnull
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
    public Option<B> andThen<B>(
        Option<B> other
    )
        where B : notnull
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
    public async Task<Option<B>> andThenAsync<B>(
        Option<B> other
    )
        where B : notnull
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
    ///     Returns the current Option if it is Some, otherwise returns the provided <paramref name="other" /> Option.
    /// </summary>
    /// <typeparam name="U">
    ///     The type of the value in the returned Option. It must be a subtype or equal to the type of the
    ///     value in the current Option.
    /// </typeparam>
    /// <param name="other">The Option to return if the current Option is None.</param>
    /// <returns>
    ///     A new Option of type <typeparamref name="U" /> with the same value as the current Option if it is Some,
    ///     otherwise a new Option of type <typeparamref name="U" /> with the same value as the provided
    ///     <paramref name="other" /> Option.
    /// </returns>
    public Option<U> orElse<U>(
        Option<U> other
    )
        where U : A
    {
        return IsSome() ? option.some((U)GetA()) : other;
    }

    /// <summary>
    ///     Returns the current Option if it is Some, otherwise returns the provided <paramref name="otherAsync" /> Option.
    /// </summary>
    /// <typeparam name="U">
    ///     The type of the value in the returned Option. It must be a subtype or equal to the type of the value in the current
    ///     Option.
    /// </typeparam>
    /// <param name="otherAsync">
    ///     A Task that will produce the Option to return if the current Option is None. This Task should not throw any
    ///     exceptions.
    /// </param>
    /// <returns>
    ///     A new Option of type <typeparamref name="U" /> with the same value as the current Option if it is Some,
    ///     otherwise a new Option of type <typeparamref name="U" /> with the same value as the provided
    ///     <paramref name="otherAsync" /> Option.
    ///     The result is represented as a Task.
    /// </returns>
    public async Task<Option<U>> orElseAsync<U>(
        Task<Option<U>> otherAsync
    )
        where U : A
    {
        return IsSome()
            ? option.some((U)GetA())
            : await otherAsync;
    }

    /// <summary>
    ///     Retrieves the value held by the current Option if it is Some.
    ///     If the current Option is None, the provided default value <paramref name="u" /> is returned.
    /// </summary>
    /// <typeparam name="U">
    ///     The type of the value to be returned. It must be a subtype or equal to the type of the value in the current Option.
    /// </typeparam>
    /// <param name="u">
    ///     The default value to be returned if the current Option is None.
    /// </param>
    /// <returns>
    ///     If the current Option is Some, the value held by the Option is returned.
    ///     If the current Option is None, the provided default value <paramref name="u" /> is returned.
    /// </returns>
    public U getOrElse<U>(U u) where U : A
    {
        return IsSome() ? (U)GetA() : u;
    }

    /// <summary>
    ///     Filters the current Option based on a specified predicate function.
    ///     If the predicate function returns true for the value in the Option, the Option remains unchanged.
    ///     If the predicate function returns false or throws an exception, the Option is transformed into a None Option.
    /// </summary>
    /// <param name="predicate">
    ///     A function that takes the value in the Option and returns a boolean.
    ///     If the function returns true, the Option remains unchanged.
    ///     If the function returns false or throws an exception, the Option is transformed into a None Option.
    /// </param>
    /// <returns>
    ///     If the predicate function returns true for the value in the Option, the Option remains unchanged.
    ///     If the predicate function returns false or throws an exception, a new None Option is returned.
    /// </returns>
    public Option<A> filter(Func<A, bool> predicate)
    {
        if (IsNone()) return option.none<A>();

        try
        {
            return predicate(GetA()) ? this : option.none<A>();
        }
        catch (Exception)
        {
            return option.none<A>();
        }
    }

    /// <summary>
    ///     Asynchronously filters the current Option based on a specified predicate function.
    ///     If the predicate function returns true for the value in the Option, the Option remains unchanged.
    ///     If the predicate function returns false or throws an exception, the Option is transformed into a None Option.
    /// </summary>
    /// <param name="predicateAsync">
    ///     A function that takes the value in the Option and returns a Task of boolean.
    ///     If the function returns true, the Option remains unchanged.
    ///     If the function returns false or throws an exception, the Option is transformed into a None Option.
    /// </param>
    /// <returns>
    ///     If the predicate function returns true for the value in the Option, the Option remains unchanged.
    ///     If the predicate function returns false or throws an exception, a new None Option is returned.
    /// </returns>
    public async Task<Option<A>> filterAsync(Func<A, Task<bool>> predicateAsync)
    {
        if (IsNone()) return option.none<A>();

        try
        {
            return await predicateAsync(GetA()) ? this : option.none<A>();
        }
        catch (Exception)
        {
            return option.none<A>();
        }
    }

    /// <summary>
    ///     Transforms the current Option using the provided transformer function.
    /// </summary>
    /// <typeparam name="U">The type of the value in the returned Option.</typeparam>
    /// <param name="transformer">
    ///     A function that takes the value in the current Option and returns a new Option of type U.
    /// </param>
    /// <returns>
    ///     A new Option of type U. If the current Option is Some, the transformer function is applied to its value,
    ///     and the result is returned as a new Some Option. If the current Option is None, a new None Option is returned.
    /// </returns>
    public Option<U> transform<U>(
        Func<A, Option<U>> transformer
    )
        where U : A
    {
        return flatMap(transformer);
    }

    /// <summary>
    ///     Transforms the current Option asynchronously using the provided transformer function.
    /// </summary>
    /// <typeparam name="U">The type of the value in the returned Option.</typeparam>
    /// <param name="transformerAsync">
    ///     A function that takes the value in the current Option and returns a Task of an Option of type U.
    ///     This function should not throw any exceptions.
    /// </param>
    /// <returns>
    ///     A new Option of type U.
    ///     If the current Option is Some, the transformer function is applied to its value asynchronously,
    ///     and the result is returned as a new Some Option.
    ///     If the current Option is None, a new None Option is returned.
    ///     The result is represented as a Task.
    /// </returns>
    public async Task<Option<U>> transformAsync<U>(
        Func<A, Task<Option<U>>> transformerAsync
    )
        where U : A
    {
        return await flatMapAsync(transformerAsync);
    }
}