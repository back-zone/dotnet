namespace back.zone.core.Monads.EitherMonad;

public static class EitherOrElse
{
    /// <summary>
    ///     Returns the current Either instance if it contains a Right value, otherwise returns the specified other Either
    ///     instance.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the Right value.</typeparam>
    /// <param name="self">The current Either instance.</param>
    /// <param name="other">The other Either instance to return if the current instance contains a Left value.</param>
    /// <returns>
    ///     The current Either instance if it contains a Right value, otherwise returns the specified other Either instance.
    ///     If an exception occurs during the evaluation, the specified other Either instance is returned.
    /// </returns>
    public static Either<TL, TR> OrElse<TL, TR>(
        this Either<TL, TR> self,
        Either<TL, TR> other
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? right
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }

    /// <summary>
    ///     Asynchronously returns the current Either instance if it contains a Right value, otherwise returns the specified
    ///     other Either
    ///     instance.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the Right value.</typeparam>
    /// <param name="self">The current Either instance.</param>
    /// <param name="otherAsync">The other Either instance to return if the current instance contains a Left value.</param>
    /// <returns>
    ///     An asynchronous task that represents the result of the operation.
    ///     If the current Either instance contains a Right value, the task will return the current instance.
    ///     Otherwise, the task will return the specified other Either instance.
    ///     If an exception occurs during the evaluation, the task will return the specified other Either instance.
    /// </returns>
    public static async Task<Either<TL, TR>> OrElseAsync<TL, TR>(
        this Either<TL, TR> self,
        Task<Either<TL, TR>> otherAsync
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? right
                : await otherAsync.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return await otherAsync.ConfigureAwait(false);
        }
    }

    /// <summary>
    ///     Asynchronously returns the current Either instance if it contains a Right value, otherwise returns the specified
    ///     other Either instance.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the Right value.</typeparam>
    /// <param name="self">The current Either instance as a task.</param>
    /// <param name="other">The other Either instance to return if the current instance contains a Left value as a task.</param>
    /// <returns>
    ///     An asynchronous task that represents the result of the operation.
    ///     If the current Either instance contains a Right value, the task will return the current instance.
    ///     Otherwise, the task will return the specified other Either instance.
    ///     If an exception occurs during the evaluation, the task will return the specified other Either instance.
    /// </returns>
    public static async Task<Either<TL, TR>> OrElseAsync<TL, TR>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR>> other
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? right
                : await other.ConfigureAwait(false);
        }
        catch (Exception)
        {
            return await other.ConfigureAwait(false);
        }
    }

    /// <summary>
    ///     Asynchronously returns the current Either instance if it contains a Right value, otherwise returns the specified
    ///     other Either instance.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the Right value.</typeparam>
    /// <param name="self">The current Either instance as a task.</param>
    /// <param name="other">The other Either instance to return if the current instance contains a Left value.</param>
    /// <returns>
    ///     An asynchronous task that represents the result of the operation.
    ///     If the current Either instance contains a Right value, the task will return the current instance.
    ///     Otherwise, the task will return the specified other Either instance.
    ///     If an exception occurs during the evaluation, the task will return the specified other Either instance.
    /// </returns>
    public static async Task<Either<TL, TR>> OrElse<TL, TR>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR> other
    )
        where TL : notnull
        where TR : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? right
                : other;
        }
        catch (Exception)
        {
            return other;
        }
    }
}