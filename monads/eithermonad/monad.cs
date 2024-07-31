namespace back.zone.monads.eithermonad;

/// <summary>
///     Represents a disjoint union type, commonly known as Either.
///     This class provides methods for working with either a left or right value.
/// </summary>
/// <typeparam name="L">The type of the left value.</typeparam>
/// <typeparam name="R">The type of the right value.</typeparam>
public abstract class Either<L, R>
    where L : notnull
    where R : notnull
{
    /// <summary>
    ///     Determines whether the current instance represents a left value.
    /// </summary>
    /// <returns>True if the current instance represents a left value; otherwise, false.</returns>
    public abstract bool IsLeft();

    public bool IsRight()
    {
        return !IsLeft();
    }

    /// <summary>
    ///     Gets the left value if the current instance represents a left value.
    /// </summary>
    /// <returns>The left value.</returns>
    public abstract L GetLeft();

    /// <summary>
    ///     Gets the right value if the current instance represents a right value.
    /// </summary>
    /// <returns>The right value.</returns>
    public abstract R GetRight();

    /// <summary>
    ///     Converts the current instance to an Either with the left value.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <returns>An Either with the left value.</returns>
    public static implicit operator Either<L, R>(L left)
    {
        return either.left<L, R>(left);
    }

    /// <summary>
    ///     Converts the current instance to an Either with the right value.
    /// </summary>
    /// <param name="right">The right value.</param>
    /// <returns>An Either with the right value.</returns>
    public static implicit operator Either<L, R>(R right)
    {
        return either.right<L, R>(right);
    }

    /// <summary>
    ///     Applies a function to the right value of the current instance, if it represents a right value.
    ///     If the current instance represents a left value, it returns a new instance with the same left value.
    ///     If the function throws an exception, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the returned instance.</typeparam>
    /// <param name="flatMapper">The function to apply to the right value.</param>
    /// <returns>
    ///     A new instance of Either with the left value if the current instance represents a left value.
    ///     A new instance of Either with the result of applying the function to the right value if the current instance
    ///     represents a right value.
    /// </returns>
    public Either<L1, B> flatMap<L1, B>(
        Func<R, Either<L1, B>> flatMapper
    )
        where L1 : L
        where B : notnull
    {
        if (IsLeft()) return either.left<L1, B>((L1)GetLeft());

        try
        {
            var result = flatMapper(GetRight());
            return result;
        }
        catch (Exception)
        {
            return either.left<L1, B>((L1)GetLeft());
        }
    }

    /// <summary>
    ///     Applies a function to the right value of the current instance asynchronously, if it represents a right value.
    ///     If the current instance represents a left value, it returns a new instance with the same left value.
    ///     If the function throws an exception, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the returned instance.</typeparam>
    /// <param name="asyncFlatMapper">The asynchronous function to apply to the right value.</param>
    /// <returns>
    ///     A new instance of Either with the left value if the current instance represents a left value.
    ///     A new instance of Either with the result of applying the function to the right value if the current instance
    ///     represents a right value.
    /// </returns>
    public async Task<Either<L1, B>> flatMapAsync<L1, B>(
        Func<R, Task<Either<L1, B>>> asyncFlatMapper
    )
        where L1 : L
        where B : notnull
    {
        if (IsLeft()) return either.left<L1, B>((L1)GetLeft());

        try
        {
            var result = await asyncFlatMapper(GetRight());
            return result;
        }
        catch (Exception)
        {
            return either.left<L1, B>((L1)GetLeft());
        }
    }

    /// <summary>
    ///     Applies a function to the right value of the current instance, if it represents a right value.
    ///     If the current instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="B">The type of the right value in the returned instance.</typeparam>
    /// <param name="mapper">The function to apply to the right value.</param>
    /// <returns>
    ///     A new instance of Either with the left value if the current instance represents a left value.
    ///     A new instance of Either with the result of applying the function to the right value if the current instance
    ///     represents a right value.
    /// </returns>
    public Either<L, B> map<B>(
        Func<R, B> mapper
    )
        where B : notnull
    {
        return IsRight()
            ? flatMap(a => either.right<L, B>(mapper(a)))
            : either.left<L, B>(GetLeft());
    }

    /// <summary>
    ///     Applies a function to the right value of the current instance asynchronously, if it represents a right value.
    ///     If the current instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="B">The type of the right value in the returned instance.</typeparam>
    /// <param name="asyncMapper">The asynchronous function to apply to the right value.</param>
    /// <returns>
    ///     A new instance of Either with the left value if the current instance represents a left value.
    ///     A new instance of Either with the result of applying the function to the right value if the current instance
    ///     represents a right value.
    /// </returns>
    public async Task<Either<L, B>> mapAsync<B>(
        Func<R, Task<B>> asyncMapper
    )
        where B : notnull
    {
        return IsRight()
            ? await flatMapAsync(async a => either.right<L, B>(await asyncMapper(a)))
            : either.left<L, B>(GetLeft());
    }

    /// <summary>
    ///     Creates a new instance of Either with the same right value as the current instance,
    ///     but with a different type for the right value.
    /// </summary>
    /// <typeparam name="B">The new type for the right value.</typeparam>
    /// <param name="b">The new right value.</param>
    /// <returns>A new instance of Either with the same left value as the current instance and the new right value.</returns>
    public Either<L, B> asThis<B>(
        B b
    )
        where B : notnull
    {
        return map(_ => b);
    }

    /// <summary>
    ///     Creates a new instance of Either with the same right value as the current instance,
    ///     but with a different type for the right value. The new right value is obtained asynchronously.
    /// </summary>
    /// <typeparam name="B">The new type for the right value.</typeparam>
    /// <param name="bTask">A task that will produce the new right value.</param>
    /// <returns>
    ///     A new instance of Either with the same left value as the current instance and the new right value.
    ///     The returned instance is asynchronous.
    /// </returns>
    public async Task<Either<L, B>> asThisAsync<B>(
        Task<B> bTask
    )
        where B : notnull
    {
        return await mapAsync(_ => bTask);
    }

    /// <summary>
    ///     Applies a function to the right values of the current and provided instances, if they represent right values.
    ///     If either instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the provided instance.</typeparam>
    /// <typeparam name="C">The type of the right value in the returned instance.</typeparam>
    /// <param name="other">The provided instance to zip with the current instance.</param>
    /// <param name="zipper">The function to apply to the right values of the current and provided instances.</param>
    /// <returns>
    ///     A new instance of Either with the left value if either of the current or provided instances represents a left
    ///     value.
    ///     A new instance of Either with the result of applying the function to the right values if both instances represent
    ///     right values.
    /// </returns>
    public Either<L1, C> zipWith<L1, B, C>(
        Either<L1, B> other,
        Func<R, B, C> zipper
    )
        where L1 : L
        where B : notnull
        where C : notnull
    {
        return flatMap(a => other.map(b => zipper(a, b)));
    }

    /// <summary>
    ///     Applies a function to the right values of the current and provided instances asynchronously, if they represent
    ///     right values.
    ///     If either instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the provided instance.</typeparam>
    /// <typeparam name="C">The type of the right value in the returned instance.</typeparam>
    /// <param name="otherAsync">A task that will produce the provided instance to zip with the current instance.</param>
    /// <param name="asyncZipper">The asynchronous function to apply to the right values of the current and provided instances.</param>
    /// <returns>
    ///     A new instance of Either with the left value if either of the current or provided instances represents a left
    ///     value.
    ///     A new instance of Either with the result of applying the function to the right values if both instances represent
    ///     right values.
    ///     The returned instance is asynchronous.
    /// </returns>
    public async Task<Either<L1, C>> zipWithAsync<L1, B, C>(
        Task<Either<L1, B>> otherAsync,
        Func<R, B, Task<C>> asyncZipper
    )
        where L1 : L
        where B : notnull
        where C : notnull
    {
        return await flatMapAsync(a => otherAsync.mapAsync(b => asyncZipper(a, b)));
    }

    /// <summary>
    ///     Zips the right value of the current instance with the right value of the provided instance.
    ///     If either instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the provided instance.</typeparam>
    /// <param name="other">The provided instance to zip with the current instance.</param>
    /// <returns>
    ///     A new instance of Either with the left value if either of the current or provided instances represents a left
    ///     value.
    ///     A new instance of Either with the right value of the provided instance if both instances represent right values.
    /// </returns>
    public Either<L1, B> zipRight<L1, B>(
        Either<L1, B> other
    )
        where L1 : L
        where B : notnull
    {
        return zipWith(other, (_, b) => b);
    }

    /// <summary>
    ///     Asynchronously zips the right value of the current instance with the right value of the provided instance.
    ///     If either instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the provided instance.</typeparam>
    /// <param name="otherAsync">A task that will produce the provided instance to zip with the current instance.</param>
    /// <returns>
    ///     A new instance of Either with the left value if either of the current or provided instances represents a left
    ///     value.
    ///     A new instance of Either with the right value of the provided instance if both instances represent right values.
    ///     The returned instance is asynchronous.
    /// </returns>
    public async Task<Either<L1, B>> zipRightAsync<L1, B>(
        Task<Either<L1, B>> otherAsync
    )
        where L1 : L
        where B : notnull
    {
        return await zipWithAsync(otherAsync, (_, b) => Task.FromResult(b));
    }

    /// <summary>
    ///     Zips the right value of the current instance with the right value of the provided instance.
    ///     If either instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the provided instance.</typeparam>
    /// <param name="other">The provided instance to zip with the current instance.</param>
    /// <returns>
    ///     A new instance of Either with the left value if either of the current or provided instances represents a left
    ///     value.
    ///     A new instance of Either with a tuple containing the right values of the current and provided instances if both
    ///     instances represent right values.
    /// </returns>
    public Either<L1, (R, B)> zip<L1, B>(
        Either<L1, B> other
    )
        where L1 : L
        where B : notnull
    {
        return zipWith(other, (a, b) => (a, b));
    }

    /// <summary>
    ///     Asynchronously zips the right value of the current instance with the right value of the provided instance.
    ///     If either instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the provided instance.</typeparam>
    /// <param name="otherAsync">A task that will produce the provided instance to zip with the current instance.</param>
    /// <returns>
    ///     <para>
    ///         A new instance of Either with the left value if either of the current or provided instances represents a left
    ///         value.
    ///     </para>
    ///     <para>
    ///         A new instance of Either with a tuple containing the right values of the current and provided instances if
    ///         both instances represent right values.
    ///     </para>
    ///     <para>The returned instance is asynchronous.</para>
    /// </returns>
    public async Task<Either<L1, (R, B)>> zipAsync<L1, B>(
        Task<Either<L1, B>> otherAsync
    )
        where L1 : L
        where B : notnull
    {
        return await zipWithAsync(otherAsync, (a, b) => Task.FromResult((a, b)));
    }

    /// <summary>
    ///     Performs a right-associative operation on the right value of the current instance and the right value of the
    ///     provided instance.
    ///     If either instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the provided instance.</typeparam>
    /// <param name="other">The provided instance to zip with the current instance.</param>
    /// <returns>
    ///     <para>
    ///         A new instance of Either with the left value if either of the current or provided instances represents a left
    ///         value.
    ///     </para>
    ///     <para>
    ///         A new instance of Either with the right value of the provided instance if both instances represent right
    ///         values.
    ///     </para>
    /// </returns>
    public Either<L1, B> andThen<L1, B>(
        Either<L1, B> other
    )
        where L1 : L
        where B : notnull
    {
        return zipRight(other);
    }

    /// <summary>
    ///     Performs a right-associative operation on the right value of the current instance and the right value of the
    ///     provided instance asynchronously.
    ///     If either instance represents a left value, it returns a new instance with the same left value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="B">The type of the right value in the provided instance.</typeparam>
    /// <param name="otherAsync">A task that will produce the provided instance to zip with the current instance.</param>
    /// <returns>
    ///     <para>
    ///         A new instance of Either with the left value if either of the current or provided instances represents a left
    ///         value.
    ///     </para>
    ///     <para>
    ///         A new instance of Either with the right value of the provided instance if both instances represent right
    ///         values.
    ///     </para>
    ///     <para>The returned instance is asynchronous.</para>
    /// </returns>
    public async Task<Either<L1, B>> andThenAsync<L1, B>(
        Task<Either<L1, B>> otherAsync
    )
        where L1 : L
        where B : notnull
    {
        return await zipRightAsync(otherAsync);
    }

    /// <summary>
    ///     Performs a fold operation on the Either instance, based on whether it contains a left or right value.
    /// </summary>
    /// <typeparam name="U">The type of the result.</typeparam>
    /// <param name="leftHandler">A function to handle the left value, if present.</param>
    /// <param name="rightHandler">A function to handle the right value, if present.</param>
    /// <returns>
    ///     The result of applying the appropriate handler function to the left or right value, depending on which one is
    ///     present.
    /// </returns>
    public U fold<U>(
        Func<L, U> leftHandler,
        Func<R, U> rightHandler
    )
        where U : notnull
    {
        return IsLeft()
            ? leftHandler(GetLeft())
            : rightHandler(GetRight());
    }

    /// <summary>
    ///     Performs a fold operation on the Either instance, based on whether it contains a left or right value.
    ///     The operation is asynchronous and applies different handler functions based on the presence of a left or right
    ///     value.
    /// </summary>
    /// <typeparam name="U">The type of the result.</typeparam>
    /// <param name="leftHandler">A function to handle the left value, if present. This function is asynchronous.</param>
    /// <param name="rightHandler">A function to handle the right value, if present. This function is asynchronous.</param>
    /// <returns>
    ///     The result of applying the appropriate handler function to the left or right value, depending on which one is
    ///     present.
    ///     The result is asynchronous.
    /// </returns>
    public async Task<U> foldAsync<U>(
        Func<L, Task<U>> leftHandler,
        Func<R, Task<U>> rightHandler
    )
        where U : notnull
    {
        return IsLeft()
            ? await leftHandler(GetLeft())
            : await rightHandler(GetRight());
    }

    /// <summary>
    ///     Performs a fold operation on the Either instance, based on whether it contains a left or right value.
    ///     The operation is asynchronous and applies different handler functions based on the presence of a left or right
    ///     value.
    /// </summary>
    /// <typeparam name="U">The type of the result.</typeparam>
    /// <param name="leftHandler">A function to handle the left value, if present. This function is synchronous.</param>
    /// <param name="rightHandler">A function to handle the right value, if present. This function is asynchronous.</param>
    /// <returns>
    ///     The result of applying the appropriate handler function to the left or right value, depending on which one is
    ///     present.
    ///     The result is asynchronous.
    /// </returns>
    public async Task<U> foldAsync<U>(
        Func<L, U> leftHandler,
        Func<R, Task<U>> rightHandler
    )
        where U : notnull
    {
        return IsLeft()
            ? leftHandler(GetLeft())
            : await rightHandler(GetRight());
    }

    /// <summary>
    ///     Performs a fold operation on the Either instance, based on whether it contains a left or right value.
    ///     The operation is asynchronous and applies different handler functions based on the presence of a left or right
    ///     value.
    /// </summary>
    /// <typeparam name="U">The type of the result.</typeparam>
    /// <param name="leftHandler">A function to handle the left value, if present. This function is asynchronous.</param>
    /// <param name="rightHandler">A function to handle the right value, if present. This function is synchronous.</param>
    /// <returns>
    ///     The result of applying the appropriate handler function to the left or right value, depending on which one is
    ///     present.
    ///     The result is asynchronous.
    /// </returns>
    public async Task<U> foldAsync<U>(
        Func<L, Task<U>> leftHandler,
        Func<R, U> rightHandler
    )
        where U : notnull
    {
        return IsLeft()
            ? await leftHandler(GetLeft())
            : rightHandler(GetRight());
    }

    /// <summary>
    ///     Performs a logical OR operation on the current instance of <see cref="Either{L, R}" /> with another instance.
    ///     If the current instance represents a left value, it returns the provided instance.
    ///     If the current instance represents a right value, it returns a new instance of <see cref="Either{L1, R1}" /> with
    ///     the same right value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="R1">The type of the right value in the returned instance.</typeparam>
    /// <param name="other">The instance to perform the OR operation with.</param>
    /// <returns>
    ///     A new instance of <see cref="Either{L1, R1}" /> representing the result of the OR operation.
    ///     If the current instance represents a left value, it returns the provided instance.
    ///     If the current instance represents a right value, it returns a new instance with the same right value.
    /// </returns>
    public Either<L1, R1> orElse<L1, R1>(
        Either<L1, R1> other
    )
        where L1 : L
        where R1 : R
    {
        return IsLeft() ? other : either.right<L1, R1>((R1)GetRight());
    }

    /// <summary>
    ///     Performs a logical OR operation on the current instance of <see cref="Either{L, R}" /> with another instance.
    ///     If the current instance represents a left value, it returns the provided instance.
    ///     If the current instance represents a right value, it returns a new instance of <see cref="Either{L1, R1}" /> with
    ///     the same right value.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="R1">The type of the right value in the returned instance.</typeparam>
    /// <param name="otherAsync">A task that will produce the instance to perform the OR operation with.</param>
    /// <returns>
    ///     A new instance of <see cref="Either{L1, R1}" /> representing the result of the OR operation.
    ///     If the current instance represents a left value, it returns the provided instance.
    ///     If the current instance represents a right value, it returns a new instance with the same right value.
    /// </returns>
    public async Task<Either<L1, R1>> orElseAsync<L1, R1>(
        Task<Either<L1, R1>> otherAsync
    )
        where L1 : L
        where R1 : R
    {
        return IsLeft()
            ? await otherAsync
            : either.right<L1, R1>((R1)GetRight());
    }

    /// <summary>
    ///     Retrieves the right value if the current instance represents a right value.
    ///     If the current instance represents a left value, it returns the provided default value.
    /// </summary>
    /// <typeparam name="R1">The type of the default value and the return value.</typeparam>
    /// <param name="r1">The default value to return if the current instance represents a left value.</param>
    /// <returns>
    ///     The right value if the current instance represents a right value.
    ///     Otherwise, it returns the provided default value.
    /// </returns>
    public R1 getOrElse<R1>(
        R1 r1
    )
        where R1 : R
    {
        return IsLeft() ? r1 : (R1)GetRight();
    }

    /// <summary>
    ///     Filters the current instance of <see cref="Either{L, R}" /> based on a given predicate function.
    ///     If the current instance represents a right value and the predicate function returns true,
    ///     it returns the current instance. Otherwise, it returns a new instance of <see cref="Either{L, R}" />
    ///     with the same left value.
    /// </summary>
    /// <param name="predicate">A function that takes a right value and returns a boolean indicating whether to keep the value.</param>
    /// <returns>
    ///     If the current instance represents a right value and the predicate function returns true,
    ///     it returns the current instance. Otherwise, it returns a new instance of <see cref="Either{L, R}" />
    ///     with the same left value.
    /// </returns>
    public Either<L, R> filter(Func<R, bool> predicate)
    {
        try
        {
            if (IsRight() && predicate(GetRight())) return this;
            return either.left<L, R>(GetLeft());
        }
        catch (Exception)
        {
            return either.left<L, R>(GetLeft());
        }
    }

    /// <summary>
    ///     Asynchronously filters the current instance of <see cref="Either{L, R}" /> based on a given predicate function.
    /// </summary>
    /// <param name="predicateAsync">
    ///     A function that takes a right value and returns a boolean indicating whether to keep the
    ///     value. This function is asynchronous.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the current instance represents a right value and the predicate function returns true,
    ///         it returns the current instance. Otherwise, it returns a new instance of <see cref="Either{L, R}" />
    ///         with the same left value.
    ///     </para>
    ///     <para>
    ///         If an exception occurs during the execution of the predicate function, it returns a new instance of
    ///         <see cref="Either{L, R}" /> with the same left value.
    ///     </para>
    /// </returns>
    public async Task<Either<L, R>> filterAsync(Func<R, Task<bool>> predicateAsync)
    {
        try
        {
            if (IsRight() && await predicateAsync(GetRight())) return this;
            return either.left<L, R>(GetLeft());
        }
        catch (Exception)
        {
            return either.left<L, R>(GetLeft());
        }
    }

    /// <summary>
    ///     Transforms the current instance of <see cref="Either{L, R}" /> using the provided transformer function.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="R1">The type of the right value in the returned instance.</typeparam>
    /// <param name="transformer">
    ///     A function that takes a right value and returns an instance of <see cref="Either{L1, R1}" />, representing the
    ///     transformation.
    /// </param>
    /// <returns>
    ///     A new instance of <see cref="Either{L1, R1}" /> representing the result of applying the transformer function to the
    ///     right value.
    ///     If the current instance represents a left value, it returns the current instance.
    /// </returns>
    public Either<L1, R1> transform<L1, R1>(
        Func<R, Either<L1, R1>> transformer
    )
        where L1 : L
        where R1 : R
    {
        return flatMap(transformer);
    }

    /// <summary>
    ///     Transforms the current instance of <see cref="Either{L, R}" /> using the provided asynchronous transformer
    ///     function.
    /// </summary>
    /// <typeparam name="L1">The type of the left value in the returned instance.</typeparam>
    /// <typeparam name="R1">The type of the right value in the returned instance.</typeparam>
    /// <param name="asyncTransformer">
    ///     A function that takes a right value and returns a task that represents an instance of <see cref="Either{L1, R1}" />
    ///     ,
    ///     representing the transformation.
    /// </param>
    /// <returns>
    ///     <para>
    ///         A new instance of <see cref="Either{L1, R1}" /> representing the result of applying the asynchronous
    ///         transformer function
    ///         to the right value.
    ///     </para>
    ///     <para>
    ///         If the current instance represents a left value, it returns the current instance.
    ///     </para>
    /// </returns>
    public async Task<Either<L1, R1>> transformAsync<L1, R1>(
        Func<R, Task<Either<L1, R1>>> asyncTransformer
    )
        where L1 : L
        where R1 : R
    {
        return await flatMapAsync(asyncTransformer);
    }

    /// <summary>
    ///     Swaps the left and right values of the current instance of <see cref="Either{L, R}" />.
    /// </summary>
    /// <returns>
    ///     A new instance of <see cref="Either{R, L}" /> where the left and right values are swapped.
    ///     If the current instance represents a left value, it returns a new instance with the left value as the right value.
    ///     If the current instance represents a right value, it returns a new instance with the right value as the left value.
    /// </returns>
    public Either<R, L> swap()
    {
        return IsLeft()
            ? either.right<R, L>(GetLeft())
            : either.left<R, L>(GetRight());
    }

    /// <summary>
    ///     Maps the left value of the current instance of <see cref="Either{L, R}" /> using the provided mapper function.
    /// </summary>
    /// <typeparam name="LN">The type of the new left value.</typeparam>
    /// <param name="mapper">A function that takes the current left value and returns a new left value.</param>
    /// <returns>
    ///     A new instance of <see cref="Either{LN, R}" /> where the left value is mapped using the provided mapper function.
    ///     If an exception occurs during the mapping process, the right value of the current instance is returned.
    /// </returns>
    public Either<LN, R> mapLeft<LN>(
        Func<L, LN> mapper
    )
        where LN : notnull
    {
        try
        {
            return either.left<LN, R>(mapper(GetLeft()));
        }
        catch (Exception)
        {
            return either.right<LN, R>(GetRight());
        }
    }

    /// <summary>
    ///     Asynchronously maps the left value of the current instance of <see cref="Either{L, R}" /> using the provided
    ///     asynchronous mapper function.
    /// </summary>
    /// <typeparam name="LN">The type of the new left value.</typeparam>
    /// <param name="asyncMapper">
    ///     A function that takes the current left value and returns a task representing a new left value.
    ///     This function is asynchronous.
    /// </param>
    /// <returns>
    ///     A new instance of <see cref="Either{LN, R}" /> where the left value is mapped using the provided asynchronous
    ///     mapper function.
    ///     If an exception occurs during the mapping process, the right value of the current instance is returned.
    /// </returns>
    public async Task<Either<LN, R>> mapLeftAsync<LN>(
        Func<L, Task<LN>> asyncMapper
    )
        where LN : notnull
    {
        try
        {
            return either.left<LN, R>(await asyncMapper(GetLeft()));
        }
        catch (Exception)
        {
            return either.right<LN, R>(GetRight());
        }
    }
}