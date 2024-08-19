namespace back.zone.core.Monads.TryMonad;

public static class TryOrElse
{
    /// <summary>
    ///     Returns the current instance if it is in a successful state, otherwise returns the result of the provided
    ///     <paramref name="other" /> Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the current Try.</typeparam>
    /// <typeparam name="TU">The type of the value in the returned Try. Must be a subtype of <typeparamref name="TA" />.</typeparam>
    /// <param name="self">The current Try instance.</param>
    /// <param name="other">The Try instance to return if the current instance is in a failure state.</param>
    /// <returns>
    ///     If the current instance is in a successful state, returns a new Try instance containing the same value.
    ///     If the current instance is in a failure state, returns the result of the provided <paramref name="other" /> Try.
    ///     If an exception occurs during the evaluation of the current instance or the provided <paramref name="other" /> Try,
    ///     returns a new Try instance containing the exception.
    /// </returns>
    public static Try<TU> OrElse<TA, TU>(
        this Try<TA> self,
        Try<TU> other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (TU)value
                : other;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Asynchronously returns the current instance if it is in a successful state, otherwise returns the result of the
    ///     provided
    ///     <paramref name="otherAsync" /> Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the current Try.</typeparam>
    /// <typeparam name="TU">The type of the value in the returned Try. Must be a subtype of <typeparamref name="TA" />.</typeparam>
    /// <param name="self">The current Try instance.</param>
    /// <param name="otherAsync">The asynchronous Try instance to return if the current instance is in a failure state.</param>
    /// <returns>
    ///     <para>
    ///         If the current instance is in a successful state, returns a new Try instance containing the same value.
    ///     </para>
    ///     <para>
    ///         If the current instance is in a failure state, returns the result of the provided
    ///         <paramref name="otherAsync" /> Try.
    ///     </para>
    ///     <para>
    ///         If an exception occurs during the evaluation of the current instance or the provided
    ///         <paramref name="otherAsync" /> Try,
    ///         returns a new Try instance containing the exception.
    ///     </para>
    /// </returns>
    public static async Task<Try<TU>> OrElseAsync<TA, TU>(
        this Try<TA> self,
        Task<Try<TU>> otherAsync
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (TU)value
                : await otherAsync.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Asynchronously returns the current instance if it is in a successful state, otherwise returns the result of the
    ///     provided
    ///     <paramref name="otherAsync" /> Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the current Try.</typeparam>
    /// <typeparam name="TU">The type of the value in the returned Try. Must be a subtype of <typeparamref name="TA" />.</typeparam>
    /// <param name="selfAsync">The asynchronous current Try instance.</param>
    /// <param name="otherAsync">The asynchronous Try instance to return if the current instance is in a failure state.</param>
    /// <returns>
    ///     <para>
    ///         If the current instance is in a successful state, returns a new Try instance containing the same value.
    ///     </para>
    ///     <para>
    ///         If the current instance is in a failure state, returns the result of the provided
    ///         <paramref name="otherAsync" /> Try.
    ///     </para>
    ///     <para>
    ///         If an exception occurs during the evaluation of the current instance or the provided
    ///         <paramref name="otherAsync" /> Try,
    ///         returns a new Try instance containing the exception.
    ///     </para>
    /// </returns>
    public static async Task<Try<TU>> OrElseAsync<TA, TU>(
        this Task<Try<TA>> selfAsync,
        Task<Try<TU>> otherAsync
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? (TU)value
                : await otherAsync.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Asynchronously returns the current instance if it is in a successful state, otherwise returns the result of the
    ///     provided
    ///     <paramref name="other" /> Try.
    /// </summary>
    /// <typeparam name="TA">The type of the value in the current Try.</typeparam>
    /// <typeparam name="TU">The type of the value in the returned Try. Must be a subtype of <typeparamref name="TA" />.</typeparam>
    /// <param name="selfAsync">The asynchronous current Try instance.</param>
    /// <param name="other">The Try instance to return if the current instance is in a failure state.</param>
    /// <returns>
    ///     <para>
    ///         If the current instance is in a successful state, returns a new Try instance containing the same value.
    ///     </para>
    ///     <para>
    ///         If the current instance is in a failure state, returns the result of the provided <paramref name="other" />
    ///         Try.
    ///     </para>
    ///     <para>
    ///         If an exception occurs during the evaluation of the current instance or the provided <paramref name="other" />
    ///         Try,
    ///         returns a new Try instance containing the exception.
    ///     </para>
    /// </returns>
    public static async Task<Try<TU>> OrElse<TA, TU>(
        this Task<Try<TA>> selfAsync,
        Try<TU> other
    )
        where TA : notnull
        where TU : TA
    {
        try
        {
            return (await selfAsync.ConfigureAwait(false)).TryGetValue(out var value)
                ? (TU)value
                : other;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}