namespace back.zone.monads.iomonad.subtypes;

/// <summary>
///     Represents a failed computation in the IO monad.
/// </summary>
/// <typeparam name="A">The type of the result.</typeparam>
public sealed class Failure<A> : IO<A> where A : notnull
{
    private readonly Exception _exception;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Failure{A}" /> class.
    /// </summary>
    /// <param name="exception">The exception that caused the failure.</param>
    internal Failure(Exception exception)
    {
        _exception = exception;
    }

    /// <inheritdoc />
    protected override bool IsSuccess()
    {
        return false;
    }

    /// <inheritdoc />
    protected override Exception GetException()
    {
        return _exception;
    }

    /// <inheritdoc />
    protected override A GetA()
    {
        throw new InvalidOperationException("This Result is a failure.");
    }

    /// <summary>
    ///     Deconstructs the failure into its exception component.
    /// </summary>
    /// <param name="exception">The exception that caused the failure.</param>
    public void Deconstruct(out Exception exception)
    {
        exception = _exception;
    }

    /// <summary>
    ///     Implicitly converts an exception into a failure.
    /// </summary>
    /// <param name="exception">The exception to convert.</param>
    /// <returns>A new failure instance.</returns>
    public static implicit operator Failure<A>(Exception exception)
    {
        return new Failure<A>(exception);
    }

    /// <summary>
    ///     Creates a new failure instance from an exception.
    /// </summary>
    /// <param name="exception">The exception to use for the failure.</param>
    /// <returns>A new failure instance.</returns>
    internal static Failure<A> Of(Exception exception)
    {
        return new Failure<A>(exception);
    }

    /// <summary>
    ///     Creates a new failure instance from a message.
    /// </summary>
    /// <param name="message">The message to use for the exception.</param>
    /// <returns>A new failure instance.</returns>
    internal static Failure<A> Of(string message)
    {
        return new Failure<A>(new Exception(message));
    }
}