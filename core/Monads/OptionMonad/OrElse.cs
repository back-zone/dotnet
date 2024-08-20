namespace back.zone.core.Monads.OptionMonad;

public static class OptionOrElse
{
    /// <summary>
    ///     Returns the current Option if it contains a value, otherwise returns the provided Option.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the Option.</typeparam>
    /// <param name="self">The current Option.</param>
    /// <param name="other">The Option to return if the current Option is empty.</param>
    /// <returns>
    ///     The current Option if it contains a value, otherwise returns the provided Option.
    ///     If an exception occurs during the evaluation, returns an empty Option.
    /// </returns>
    public static Option<TA> OrElse<TA>(
        this Option<TA> self,
        Option<TA> other
    )
    {
        try
        {
            return self.TryGetValue(out var value)
                ? value
                : other;
        }
        catch (Exception)
        {
            return Option.None<TA>();
        }
    }

    /// <summary>
    ///     Asynchronously returns the current Option if it contains a value, otherwise returns the provided Option.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the Option.</typeparam>
    /// <param name="self">The current Option.</param>
    /// <param name="otherAsync">The asynchronous Option to return if the current Option is empty.</param>
    /// <returns>
    ///     An asynchronous Task that will return the current Option if it contains a value,
    ///     otherwise returns the provided Option. If an exception occurs during the evaluation,
    ///     returns an empty Option.
    /// </returns>
    public static async Task<Option<TA>> OrElseAsync<TA>(
        this Option<TA> self,
        Task<Option<TA>> otherAsync
    )
    {
        try
        {
            return self.TryGetValue(out var value)
                ? value
                : await otherAsync.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TA>();
        }
    }

    /// <summary>
    ///     Asynchronously returns the current Option if it contains a value, otherwise returns the provided Option.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the Option.</typeparam>
    /// <param name="self">An asynchronous Task that will return the current Option.</param>
    /// <param name="other">An asynchronous Task that will return the Option to return if the current Option is empty.</param>
    /// <returns>
    ///     An asynchronous Task that will return the current Option if it contains a value,
    ///     otherwise returns the provided Option. If an exception occurs during the evaluation,
    ///     returns an empty Option.
    /// </returns>
    public static async Task<Option<TA>> OrElseAsync<TA>(
        this Task<Option<TA>> self,
        Task<Option<TA>> other
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? value
                : await other.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return Option.None<TA>();
        }
    }

    /// <summary>
    ///     Asynchronously returns the current Option if it contains a value, otherwise returns the provided Option.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the Option.</typeparam>
    /// <param name="self">An asynchronous Task that will return the current Option.</param>
    /// <param name="other">The Option to return if the current Option is empty.</param>
    /// <returns>
    ///     An asynchronous Task that will return the current Option if it contains a value,
    ///     otherwise returns the provided Option. If an exception occurs during the evaluation,
    ///     returns an empty Option.
    /// </returns>
    public static async Task<Option<TA>> OrElse<TA>(
        this Task<Option<TA>> self,
        Option<TA> other
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? value
                : other;
        }
        catch (Exception)
        {
            return Option.None<TA>();
        }
    }
}