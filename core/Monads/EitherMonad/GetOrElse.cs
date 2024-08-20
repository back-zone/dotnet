namespace back.zone.core.Monads.EitherMonad;

public static class EitherGetOrElse
{
    /// <summary>
    ///     Returns the value of the Right side of the Either if it exists, otherwise returns a specified default value.
    /// </summary>
    /// <typeparam name="TL">Type of the Left side of the Either.</typeparam>
    /// <typeparam name="TR">Type of the Right side of the Either.</typeparam>
    /// <typeparam name="TU">Type of the default value and the return value.</typeparam>
    /// <param name="self">The Either to get the value from.</param>
    /// <param name="other">The default value to return if the Right side does not exist.</param>
    /// <returns>The value of the Right side of the Either if it exists, otherwise the specified default value.</returns>
    public static TU GetOrElse<TL, TR, TU>(
        this Either<TL, TR> self,
        TU other
    )
        where TL : notnull
        where TR : notnull
        where TU : TR
    {
        try
        {
            return self.TryGetRight(out var right)
                ? (TU)right
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }

    /// <summary>
    ///     Asynchronously returns the value of the Right side of the Either if it exists, otherwise waits for and returns the
    ///     result of the specified asynchronous default value.
    /// </summary>
    /// <typeparam name="TL">Type of the Left side of the Either.</typeparam>
    /// <typeparam name="TR">Type of the Right side of the Either.</typeparam>
    /// <typeparam name="TU">Type of the default value and the return value.</typeparam>
    /// <param name="self">The Either to get the value from.</param>
    /// <param name="otherAsync">The asynchronous default value to return if the Right side does not exist.</param>
    /// <returns>
    ///     An asynchronous operation that returns the value of the Right side of the Either if it exists, otherwise the result
    ///     of the specified asynchronous default value.
    /// </returns>
    /// <exception cref="Exception">An exception may be thrown if the asynchronous operation fails.</exception>
    public static async Task<TU> GetOrElseAsync<TL, TR, TU>(
        this Either<TL, TR> self,
        Task<TU> otherAsync
    )
        where TL : notnull
        where TR : notnull
        where TU : TR
    {
        return self.TryGetRight(out var right)
            ? (TU)right
            : await otherAsync.ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously returns the value of the Right side of the Either if it exists, otherwise waits for and returns the
    ///     result of the specified asynchronous default value.
    /// </summary>
    /// <typeparam name="TL">Type of the Left side of the Either.</typeparam>
    /// <typeparam name="TR">Type of the Right side of the Either.</typeparam>
    /// <typeparam name="TU">Type of the default value and the return value.</typeparam>
    /// <param name="selfAsync">The asynchronous Either to get the value from.</param>
    /// <param name="otherAsync">The asynchronous default value to return if the Right side does not exist.</param>
    /// <returns>
    ///     An asynchronous operation that returns the value of the Right side of the Either if it exists, otherwise the result
    ///     of the specified asynchronous default value.
    /// </returns>
    /// <exception cref="Exception">An exception may be thrown if the asynchronous operation fails.</exception>
    public static async Task<TU> GetOrElseAsync<TL, TR, TU>(
        this Task<Either<TL, TR>> selfAsync,
        Task<TU> otherAsync
    )
        where TL : notnull
        where TR : notnull
        where TU : TR
    {
        var current = await selfAsync.ConfigureAwait(false);

        return current.TryGetRight(out var right)
            ? (TU)right
            : await otherAsync.ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously returns the value of the Right side of the Either if it exists, otherwise waits for and returns the
    ///     specified default value.
    /// </summary>
    /// <typeparam name="TL">Type of the Left side of the Either.</typeparam>
    /// <typeparam name="TR">Type of the Right side of the Either.</typeparam>
    /// <typeparam name="TU">Type of the default value and the return value.</typeparam>
    /// <param name="self">The asynchronous Either to get the value from.</param>
    /// <param name="other">The default value to return if the Right side does not exist.</param>
    /// <returns>
    ///     An asynchronous operation that returns the value of the Right side of the Either if it exists, otherwise the
    ///     specified default value.
    ///     If an exception occurs during the asynchronous operation, the default value will be returned.
    /// </returns>
    public static async Task<TU> GetOrElse<TL, TR, TU>(
        this Task<Either<TL, TR>> self,
        TU other
    )
        where TL : notnull
        where TR : notnull
        where TU : TR
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? (TU)right
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }
}