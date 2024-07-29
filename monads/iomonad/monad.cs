using monads.result;

namespace monads.iomonad;

/// <summary>
///     Represents an immutable container that encapsulates a value of type A and provides functionalities for working with
///     the value.
///     This class supports operations like mapping, zipping, recovering from failures, and applying functions to the
///     encapsulated value.
/// </summary>
public abstract class IO<A>
{
    protected abstract bool IsSuccess();

    /// <summary>
    ///     Determines whether the current instance represents a failure state.
    /// </summary>
    /// <returns>
    ///     <c>true</c> if the current instance is in a failure state; otherwise, <c>false</c>.
    /// </returns>
    private bool IsFailure()
    {
        return !IsSuccess();
    }

    protected abstract Exception GetException();

    protected abstract A GetA();

    public static implicit operator IO<A>(A a)
    {
        return io.succeed(a);
    }

    public static implicit operator IO<A>(Exception exception)
    {
        return io.fail<A>(exception);
    }

    /// <summary>
    ///     Applies a function to the encapsulated value of the current IO instance, producing a new IO instance.
    ///     If the current instance is in a failure state, the function is not applied and the current instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="flatMapper">
    ///     A function that takes the encapsulated value of the current IO instance and returns a new IO
    ///     instance.
    /// </param>
    /// <returns>
    ///     A new IO instance containing the result of applying the function to the encapsulated value of the current instance.
    ///     If the current instance is in a failure state, the function is not applied and the current instance is returned.
    /// </returns>
    public IO<B> flatMap<B>(Func<A, IO<B>> flatMapper)
    {
        if (IsFailure()) return io.fail<B>(GetException());
        try
        {
            var result = flatMapper(GetA());
            return result;
        }
        catch (Exception e)
        {
            return io.fail<B>(e);
        }
    }

    /// <summary>
    ///     Applies an asynchronous function to the encapsulated value of the current IO instance, producing a new IO instance.
    ///     If the current instance is in a failure state, the function is not applied and the current instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the result of the asynchronous function.</typeparam>
    /// <param name="asyncFlatMapper">
    ///     A function that takes the encapsulated value of the current IO instance and returns a new IO instance wrapped in a
    ///     Task.
    /// </param>
    /// <returns>
    ///     A new IO instance containing the result of applying the asynchronous function to the encapsulated value of the
    ///     current instance.
    ///     If the current instance is in a failure state, the function is not applied and the current instance is returned.
    /// </returns>
    public async Task<IO<B>> flatMapAsync<B>(Func<A, Task<IO<B>>> asyncFlatMapper)
    {
        return IsSuccess()
            ? await io.ofAsync(asyncFlatMapper(GetA()))
            : io.fail<B>(GetException());
    }

    /// <summary>
    ///     Applies a function to the encapsulated value of the current IO instance, producing a new IO instance.
    ///     If the current instance is in a failure state, the function is not applied and the current instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="mapper">
    ///     A function that takes the encapsulated value of the current IO instance and returns a new value of type B.
    /// </param>
    /// <returns>
    ///     A new IO instance containing the result of applying the function to the encapsulated value of the current instance.
    ///     If the current instance is in a failure state, the function is not applied and the current instance is returned.
    /// </returns>
    public IO<B> map<B>(Func<A, B> mapper)
    {
        return IsSuccess()
            ? flatMap(a => io.succeed(mapper(a)))
            : io.fail<B>(GetException());
    }

    /// <summary>
    ///     Applies an asynchronous function to the encapsulated value of the current IO instance, producing a new IO instance.
    ///     If the current instance is in a failure state, the function is not applied and the current instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the result of the asynchronous function.</typeparam>
    /// <param name="asyncMapper">
    ///     A function that takes the encapsulated value of the current IO instance and returns a new IO instance wrapped in a
    ///     Task.
    /// </param>
    /// <returns>
    ///     A new IO instance containing the result of applying the asynchronous function to the encapsulated value of the
    ///     current instance.
    ///     If the current instance is in a failure state, the function is not applied and the current instance is returned.
    /// </returns>
    public async Task<IO<B>> mapAsync<B>(Func<A, Task<B>> asyncMapper)
    {
        return IsSuccess()
            ? await flatMapAsync(a => io.ofAsync(asyncMapper(a)))
            : io.fail<B>(GetException());
    }

