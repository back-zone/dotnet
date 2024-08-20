using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionFlatMap
{
    /// <summary>
    ///     Applies a function to the value inside an <see cref="Option{TA}" /> and returns a new <see cref="Option{TB}" />.
    ///     If the <see cref="Option{TA}" /> is None, the function is not applied and the result is also None.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the <see cref="Option{TA}" />.</typeparam>
    /// <typeparam name="TB">The type of the value inside the returned <see cref="Option{TB}" />.</typeparam>
    /// <param name="self">The <see cref="Option{TA}" /> to apply the function to.</param>
    /// <param name="continuation">
    ///     A function that takes a value of type TA and returns an <see cref="Option{TB}" />.
    ///     This function is applied to the value inside the <see cref="Option{TA}" /> if it is Some.
    /// </param>
    /// <returns>
    ///     A new <see cref="Option{TB}" /> containing the result of applying the function to the value inside the
    ///     <see cref="Option{TA}" />.
    ///     If the <see cref="Option{TA}" /> is None, the result is also None.
    /// </returns>
    public static Option<TB> FlatMap<TA, TB>(
        this Option<TA> self,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        return OptionRuntime.RunOption(self, continuation);
    }

    /// <summary>
    ///     Applies a function to the value inside a task that returns an <see cref="Option{TA}" /> and returns a new
    ///     <see cref="Option{TB}" /> asynchronously.
    ///     If the <see cref="Option{TA}" /> is None, the function is not applied and the result is also None.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the <see cref="Option{TA}" />.</typeparam>
    /// <typeparam name="TB">The type of the value inside the returned <see cref="Option{TB}" />.</typeparam>
    /// <param name="self">The <see cref="Option{TA}" /> to apply the function to.</param>
    /// <param name="continuation">
    ///     A function that takes a value of type TA and returns a task that returns an <see cref="Option{TB}" />.
    ///     This function is applied to the value inside the <see cref="Option{TA}" /> if it is Some.
    /// </param>
    /// <returns>
    ///     A new <see cref="Option{TB}" /> containing the result of applying the function to the value inside the
    ///     <see cref="Option{TA}" /> asynchronously.
    ///     If the <see cref="Option{TA}" /> is None, the result is also None.
    /// </returns>
    public static async Task<Option<TB>> FlatMapAsync<TA, TB>(
        this Option<TA> self,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }


    /// <summary>
    ///     Applies a function to the value inside a task that returns an <see cref="Option{TA}" /> and returns a new
    ///     <see cref="Option{TB}" /> asynchronously.
    ///     If the <see cref="Option{TA}" /> is None, the function is not applied and the result is also None.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the <see cref="Option{TA}" />.</typeparam>
    /// <typeparam name="TB">The type of the value inside the returned <see cref="Option{TB}" />.</typeparam>
    /// <param name="self">The <see cref="Option{TA}" /> to apply the function to.</param>
    /// <param name="continuation">
    ///     A function that takes a value of type TA and returns a <see cref="Option{TB}" />.
    ///     This function is applied to the value inside the <see cref="Option{TA}" /> if it is Some.
    /// </param>
    /// <returns>
    ///     A new <see cref="Option{TB}" /> containing the result of applying the function to the value inside the
    ///     <see cref="Option{TA}" /> asynchronously.
    ///     If the <see cref="Option{TA}" /> is None, the result is also None.
    /// </returns>
    public static async Task<Option<TB>> FlatMapAsync<TA, TB>(
        this Task<Option<TA>> self,
        OptionalContinuation<TA, Task<Option<TB>>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Applies a function to the value inside a task that returns an <see cref="Option{TA}" /> and returns a new
    ///     <see cref="Option{TB}" /> asynchronously.
    ///     If the <see cref="Option{TA}" /> is None, the function is not applied and the result is also None.
    /// </summary>
    /// <typeparam name="TA">The type of the value inside the <see cref="Option{TA}" />.</typeparam>
    /// <typeparam name="TB">The type of the value inside the returned <see cref="Option{TB}" />.</typeparam>
    /// <param name="self">
    ///     The task of <see cref="Option{TA}" /> to apply the function to. This task should contain an
    ///     <see cref="Option{TA}" />.
    /// </param>
    /// <param name="continuation">
    ///     A function that takes a value of type TA and returns an <see cref="Option{TB}" />.
    ///     This function is applied to the value inside the <see cref="Option{TA}" /> if it is Some.
    /// </param>
    /// <returns>
    ///     A new task of <see cref="Option{TB}" /> containing the result of applying the function to the value inside the
    ///     <see cref="Option{TA}" /> asynchronously.
    ///     If the <see cref="Option{TA}" /> is None, the result is also None.
    /// </returns>
    public static async Task<Option<TB>> FlatMap<TA, TB>(
        this Task<Option<TA>> self,
        OptionalContinuation<TA, Option<TB>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }
}