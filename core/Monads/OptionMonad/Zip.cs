using back.zone.core.Types;

namespace back.zone.core.Monads.OptionMonad;

public static class OptionZip
{
    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" />.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <typeparam name="TC">The type of the result.</typeparam>
    /// <param name="self">The first option.</param>
    /// <param name="other">The second option.</param>
    /// <param name="zipper">A function that takes the values from the two options and returns a new value.</param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipper" /> function to the values
    ///     inside the two options,
    ///     or <see cref="Option.None{T}" /> if either of the input options is <see cref="Option.None{T}" />.
    /// </returns>
    public static Option<TC> ZipWith<TA, TB, TC>(
        this Option<TA> self,
        Option<TB> other,
        OptionalZipper<TA, TB, TC> zipper
    )
    {
        try
        {
            return self.TryGetValue(out var a)
                ? other.TryGetValue(out var b)
                    ? zipper(a, b)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" />.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />.
    ///     This method is asynchronous and waits for the second option to complete before proceeding.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <typeparam name="TC">The type of the result.</typeparam>
    /// <param name="self">The first option.</param>
    /// <param name="otherAsync">A task representing the second option.</param>
    /// <param name="zipper">A function that takes the values from the two options and returns a new value.</param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipper" /> function to the values
    ///     inside the two options, or <see cref="Option.None{T}" /> if either of the input options is
    ///     <see cref="Option.None{T}" />.
    /// </returns>
    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Option<TA> self,
        Task<Option<TB>> otherAsync,
        OptionalZipper<TA, TB, TC> zipper
    )
    {
        try
        {
            return self.TryGetValue(out var a)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var b)
                    ? zipper(a, b)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" />.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />.
    ///     This method is asynchronous and waits for the second option to complete before proceeding.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <typeparam name="TC">The type of the result.</typeparam>
    /// <param name="self">The first option.</param>
    /// <param name="other">The second option.</param>
    /// <param name="zipperAsync">
    ///     A function that takes the values from the two options and returns a task representing a new
    ///     value.
    /// </param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipperAsync" /> function to the
    ///     values inside the two options,
    ///     or <see cref="Option.None{T}" /> if either of the input options is <see cref="Option.None{T}" />.
    /// </returns>
    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Option<TA> self,
        Option<TB> other,
        OptionalZipper<TA, TB, Task<TC>> zipperAsync
    )
    {
        try
        {
            return self.TryGetValue(out var a)
                ? other.TryGetValue(out var b)
                    ? await zipperAsync(a, b).ConfigureAwait(false)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" />.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />.
    ///     This method is asynchronous and waits for the second option to complete before proceeding.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <typeparam name="TC">The type of the result.</typeparam>
    /// <param name="self">The first option.</param>
    /// <param name="otherAsync">A task representing the second option.</param>
    /// <param name="zipperAsync">
    ///     A function that takes the values from the two options and returns a task representing a new value.
    /// </param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipperAsync" /> function to the
    ///     values
    ///     inside the two options, or <see cref="Option.None{T}" /> if either of the input options is
    ///     <see cref="Option.None{T}" />.
    /// </returns>
    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Option<TA> self,
        Task<Option<TB>> otherAsync,
        OptionalZipper<TA, TB, Task<TC>> zipperAsync
    )
    {
        try
        {
            return self.TryGetValue(out var a)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var b)
                    ? await zipperAsync(a, b).ConfigureAwait(false)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" />.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />.
    ///     This method is asynchronous and waits for the second option to complete before proceeding.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <typeparam name="TC">The type of the result.</typeparam>
    /// <param name="self">A task representing the first option.</param>
    /// <param name="other">The second option.</param>
    /// <param name="zipper">A function that takes the values from the two options and returns a new value.</param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipper" /> function to the values
    ///     inside the two options, or <see cref="Option.None{T}" /> if either of the input options is
    ///     <see cref="Option.None{T}" />.
    /// </returns>
    public static async Task<Option<TC>> ZipWith<TA, TB, TC>(
        this Task<Option<TA>> self,
        Option<TB> other,
        OptionalZipper<TA, TB, TC> zipper
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var a)
                ? other.TryGetValue(out var b)
                    ? zipper(a, b)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" />.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />.
    ///     This method is asynchronous and waits for the second option to complete before proceeding.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <typeparam name="TC">The type of the result.</typeparam>
    /// <param name="self">A task representing the first option.</param>
    /// <param name="otherAsync">A task representing the second option.</param>
    /// <param name="zipper">A function that takes the values from the two options and returns a new value.</param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipper" /> function to the values
    ///     inside the two options,
    ///     or <see cref="Option.None{T}" /> if either of the input options is <see cref="Option.None{T}" />.
    /// </returns>
    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Option<TA>> self,
        Task<Option<TB>> otherAsync,
        OptionalZipper<TA, TB, TC> zipper
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var a)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var b)
                    ? zipper(a, b)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" />.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />.
    ///     This method is asynchronous and waits for the second option to complete before proceeding.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <typeparam name="TC">The type of the result.</typeparam>
    /// <param name="self">A task representing the first option.</param>
    /// <param name="other">The second option.</param>
    /// <param name="zipperAsync">
    ///     A function that takes the values from the two options and returns a task representing a new value.
    /// </param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipperAsync" /> function to the
    ///     values inside the two options, or <see cref="Option.None{T}" /> if either of the input options is
    ///     <see cref="Option.None{T}" />.
    /// </returns>
    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Option<TA>> self,
        Option<TB> other,
        OptionalZipper<TA, TB, Task<TC>> zipperAsync
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var a)
                ? other.TryGetValue(out var b)
                    ? await zipperAsync(a, b).ConfigureAwait(false)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" /> and waits for both options to complete before proceeding.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />.
    ///     This method is asynchronous.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <typeparam name="TC">The type of the result.</typeparam>
    /// <param name="self">A task representing the first option.</param>
    /// <param name="otherAsync">A task representing the second option.</param>
    /// <param name="zipperAsync">
    ///     A function that takes the values from the two options and returns a task representing a new value.
    /// </param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipperAsync" /> function to the
    ///     values inside the two options, or <see cref="Option.None{T}" /> if either of the input options is
    ///     <see cref="Option.None{T}" />, or if an exception is thrown during the execution of the function.
    /// </returns>
    public static async Task<Option<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Option<TA>> self,
        Task<Option<TB>> otherAsync,
        OptionalZipper<TA, TB, Task<TC>> zipperAsync
    )
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var a)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var b)
                    ? await zipperAsync(a, b).ConfigureAwait(false)
                    : Option.None<TC>()
                : Option.None<TC>();
        }
        catch (Exception)
        {
            return Option.None<TC>();
        }
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" /> and returns the second option's value.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <param name="self">The first option.</param>
    /// <param name="other">The second option.</param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the second option's value if both instances are not
    ///     <see cref="Option.None{T}" />, or <see cref="Option.None{T}" /> if either of the input options is
    ///     <see cref="Option.None{T}" />
    /// </returns>
    public static Option<TB> ZipRight<TA, TB>(
        this Option<TA> self,
        Option<TB> other
    )
    {
        return self.ZipWith(other, (_, b) => b);
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" /> and waits for both options to complete before proceeding.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />, and the
    ///     function's result is returned.
    ///     This method is asynchronous.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <param name="self">A task representing the first option.</param>
    /// <param name="otherAsync">A task representing the second option.</param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipperAsync" /> function to the
    ///     values inside the two options,
    ///     or <see cref="Option.None{T}" /> if either of the input options is <see cref="Option.None{T}" />, or if an
    ///     exception is thrown during the execution of the function.
    /// </returns>
    public static async Task<Option<TB>> ZipRightAsync<TA, TB>(
        this Option<TA> self,
        Task<Option<TB>> otherAsync
    )
    {
        return await self.ZipWithAsync(otherAsync, (_, b) => b).ConfigureAwait(false);
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" /> and waits for both options to complete before proceeding.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />, and the
    ///     function's result is returned.
    ///     This method is asynchronous.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <param name="selfAsync">A task representing the first option.</param>
    /// <param name="otherAsync">A task representing the second option.</param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipperAsync" /> function to the
    ///     values inside the two options, or <see cref="Option.None{T}" /> if either of the input options is
    ///     <see cref="Option.None{T}" />, or if an exception is thrown during the execution of the function.
    /// </returns>
    public static async Task<Option<TB>> ZipRightAsync<TA, TB>(
        this Task<Option<TA>> selfAsync,
        Task<Option<TB>> otherAsync
    )
    {
        return await selfAsync.ZipWithAsync(otherAsync, (_, b) => b).ConfigureAwait(false);
    }

    /// <summary>
    ///     Applies a function to the values inside two <see cref="Option{T}" /> instances, if both instances are not
    ///     <see cref="Option.None{T}" /> and waits for both options to complete before proceeding.
    ///     If either instance is <see cref="Option.None{T}" />, the result is also <see cref="Option.None{T}" />, and the
    ///     function's result is returned.
    ///     This method is asynchronous.
    /// </summary>
    /// <typeparam name="TA">The type of the first option's value.</typeparam>
    /// <typeparam name="TB">The type of the second option's value.</typeparam>
    /// <param name="selfAsync">A task representing the first option.</param>
    /// <param name="other">The second option.</param>
    /// <returns>
    ///     An <see cref="Option{T}" /> containing the result of applying the <paramref name="zipperAsync" /> function to the
    ///     values inside the two options, or <see cref="Option.None{T}" /> if either of the input options is
    ///     <see cref="Option.None{T}" />, or if an exception is thrown during the execution of the function.
    /// </returns>
    public static async Task<Option<TB>> ZipRightAsync<TA, TB>(
        this Task<Option<TA>> selfAsync,
        Option<TB> other
    )
    {
        return await selfAsync.ZipWith(other, (_, b) => b).ConfigureAwait(false);
    }

    /// <summary>
    ///     Combines two Option instances into a single Option of a tuple, containing the values from the original Options.
    ///     If either of the original Options is None, the result will also be None.
    /// </summary>
    /// <typeparam name="TA">The type of the first Option's value.</typeparam>
    /// <typeparam name="TB">The type of the second Option's value.</typeparam>
    /// <param name="self">The first Option instance.</param>
    /// <param name="other">The second Option instance.</param>
    /// <returns>
    ///     An Option of a tuple, containing the values from the original Options if both were not None.
    ///     Otherwise, returns None.
    /// </returns>
    public static Option<(TA, TB)> Zip<TA, TB>(
        this Option<TA> self,
        Option<TB> other
    )
    {
        return self.ZipWith(other, (a, b) => (a, b));
    }

    /// <summary>
    ///     Combines two Option instances into a single Option of a tuple, containing the values from the original Options.
    ///     If either of the original Options is None, the result will also be None. This method is asynchronous.
    /// </summary>
    /// <typeparam name="TA">The type of the first Option's value.</typeparam>
    /// <typeparam name="TB">The type of the second Option's value.</typeparam>
    /// <param name="self">The first Option instance.</param>
    /// <param name="otherAsync">A task representing the second Option instance.</param>
    /// <returns>
    ///     An Option of a tuple, containing the values from the original Options if both were not None.
    ///     Otherwise, returns None.
    /// </returns>
    public static async Task<Option<(TA, TB)>> ZipAsync<TA, TB>(
        this Option<TA> self,
        Task<Option<TB>> otherAsync
    )
    {
        return await self.ZipWithAsync(otherAsync, (a, b) => (a, b)).ConfigureAwait(false);
    }

    /// <summary>
    ///     Combines two Option instances into a single Option of a tuple, containing the values from the original Options.
    ///     If either of the original Options is None, the result will also be None. This method is asynchronous.
    /// </summary>
    /// <typeparam name="TA">The type of the first Option's value.</typeparam>
    /// <typeparam name="TB">The type of the second Option's value.</typeparam>
    /// <param name="selfAsync">A task representing the first Option instance.</param>
    /// <param name="otherAsync">A task representing the second Option instance.</param>
    /// <returns>
    ///     An Option of a tuple, containing the values from the original Options if both were not None.
    ///     Otherwise, returns None.
    /// </returns>
    public static async Task<Option<(TA, TB)>> ZipAsync<TA, TB>(
        this Task<Option<TA>> selfAsync,
        Task<Option<TB>> otherAsync
    )
    {
        return await selfAsync.ZipWithAsync(otherAsync, (a, b) => (a, b)).ConfigureAwait(false);
    }

    /// <summary>
    ///     Combines two Option instances into a single Option of a tuple, containing the values from the original Options.
    ///     If either of the original Options is None, the result will also be None. This method is asynchronous.
    /// </summary>
    /// <typeparam name="TA">The type of the first Option's value.</typeparam>
    /// <typeparam name="TB">The type of the second Option's value.</typeparam>
    /// <param name="selfAsync">A task representing the first Option instance.</param>
    /// <param name="other">The second Option instance.</param>
    /// <returns>
    ///     An Option of a tuple, containing the values from the original Options if both were not None.
    ///     Otherwise, returns None.
    /// </returns>
    public static async Task<Option<(TA, TB)>> Zip<TA, TB>(
        this Task<Option<TA>> selfAsync,
        Option<TB> other
    )
    {
        return await selfAsync.ZipWith(other, (a, b) => (a, b)).ConfigureAwait(false);
    }
}