    /// <summary>
    ///     Creates a new IO instance with the specified value.
    ///     This function is useful when you want to convert a non-IO value into an IO instance.
    /// </summary>
    /// <typeparam name="B">The type of the value to be encapsulated in the new IO instance.</typeparam>
    /// <param name="b">The value to be encapsulated in the new IO instance.</param>
    /// <returns>A new IO instance containing the specified value.</returns>
    public IO<B> asThis<B>(B b)
    {
        return map(_ => b);
    }

    /// <summary>
    ///     Creates a new IO instance with the specified value wrapped in a Task.
    ///     This function is useful when you want to convert a non-IO value wrapped in a Task into an IO instance.
    /// </summary>
    /// <typeparam name="B">The type of the value to be encapsulated in the new IO instance.</typeparam>
    /// <param name="b">A Task containing the value to be encapsulated in the new IO instance.</param>
    /// <returns>A new IO instance containing the specified value wrapped in a Task.</returns>
    public async Task<IO<B>> asThisAsync<B>(Task<B> b)
    {
        return await mapAsync(_ => b);
    }

    /// <summary>
    ///     Applies a function to the encapsulated value of the current IO instance and the encapsulated value of another IO
    ///     instance,
    ///     producing a new IO instance containing the result of the function.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the encapsulated value in the other IO instance.</typeparam>
    /// <typeparam name="C">The type of the result of the function.</typeparam>
    /// <param name="other">The other IO instance to zip with the current instance.</param>
    /// <param name="zipper">
    ///     A function that takes the encapsulated value of the current IO instance and the encapsulated value of the other IO
    ///     instance,
    ///     and returns a new value of type C.
    /// </param>
    /// <returns>
    ///     A new IO instance containing the result of applying the function to the encapsulated values of the current and
    ///     other instances.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </returns>
    public IO<C> zipWith<B, C>(IO<B> other, Func<A, B, C> zipper)
    {
        return flatMap(a => other.map(b => zipper(a, b)));
    }

    /// <summary>
    ///     Applies an asynchronous function to the encapsulated value of the current IO instance and the encapsulated value of
    ///     another IO instance,
    ///     producing a new IO instance containing the result of the function.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the encapsulated value in the other IO instance.</typeparam>
    /// <typeparam name="C">The type of the result of the function.</typeparam>
    /// <param name="otherAsync">A Task containing the other IO instance to zip with the current instance.</param>
    /// <param name="zipper">
    ///     A function that takes the encapsulated value of the current IO instance and the encapsulated value of the other IO
    ///     instance,
    ///     and returns a new Task containing a value of type C.
    /// </param>
    /// <returns>
    ///     A new IO instance containing the result of applying the function to the encapsulated values of the current and
    ///     other instances.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </returns>
    public async Task<IO<C>> zipWithAsync<B, C>(Task<IO<B>> otherAsync, Func<A, B, Task<C>> zipper)
    {
        return await flatMapAsync(a => otherAsync.mapAsync(b => zipper(a, b)));
    }

    /// <summary>
    ///     Applies the encapsulated value of the 'other' IO instance to the function, discarding the encapsulated value of the
    ///     current IO instance.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the encapsulated value in the other IO instance.</typeparam>
    /// <param name="other">The other IO instance to zip with the current instance.</param>
    /// <returns>
    ///     A new IO instance containing the encapsulated value of the 'other' IO instance.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </returns>
    public IO<B> zipRight<B>(IO<B> other)
    {
        return zipWith(other, (_, b) => b);
    }

    /// <summary>
    ///     Applies the encapsulated value of the 'other' IO instance to the function, discarding the encapsulated value of the
    ///     current IO instance.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the encapsulated value in the other IO instance.</typeparam>
    /// <param name="otherAsync">A Task containing the other IO instance to zip with the current instance.</param>
    /// <returns>
    ///     A new IO instance containing the encapsulated value of the 'other' IO instance.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </returns>
    public async Task<IO<B>> zipRightAsync<B>(Task<IO<B>> otherAsync)
    {
        return await zipWithAsync(otherAsync, (_, b) => Task.FromResult(b));
    }

