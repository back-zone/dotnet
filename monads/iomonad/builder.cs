using back.zone.monads.iomonad.subtypes;
using monads.iomonad;

namespace back.zone.monads.iomonad;

/// <summary>
///     Represents an immutable container that encapsulates a value of type A and provides functionalities for working with
///     the value.
///     This class supports operations like mapping, zipping, recovering from failures, and applying functions to the
///     encapsulated value.
/// </summary>
public static class io
{
    /// <summary>
    ///     Creates an instance of <see cref="IO{A}" /> by invoking the provided function and wrapping the result in a
    ///     successful <see cref="IO{A}" /> instance.
    ///     If the function throws an exception, it is caught and wrapped in a failed <see cref="IO{A}" /> instance.
    /// </summary>
    /// <typeparam name="A">The type of the result.</typeparam>
    /// <param name="builder">A function that returns a value of type <typeparamref name="A" />.</param>
    /// <returns>
    ///     A successful <see cref="IO{A}" /> instance containing the result of the function if it does not throw an exception,
    ///     or a failed <see cref="IO{A}" /> instance containing the exception if it does.
    /// </returns>
    public static IO<A> of<A>(Func<A> builder)
    {
        try
        {
            var product = builder();
            return product is null
                ? fail<A>("of builder returned null.")
                : succeed(product);
        }
        catch (Exception e)
        {
            return fail<A>(e);
        }
    }

    /// <summary>
    ///     Asynchronously creates an instance of <see cref="IO{A}" /> by invoking the provided function and wrapping the
    ///     result in a
    ///     successful <see cref="IO{A}" /> instance.
    ///     If the function throws an exception, it is caught and wrapped in a failed <see cref="IO{A}" /> instance.
    /// </summary>
    /// <typeparam name="A">The type of the result.</typeparam>
    /// <param name="builder">
    ///     A function that returns a <see cref="Task{T}" /> containing a value of type <typeparamref name="A" />.
    /// </param>
    /// <returns>
    ///     A successful <see cref="IO{A}" /> instance containing the result of the function if it does not throw an exception,
    ///     or a failed <see cref="IO{A}" /> instance containing the exception if it does.
    ///     If the provided <paramref name="builder" /> returns a null value, a failed <see cref="IO{A}" /> instance is
    ///     returned with a message indicating the null value.
    /// </returns>
    public static async Task<IO<A>> ofAsync<A>(Task<A> builder)
    {
        try
        {
            var product = await builder;
            return product is null
                ? fail<A>("ofAsync builder returned null.")
                : succeed(product);
        }
        catch (Exception e)
        {
            return fail<A>(e);
        }
    }

    /// <summary>
    ///     Asynchronously creates an instance of <see cref="IO{A}" /> by invoking the provided function and wrapping the
    ///     result in a successful <see cref="IO{A}" /> instance.
    ///     If the function throws an exception, it is caught and wrapped in a failed <see cref="IO{A}" /> instance.
    /// </summary>
    /// <typeparam name="A">The type of the result.</typeparam>
    /// <param name="builder">
    ///     A function that returns a <see cref="Task{T}" /> containing an instance of <see cref="IO{A}" />.
    /// </param>
    /// <returns>
    ///     A successful <see cref="IO{A}" /> instance containing the result of the function if it does not throw an exception,
    ///     or a failed <see cref="IO{A}" /> instance containing the exception if it does.
    ///     If the provided <paramref name="builder" /> returns a null value, a failed <see cref="IO{A}" /> instance is
    ///     returned with a message indicating the null value.
    /// </returns>
    public static async Task<IO<A>> ofAsync<A>(Task<IO<A>> builder)
    {
        try
        {
            var product = await builder;

            return product;
        }
        catch (Exception e)
        {
            return fail<A>(e);
        }
    }

    /// <summary>
    ///     Creates an instance of <see cref="IO{A}" /> representing a successful operation with the provided value.
    /// </summary>
    /// <typeparam name="A">The type of the result.</typeparam>
    /// <param name="a">The value to be wrapped in a successful <see cref="IO{A}" /> instance.</param>
    /// <returns>
    ///     A successful <see cref="IO{A}" /> instance containing the provided value.
    /// </returns>
    public static IO<A> succeed<A>(A a) where A : notnull
    {
        return Success<A>.Of(a);
    }

    /// <summary>
    ///     Creates an instance of <see cref="IO{A}" /> representing a failed operation with the provided exception.
    /// </summary>
    /// <typeparam name="A">The type of the result.</typeparam>
    /// <param name="exception">The exception to be wrapped in a failed <see cref="IO{A}" /> instance.</param>
    /// <returns>
    ///     A failed <see cref="IO{A}" /> instance containing the provided exception.
    /// </returns>
    public static IO<A> fail<A>(Exception exception) where A : notnull
    {
        return Failure<A>.Of(exception);
    }

    /// <summary>
    ///     Creates an instance of <see cref="IO{A}" /> representing a failed operation with the provided message.
    /// </summary>
    /// <typeparam name="A">The type of the result.</typeparam>
    /// <param name="message">The message to be wrapped in a failed <see cref="IO{A}" /> instance.</param>
    /// <returns>
    ///     A failed <see cref="IO{A}" /> instance containing the provided message.
    /// </returns>
    public static IO<A> fail<A>(string message) where A : notnull
    {
        return Failure<A>.Of(message);
    }
}