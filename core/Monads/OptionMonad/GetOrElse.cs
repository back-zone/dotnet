namespace back.zone.core.Monads.OptionMonad;

public static class OptionGetOrElse
{
    /// <summary>
    ///     Returns the value of the current <see cref="Option{TA}" /> if it is in a <see cref="TA" /> state,
    ///     otherwise returns a specified default value.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the <see cref="Option{TA}" />.</typeparam>
    /// <typeparam name="TU">The type of the default value. Must be a subtype of <typeparamref name="TA" />.</typeparam>
    /// <param name="self">The current <see cref="Option{TA}" />.</param>
    /// <param name="other">
    ///     The default value to return if the current <see cref="Option{TA}" /> is in a None
    ///     state.
    /// </param>
    /// <returns>
    ///     The value of the current <see cref="Option{TA}" /> if it is in a <see cref="TA" /> state,
    ///     otherwise returns the specified default value.
    /// </returns>
    public static TU GetOrElse<TA, TU>(
        this Option<TA> self,
        TU other
    )
        where TU : TA
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (TU)value
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }

    /// <summary>
    ///     Asynchronously returns the value of the current <see cref="Option{TA}" /> if it is in a <see cref="TA" /> state,
    ///     otherwise returns the result of the provided asynchronous computation.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the <see cref="Option{TA}" />.</typeparam>
    /// <typeparam name="TU">
    ///     The type of the value returned by the asynchronous computation. Must be a subtype of
    ///     <typeparamref name="TA" />.
    /// </typeparam>
    /// <param name="self">The current <see cref="Option{TA}" />.</param>
    /// <param name="otherAsync">
    ///     An asynchronous computation that returns the default value to return if the current <see cref="Option{TA}" /> is in
    ///     a None state.
    /// </param>
    /// <returns>
    ///     An asynchronous computation that returns the value of the current <see cref="Option{TA}" /> if it is in a
    ///     <see cref="TA" /> state,
    ///     otherwise returns the result of the provided asynchronous computation.
    /// </returns>
    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Option<TA> self,
        Task<TU> otherAsync
    )
        where TU : TA
    {
        return self.TryGetValue(out var value)
            ? (TU)value
            : await otherAsync.ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously returns the value of the current <see cref="Option{TA}" /> if it is in a <see cref="TA" /> state,
    ///     otherwise returns the result of the provided asynchronous computation.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the <see cref="Option{TA}" />.</typeparam>
    /// <typeparam name="TU">
    ///     The type of the value returned by the asynchronous computation. Must be a subtype of
    ///     <typeparamref name="TA" />.
    /// </typeparam>
    /// <param name="selfAsync">An asynchronous computation that returns the current <see cref="Option{TA}" />.</param>
    /// <param name="otherAsync">
    ///     An asynchronous computation that returns the default value to return if the current <see cref="Option{TA}" /> is in
    ///     a None state.
    /// </param>
    /// <returns>
    ///     An asynchronous computation that returns the value of the current <see cref="Option{TA}" /> if it is in a
    ///     <see cref="TA" /> state, otherwise returns the result of the provided asynchronous computation.
    /// </returns>
    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Task<Option<TA>> selfAsync,
        Task<TU> otherAsync
    )
        where TU : TA
    {
        var current = await selfAsync.ConfigureAwait(false);

        return current.TryGetValue(out var value)
            ? (TU)value
            : await otherAsync.ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously returns the value of the current <see cref="Option{TA}" /> if it is in a <see cref="TA" /> state,
    ///     otherwise returns the specified default value.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the <see cref="Option{TA}" />.</typeparam>
    /// <typeparam name="TU">
    ///     The type of the default value. Must be a subtype of <typeparamref name="TA" />.
    /// </typeparam>
    /// <param name="self">An asynchronous computation that returns the current <see cref="Option{TA}" />.</param>
    /// <param name="other">
    ///     The default value to return if the current <see cref="Option{TA}" /> is in a None state.
    /// </param>
    /// <returns>
    ///     An asynchronous computation that returns the value of the current <see cref="Option{TA}" /> if it is in a
    ///     <see cref="TA" /> state,
    ///     otherwise returns the specified default value.
    /// </returns>
    public static async Task<TU> GetOrElse<TA, TU>(
        this Task<Option<TA>> self,
        TU other
    )
        where TU : TA
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? (TU)value
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }
}