    /// <summary>
    ///     Creates a new IO instance containing a tuple of the encapsulated values of the current and other IO instances.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the encapsulated value in the other IO instance.</typeparam>
    /// <param name="other">The other IO instance to zip with the current instance.</param>
    /// <returns>
    ///     A new IO instance containing a tuple of the encapsulated values of the current and other instances.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </returns>
    public IO<(A, B)> zip<B>(IO<B> other)
    {
        return zipWith(other, (a, b) => (a, b));
    }

    /// <summary>
    ///     Creates a new IO instance containing a tuple of the encapsulated values of the current and other IO instances.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the encapsulated value in the other IO instance.</typeparam>
    /// <param name="otherAsync">A Task containing the other IO instance to zip with the current instance.</param>
    /// <returns>
    ///     A new IO instance containing a tuple of the encapsulated values of the current and other instances.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </returns>
    public async Task<IO<(A, B)>> zipAsync<B>(Task<IO<B>> otherAsync)
    {
        return await zipWithAsync(otherAsync, (a, b) => Task.FromResult((a, b)));
    }

    /// <summary>
    ///     Applies the encapsulated value of the 'other' IO instance to the function, discarding the encapsulated value of the
    ///     current IO instance.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </summary>
    /// <typeparam name="B">The type of the encapsulated value in the other IO instance.</typeparam>
    /// <param name="other">The other IO instance to zip with the current instance.</param>
    /// <returns>
    ///     A new IO instance containing the encapsulated value of the 'other' IO instance.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    /// </returns>
    public IO<B> andThen<B>(IO<B> other)
    {
        return zipRight(other);
    }

    /// <summary>
    ///     Applies the encapsulated value of the 'other' IO instance to the function, discarding the encapsulated value of the
    ///     current IO instance.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    ///     This function is asynchronous and returns a new IO instance containing the encapsulated value of the 'other' IO
    ///     instance.
    /// </summary>
    /// <typeparam name="B">The type of the encapsulated value in the other IO instance.</typeparam>
    /// <param name="otherAsync">A Task containing the other IO instance to zip with the current instance.</param>
    /// <returns>
    ///     A new IO instance containing the encapsulated value of the 'other' IO instance.
    ///     If either of the current or other IO instances is in a failure state, the function is not applied and the current
    ///     instance is returned.
    ///     The returned IO instance is asynchronous.
    /// </returns>
    public async Task<IO<B>> andThenAsync<B>(Task<IO<B>> otherAsync)
    {
        return await zipRightAsync(otherAsync);
    }

    /// <summary>
    ///     Applies a function to the encapsulated value of the current IO instance based on its success or failure state.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="failureHandler">
    ///     A function that takes the exception encapsulated in the current IO instance in case of failure, and returns a new
    ///     value of type B.
    /// </param>
    /// <param name="successHandler">
    ///     A function that takes the encapsulated value of the current IO instance in case of success, and returns a new value
    ///     of type B.
    /// </param>
    /// <returns>
    ///     If the current IO instance is in a success state, the function <paramref name="successHandler" /> is applied to the
    ///     encapsulated value,
    ///     and the result is returned. If the current IO instance is in a failure state, the function
    ///     <paramref name="failureHandler" /> is applied to the encapsulated exception,
    ///     and the result is returned.
    /// </returns>
    public B fold<B>(Func<Exception, B> failureHandler, Func<A, B> successHandler)
    {
        return IsSuccess() ? successHandler(GetA()) : failureHandler(GetException());
    }

    /// <summary>
    ///     Applies a function to the encapsulated value of the current IO instance based on its success or failure state.
    ///     The function is asynchronous and returns a new IO instance containing the result.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="failureHandler">
    ///     A function that takes the exception encapsulated in the current IO instance in case of failure, and returns a new
    ///     Task containing a value of type B.
    /// </param>
    /// <param name="successHandler">
    ///     A function that takes the encapsulated value of the current IO instance in case of success, and returns a new Task
    ///     containing a value of type B.
    /// </param>
    /// <returns>
    ///     If the current IO instance is in a success state, the function <paramref name="successHandler" /> is applied to the
    ///     encapsulated value,
    ///     and the result is returned as a Task. If the current IO instance is in a failure state, the function
    ///     <paramref name="failureHandler" /> is applied to the encapsulated exception,
    ///     and the result is returned as a Task.
    /// </returns>
    public async Task<B> foldAsync<B>(Func<Exception, Task<B>> failureHandler, Func<A, Task<B>> successHandler)
    {
        return IsSuccess()
            ? await successHandler(GetA())
            : await failureHandler(GetException());
    }

