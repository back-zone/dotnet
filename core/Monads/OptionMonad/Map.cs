using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionMap
{
    /// <summary>
    ///     Maps the given continuation function over the <see cref="Option{TA}" /> instance.
    ///     If the <see cref="Option{TA}" /> is in a Some state, the continuation function is applied to the contained value.
    ///     If the <see cref="Option{TA}" /> is in a None state, the continuation function is not invoked and the result is
    ///     also None.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the <see cref="Option{TA}" /> instance.</typeparam>
    /// <typeparam name="TB">The type of the value returned by the continuation function.</typeparam>
    /// <param name="self">The <see cref="Option{TA}" /> instance to map over.</param>
    /// <param name="continuation">
    ///     The function to apply to the contained value if the <see cref="Option{TA}" /> is in a Some
    ///     state.
    /// </param>
    /// <returns>
    ///     An <see cref="Option{TB}" /> instance representing the result of applying the continuation function to the
    ///     contained value.
    /// </returns>
    public static Option<TB> Map<TA, TB>(
        this Option<TA> self,
        OptionalContinuation<TA, TB> continuation
    )
    {
        return OptionRuntime.RunOption(self, continuation);
    }

    /// <summary>
    ///     Asynchronously maps the given continuation function over the <see cref="Option{TA}" /> instance.
    ///     If the <see cref="Option{TA}" /> is in a Some state, the continuation function is applied to the contained value.
    ///     If the <see cref="Option{TA}" /> is in a None state, the continuation function is not invoked and the result is
    ///     also None.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the <see cref="Option{TA}" /> instance.</typeparam>
    /// <typeparam name="TB">The type of the value returned by the continuation function.</typeparam>
    /// <param name="self">The <see cref="Option{TA}" /> instance to map over.</param>
    /// <param name="continuation">
    ///     The function to apply to the contained value if the <see cref="Option{TA}" /> is in a Some state.
    ///     This function should return a <see cref="Task{TB}" /> representing the result of the asynchronous operation.
    /// </param>
    /// <returns>
    ///     An asynchronous <see cref="Option{TB}" /> instance representing the result of applying the continuation function to
    ///     the contained value.
    ///     If the continuation function returns a successful <see cref="Task{TB}" />, the returned <see cref="Option{TB}" />
    ///     will be in a Some state with the result.
    ///     If the continuation function returns a failed <see cref="Task{TB}" />, the returned <see cref="Option{TB}" /> will
    ///     be in a None state.
    /// </returns>
    public static async Task<Option<TB>> MapAsync<TA, TB>(
        this Option<TA> self,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously maps the given continuation function over the <see cref="Option{TA}" /> instance.
    ///     If the <see cref="Option{TA}" /> is in a completed state with a Some state, the continuation function is
    ///     applied to the contained value.
    ///     If the <see cref="Option{TA}" /> is in a completed state with a None state, the continuation function is not
    ///     invoked and the result is also None.
    ///     If the <see cref="Option{TA}" /> is still in progress, the continuation function is not invoked until the
    ///     task completes.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the <see cref="Option{TA}" /> instance.</typeparam>
    /// <typeparam name="TB">The type of the value returned by the continuation function.</typeparam>
    /// <param name="self">The <see cref="Option{TA}" /> instance to map over.</param>
    /// <param name="continuation">
    ///     The function to apply to the contained value if the <see cref="Option{TA}" /> is in a completed state with a
    ///     Some state.
    ///     This function should return a <see cref="Task{TB}" /> representing the result of the asynchronous operation.
    /// </param>
    /// <returns>
    ///     An asynchronous <see cref="Option{TB}" /> instance representing the result of applying the continuation
    ///     function to the contained value.
    ///     If the continuation function returns a successful <see cref="Task{TB}" />, the returned
    ///     <see cref="Option{TB}" /> will be in a completed state with a Some state with the result.
    ///     If the continuation function returns a failed <see cref="Task{TB}" />, the returned <see cref="Option{TB}" />
    ///     will be in a completed state with a None state.
    ///     If the <see cref="Option{TA}" /> is still in progress, the returned <see cref="Option{TB}" /> will also
    ///     be in progress until the task completes.
    /// </returns>
    public static async Task<Option<TB>> MapAsync<TA, TB>(
        this Task<Option<TA>> self,
        OptionalContinuation<TA, Task<TB>> continuation
    )
    {
        return await OptionRuntime.RunOptionAsync(self, continuation).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously maps the given continuation function over the <see cref="Option{TA}" /> instance.
    ///     If the <see cref="Option{TA}" /> is in a completed state with a Some state, the continuation function is
    ///     applied to the contained value.
    ///     If the <see cref="Option{TA}" /> is in a completed state with a None state, the continuation function is not
    ///     invoked and the result is also None.
    ///     If the <see cref="Option{TA}" /> is still in progress, the continuation function is not invoked until the
    ///     task completes.
    /// </summary>
    /// <typeparam name="TA">The type of the value contained in the <see cref="Option{TA}" /> instance.</typeparam>
    /// <typeparam name="TB">The type of the value returned by the continuation function.</typeparam>
    /// <param name="self">The <see cref="Option{TA}" /> instance to map over.</param>
    /// <param name="continuation">
    ///     The function to apply to the contained value if the <see cref="Option{TA}" /> is in a completed state with a
    ///     Some state.
    ///     This function should return a <see cref="Task{TB}" /> representing the result of the asynchronous operation.
    /// </param>
    /// <returns>
    ///     An asynchronous <see cref="Option{TB}" /> instance representing the result of applying the continuation
    ///     function to the contained value.
    ///     If the continuation function returns a successful <see cref="Task{TB}" />, the returned
    ///     <see cref="Option{TB}" /> will be in a completed state with a Some state with the result.
    ///     If the continuation function returns a failed <see cref="Task{TB}" />, the returned <see cref="Option{TB}" />
    ///     will be in a completed state with a None state.
    ///     If the <see cref="Option{TA}" /> is still in progress, the returned <see cref="Option{TB}" /> will also
    ///     be in progress until the task completes.
    /// </returns>
    public static async Task<Option<TB>> Map<TA, TB>(
        this Task<Option<TA>> self,
        OptionalContinuation<TA, TB> continuation
    )
    {
        return await OptionRuntime.RunOption(self, continuation).ConfigureAwait(false);
    }
}