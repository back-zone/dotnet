using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherFlatMapLeft
{
    /// <summary>
    ///     Applies a function to the left side of an <see cref="Either{TL, TR}" />, if the instance is in the Left state.
    ///     If the instance is in the Right state, the function returns the original instance.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either.</typeparam>
    /// <typeparam name="TL1">The type to which the left side of the Either will be mapped.</typeparam>
    /// <param name="self">The Either instance to apply the function to.</param>
    /// <param name="continuation">A function that takes the left side of the Either and returns a new value of type TL1.</param>
    /// <returns>
    ///     If the instance is in the Left state, returns a new Either with the left side mapped to the result of the function.
    ///     If the instance is in the Right state, returns the original instance.
    /// </returns>
    public static Either<TL1, TR> FlatMapLeft<TL, TR, TL1>(
        this Either<TL, TR> self,
        Continuation<TL, TL1> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return EitherRunTime.RunEither(self, continuation);
    }

    /// <summary>
    ///     Applies a function to the left side of an <see cref="Either{TL, TR}" />, if the instance is in the Left state.
    ///     If the instance is in the Right state, the function returns the original instance.
    ///     This method is asynchronous and will await the result of the provided continuation function.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either.</typeparam>
    /// <typeparam name="TL1">The type to which the left side of the Either will be mapped.</typeparam>
    /// <param name="self">The Either instance to apply the function to.</param>
    /// <param name="continuation">
    ///     A function that takes the left side of the Either and returns a new <see cref="Either{TL1, TR}" />,
    ///     representing the result of the asynchronous operation.
    /// </param>
    /// <returns>
    ///     If the instance is in the Left state, returns a new <see cref="Either{TL1,TR}" /> with the left side mapped
    ///     to the result of the function.
    ///     If the instance is in the Right state, returns a new <see cref="Either{TL1, TR}" /> with the original
    ///     instance.
    /// </returns>
    public static async Task<Either<TL1, TR>> FlatMapLeftAsync<TL, TL1, TR>(
        this Either<TL, TR> self,
        Continuation<TL, Task<Either<TL1, TR>>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Applies a function to the left side of an <see cref="Either{TL, TR}" />, if the instance is in the Left state.
    ///     If the instance is in the Right state, the function returns the original instance.
    ///     This method is asynchronous and will await the result of the provided continuation function.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either.</typeparam>
    /// <typeparam name="TL1">The type to which the left side of the Either will be mapped.</typeparam>
    /// <param name="self">The Task of Either instance to apply the function to.</param>
    /// <param name="continuation">
    ///     A function that takes the left side of the Either and returns a new <see cref="Either{TL1, TR}" />,
    ///     representing the result of the asynchronous operation.
    /// </param>
    /// <returns>
    ///     If the instance is in the Left state, returns a new <see cref="Either{TL1,TR}" /> with the left side mapped
    ///     to the result of the function.
    ///     If the instance is in the Right state, returns a new <see cref="Either{TL1, TR}" /> with the original
    ///     instance.
    /// </returns>
    public static async Task<Either<TL1, TR>> FlatMapLeftAsync<TL, TL1, TR>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, Task<Either<TL1, TR>>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Applies a function to the left side of an <see cref="Either{TL, TR}" />, if the instance is in the Left state.
    ///     If the instance is in the Right state, the function returns the original instance.
    ///     This method is asynchronous and will await the result of the provided continuation function.
    /// </summary>
    /// <typeparam name="TL">The type of the left side of the Either.</typeparam>
    /// <typeparam name="TR">The type of the right side of the Either.</typeparam>
    /// <typeparam name="TL1">The type to which the left side of the Either will be mapped.</typeparam>
    /// <param name="self">The Task of Either instance to apply the function to.</param>
    /// <param name="continuation">
    ///     A function that takes the left side of the Either and returns a new <see cref="Either{TL1, TR}" />,
    ///     representing the result of the asynchronous operation.
    /// </param>
    /// <returns>
    ///     If the instance is in the Left state, returns a new <see cref="Either{TL1,TR}" /> with the left side mapped
    ///     to the result of the function.
    ///     If the instance is in the Right state, returns a new <see cref="Either{TL1, TR}" /> with the original
    ///     instance.
    /// </returns>
    public static async Task<Either<TL1, TR>> FlatMapLeftAsync<TL, TL1, TR>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, Either<TL1, TR>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return await EitherRunTime.RunEither(self, continuation).ConfigureAwait(false);
    }
}