    /// <summary>
    ///     Applies a function to the encapsulated value of the current IO instance based on its success or failure state.
    ///     The function is asynchronous and returns a new B instance containing the result.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="failureHandler">
    ///     A function that takes the exception encapsulated in the current IO instance in case of failure, and returns a new
    ///     value of type B.
    /// </param>
    /// <param name="successHandler">
    ///     A function that takes the encapsulated value of the current IO instance in case of success, and returns a new Task
    ///     containing a value of type B.
    /// </param>
    /// <returns>
    ///     If the current IO instance is in a success state, the function <paramref name="successHandler" /> is applied to the
    ///     encapsulated value,
    ///     and the result is returned as a Task. If the current IO instance is in a failure state, the function
    ///     <paramref name="failureHandler" /> is applied to the encapsulated exception,
    ///     and the result is returned.
    /// </returns>
    public async Task<B> foldAsync<B>(Func<Exception, B> failureHandler, Func<A, Task<B>> successHandler)
    {
        return IsSuccess()
            ? await successHandler(GetA())
            : failureHandler(GetException());
    }

    /// <summary>
    ///     Applies a function to the encapsulated value of the current IO instance based on its success or failure state.
    ///     The function is asynchronous and returns a new B instance containing the result.
    /// </summary>
    /// <typeparam name="B">The type of the result of the function.</typeparam>
    /// <param name="failureHandler">
    ///     A function that takes the exception encapsulated in the current IO instance in case of failure, and returns a new
    ///     Task containing a value of type B.
    /// </param>
    /// <param name="successHandler">
    ///     A function that takes the encapsulated value of the current IO instance in case of success, and returns a new value
    ///     of type B.
    /// </param>
    /// <returns>
    ///     If the current IO instance is in a success state, the function <paramref name="successHandler" /> is applied to the
    ///     encapsulated value,
    ///     and the result is returned as a Task. If the current IO instance is in a failure state, the function
    ///     <paramref name="failureHandler" /> is applied to the encapsulated exception,
    ///     and the result is returned as a Task.
    /// </returns>
    public async Task<B> foldAsync<B>(Func<Exception, Task<B>> failureHandler, Func<A, B> successHandler)
    {
        return IsSuccess()
            ? successHandler(GetA())
            : await failureHandler(GetException());
    }

    /// <summary>
    ///     Creates a new IO instance by applying a recovery function to the encapsulated exception if the current instance is
    ///     in a failure state.
    /// </summary>
    /// <param name="recover">A function that takes the encapsulated exception and returns a new value of type A.</param>
    /// <returns>
    ///     If the current IO instance is in a failure state, the recovery function is applied to the encapsulated exception,
    ///     and a new IO instance is returned with the recovered value.
    ///     If the current IO instance is in a success state, the original IO instance is returned without applying the
    ///     recovery function.
    /// </returns>
    public IO<A> recover(Func<Exception, A> recover)
    {
        return IsFailure() ? io.of(() => recover(GetException())) : this;
    }

    /// <summary>
    ///     Applies a recovery function to the encapsulated exception if the current instance is in a failure state.
    /// </summary>
    /// <param name="recoverAsync">
    ///     A function that takes the encapsulated exception and returns a new Task containing a value of type A.
    /// </param>
    /// <returns>
    ///     If the current IO instance is in a failure state, the recovery function is applied to the encapsulated exception,
    ///     and a new IO instance is returned with the recovered value.
    ///     If the current IO instance is in a success state, the original IO instance is returned without applying the
    ///     recovery function.
    /// </returns>
    public async Task<IO<A>> recoverAsync(Func<Exception, Task<A>> recoverAsync)
    {
        return IsFailure()
            ? await io.ofAsync(recoverAsync(GetException()))
            : this;
    }
}