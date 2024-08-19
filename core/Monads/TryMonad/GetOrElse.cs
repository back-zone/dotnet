namespace back.zone.core.Monads.TryMonad;

public static class TryGetOrElse
{
    /// <summary>
    ///     Retrieves the value from a Try monad if successful, or returns a default value if the Try is a failure.
    /// </summary>
    /// <typeparam name="TA">The type of the value wrapped in the Try monad.</typeparam>
    /// <typeparam name="TU">The type of the default value, which must be assignable to TA.</typeparam>
    /// <param name="self">The Try monad instance to retrieve the value from.</param>
    /// <param name="other">The default value to return if the Try is a failure.</param>
    /// <returns>
    ///     The value contained in the Try if it's successful, cast to type TU;
    ///     otherwise, returns the provided default value.
    /// </returns>
    public static TU GetOrElse<TA, TU>(
        this Try<TA> self,
        TU other
    )
        where TA : notnull
        where TU : TA
    {
        return self.TryGetValue(out var value)
            ? (TU)value
            : other;
    }

    /// <summary>
    ///     Asynchronously retrieves the value from a Try monad if successful, or returns a default value from a Task if the
    ///     Try is a failure.
    /// </summary>
    /// <typeparam name="TA">The type of the value wrapped in the Try monad.</typeparam>
    /// <typeparam name="TU">The type of the default value, which must be assignable to TA.</typeparam>
    /// <param name="self">The Try monad instance to retrieve the value from.</param>
    /// <param name="otherAsync">The Task that provides the default value to return if the Try is a failure.</param>
    /// <returns>
    ///     A Task that represents the asynchronous operation. The task result contains:
    ///     - The value contained in the Try if it's successful, cast to type TU.
    ///     - The result of the otherAsync Task if the Try is a failure.
    /// </returns>
    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Try<TA> self,
        Task<TU> otherAsync
    )
        where TA : notnull
        where TU : TA
    {
        return self.TryGetValue(out var value)
            ? (TU)value
            : await otherAsync.ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously retrieves the value from a Task of Try monad if successful, or returns a default value from another
    ///     Task if the Try is a failure.
    /// </summary>
    /// <typeparam name="TA">The type of the value wrapped in the Try monad.</typeparam>
    /// <typeparam name="TU">The type of the default value, which must be assignable to TA.</typeparam>
    /// <param name="self">The Task of Try monad instance to retrieve the value from.</param>
    /// <param name="other">The Task that provides the default value to return if the Try is a failure.</param>
    /// <returns>
    ///     A Task that represents the asynchronous operation. The task result contains:
    ///     - The value contained in the Try if it's successful, cast to type TU.
    ///     - The result of the other Task if the Try is a failure.
    /// </returns>
    public static async Task<TU> GetOrElseAsync<TA, TU>(
        this Task<Try<TA>> self,
        Task<TU> other
    )
        where TA : notnull
        where TU : TA
    {
        return (await self.ConfigureAwait(false)).TryGetValue(out var value)
            ? (TU)value
            : await other.ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously retrieves the value from a Task of Try monad if successful, or returns a default value if the Try is
    ///     a failure.
    /// </summary>
    /// <typeparam name="TA">The type of the value wrapped in the Try monad.</typeparam>
    /// <typeparam name="TU">The type of the default value, which must be assignable to TA.</typeparam>
    /// <param name="self">The Task of Try monad instance to retrieve the value from.</param>
    /// <param name="other">The default value to return if the Try is a failure or if an exception occurs.</param>
    /// <returns>
    ///     A Task that represents the asynchronous operation. The task result contains:
    ///     - The value contained in the Try if it's successful, cast to type TU.
    ///     - The provided default value if the Try is a failure or if an exception occurs during the operation.
    /// </returns>
    public static async Task<TU> GetOrElse<TA, TU>(
        this Task<Try<TA>> self,
        TU other
    )
        where TA : notnull
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