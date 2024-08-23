using back.zone.core.Monads.OptionMonad;
using back.zone.core.Monads.TryMonad;
using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherConversions
{
    /// <summary>
    ///     Converts an Either to a Try, handling any exceptions that may occur during the conversion process.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either.</typeparam>
    /// <typeparam name="TR">The type of the right value in the Either.</typeparam>
    /// <param name="either">The Either to convert.</param>
    /// <param name="failure">A function that converts a left value to an exception.</param>
    /// <returns>
    ///     An instance of Try.
    ///     If the Either contains a right value, the Try will contain that value.
    ///     If the Either contains a left value or an exception occurs during conversion, the Try will contain the exception.
    /// </returns>
    public static Try<TR> ToTry<TL, TR>(
        this Either<TL, TR> either,
        Continuation<TL, Exception> failure
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return either.TryGetRight(out var right)
                ? Try.Succeed(right)
                : either.TryGetLeft(out var left)
                    ? failure(left)
                    : Try.Fail<TR>(new Exception("#no_left_or_right_exception#"));
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Converts an asynchronous task that returns an <see cref="Either{TL, TR}" /> to a <see cref="Try{TR}" />, handling
    ///     any exceptions that may occur during the conversion process.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either.</typeparam>
    /// <typeparam name="TR">The type of the right value in the Either.</typeparam>
    /// <param name="eitherTask">The asynchronous task that returns an Either.</param>
    /// <param name="failure">A function that converts a left value to an exception.</param>
    /// <returns>
    ///     An asynchronous task that returns a Try.
    ///     If the Either contains a right value, the Try will contain that value.
    ///     If the Either contains a left value or an exception occurs during conversion, the Try will contain the exception.
    /// </returns>
    public static async Task<Try<TR>> ToTryAsync<TL, TR>(
        this Task<Either<TL, TR>> eitherTask,
        Continuation<TL, Exception> failure
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return (await eitherTask.ConfigureAwait(false)).ToTry(failure);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Converts an Either to an Option, handling any exceptions that may occur during the conversion process.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either.</typeparam>
    /// <typeparam name="TR">The type of the right value in the Either.</typeparam>
    /// <param name="either">The Either to convert.</param>
    /// <returns>
    ///     An Option.
    ///     If the Either contains a right value, the Option will contain that value.
    ///     If the Either contains a left value or an exception occurs during conversion, the Option will be None.
    /// </returns>
    public static Option<TR> ToOption<TL, TR>(
        this Either<TL, TR> either
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return either.TryGetRight(out var right)
                ? Option.Some(right)
                : Option.None<TR>();
        }
        catch (Exception)
        {
            return Option.None<TR>();
        }
    }

    /// <summary>
    ///     Converts an asynchronous task that returns an <see cref="Either{TL, TR}" /> to an <see cref="Option{TR}" />,
    ///     handling any exceptions that may occur during the conversion process.
    /// </summary>
    /// <typeparam name="TL">The type of the left value in the Either.</typeparam>
    /// <typeparam name="TR">The type of the right value in the Either.</typeparam>
    /// <param name="eitherTask">The asynchronous task that returns an Either.</param>
    /// <returns>
    ///     An asynchronous task that returns an Option.
    ///     If the Either contains a right value, the Option will contain that value.
    ///     If the Either contains a left value or an exception occurs during conversion, the Option will be None.
    /// </returns>
    public static async Task<Option<TR>> ToOptionAsync<TL, TR>(
        this Task<Either<TL, TR>> eitherTask
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return (await eitherTask.ConfigureAwait(false)).ToOption();
        }
        catch (Exception)
        {
            return Option.None<TR>();
        }
    }
}