using back.zone.core.Types;

namespace back.zone.core.Monads.TryMonad;

public static class TryZip
{
    /// <summary>
    ///     Applies a zipper function to the values of two Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <typeparam name="TC">The type of the result Try instance.</typeparam>
    /// <param name="self">The first Try instance.</param>
    /// <param name="other">The second Try instance.</param>
    /// <param name="zipper">A function that combines the values of the two Try instances into a new value.</param>
    /// <returns>
    ///     A Try instance containing the result of applying the zipper function to the values of the two Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static Try<TC> ZipWith<TA, TB, TC>(
        this Try<TA> self,
        Try<TB> other,
        Zipper<TA, TB, TC> zipper)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? other.TryGetValue(out var otherValue)
                    ? zipper(value, otherValue)
                    : other.TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances asynchronously.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <typeparam name="TC">The type of the result Try instance.</typeparam>
    /// <param name="self">The first Try instance.</param>
    /// <param name="otherAsync">A task representing the second Try instance.</param>
    /// <param name="zipper">A function that combines the values of the two Try instances into a new value.</param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Try<TA> self,
        Task<Try<TB>> otherAsync,
        Zipper<TA, TB, TC> zipper)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var otherValue)
                    ? zipper(value, otherValue)
                    : (await otherAsync.ConfigureAwait(false)).TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances asynchronously.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <typeparam name="TC">The type of the result Try instance.</typeparam>
    /// <param name="self">The first Try instance.</param>
    /// <param name="other">The second Try instance.</param>
    /// <param name="zipperAsync">
    ///     A function that combines the values of the two Try instances into a new value, returning a
    ///     task.
    /// </param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Try<TA> self,
        Try<TB> other,
        Zipper<TA, TB, Task<TC>> zipperAsync)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? other.TryGetValue(out var otherValue)
                    ? await zipperAsync(value, otherValue).ConfigureAwait(false)
                    : other.TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances asynchronously.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <typeparam name="TC">The type of the result Try instance.</typeparam>
    /// <param name="self">The first Try instance.</param>
    /// <param name="otherAsync">A task representing the second Try instance.</param>
    /// <param name="zipperAsync">
    ///     A function that combines the values of the two Try instances into a new value, returning a
    ///     task.
    /// </param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Try<TA> self,
        Task<Try<TB>> otherAsync,
        Zipper<TA, TB, Task<TC>> zipperAsync)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return self.TryGetValue(out var value)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var otherValue)
                    ? await zipperAsync(value, otherValue).ConfigureAwait(false)
                    : (await otherAsync.ConfigureAwait(false)).TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : self.TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances asynchronously, where the first Try instance is
    ///     obtained from a task.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <typeparam name="TC">The type of the result Try instance.</typeparam>
    /// <param name="self">A task representing the first Try instance.</param>
    /// <param name="other">The second Try instance.</param>
    /// <param name="zipper">A function that combines the values of the two Try instances into a new value.</param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TC>> ZipWith<TA, TB, TC>(
        this Task<Try<TA>> self,
        Try<TB> other,
        Zipper<TA, TB, TC> zipper)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? other.TryGetValue(out var otherValue)
                    ? zipper(value, otherValue)
                    : other.TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : (await self.ConfigureAwait(false)).TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances asynchronously, where both Try instances are obtained
    ///     from tasks.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <typeparam name="TC">The type of the result Try instance.</typeparam>
    /// <param name="self">A task representing the first Try instance.</param>
    /// <param name="otherAsync">A task representing the second Try instance.</param>
    /// <param name="zipper">A function that combines the values of the two Try instances into a new value.</param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Try<TA>> self,
        Task<Try<TB>> otherAsync,
        Zipper<TA, TB, TC> zipper)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var otherValue)
                    ? zipper(value, otherValue)
                    : (await otherAsync.ConfigureAwait(false)).TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : (await self.ConfigureAwait(false)).TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances asynchronously, where the first Try instance is
    ///     obtained from a task.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <typeparam name="TC">The type of the result Try instance.</typeparam>
    /// <param name="self">A task representing the first Try instance.</param>
    /// <param name="other">The second Try instance.</param>
    /// <param name="zipperAsync">
    ///     A function that combines the values of the two Try instances into a new value, returning a task.
    /// </param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Try<TA>> self,
        Try<TB> other,
        Zipper<TA, TB, Task<TC>> zipperAsync)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? other.TryGetValue(out var otherValue)
                    ? await zipperAsync(value, otherValue).ConfigureAwait(false)
                    : other.TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : (await self.ConfigureAwait(false)).TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances asynchronously, where both Try instances are obtained
    ///     from tasks.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <typeparam name="TC">The type of the result Try instance.</typeparam>
    /// <param name="self">A task representing the first Try instance.</param>
    /// <param name="otherAsync">A task representing the second Try instance.</param>
    /// <param name="zipperAsync">
    ///     A function that combines the values of the two Try instances into a new value, returning a task.
    /// </param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TC>> ZipWithAsync<TA, TB, TC>(
        this Task<Try<TA>> self,
        Task<Try<TB>> otherAsync,
        Zipper<TA, TB, Task<TC>> zipperAsync)
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetValue(out var value)
                ? (await otherAsync.ConfigureAwait(false)).TryGetValue(out var otherValue)
                    ? await zipperAsync(value, otherValue).ConfigureAwait(false)
                    : (await otherAsync.ConfigureAwait(false)).TryGetException(out var otherException)
                        ? otherException
                        : new InvalidOperationException("#no_value_or_exception_in_zip#")
                : (await self.ConfigureAwait(false)).TryGetException(out var exception)
                    ? exception
                    : new InvalidOperationException("#no_value_or_exception_in_fold#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances, where the second Try instance is returned. ///     If
    ///     either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <param name="self">The first Try instance.</param>
    /// <param name="other">The second Try instance.</param>
    /// <returns>
    ///     A Try instance containing the result of applying the zipper function to the values of the two Try instances,
    ///     where the second Try instance is returned.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static Try<TB> ZipRight<TA, TB>(
        this Try<TA> self,
        Try<TB> other
    )
        where TA : notnull
        where TB : notnull
    {
        return self.ZipWith(other, (_, b) => b);
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances, where the second Try instance is returned.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <param name="self">The first Try instance.</param>
    /// <param name="otherAsync">A task representing the second Try instance.</param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances, where the second Try instance is returned.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TB>> ZipRightAsync<TA, TB>(
        this Try<TA> self,
        Task<Try<TB>> otherAsync
    )
        where TA : notnull
        where TB : notnull
    {
        return await self.ZipWithAsync(otherAsync, (_, b) => b).ConfigureAwait(false);
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances, where the second Try instance is returned.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <param name="selfAsync">A task representing the first Try instance.</param>
    /// <param name="otherAsync">A task representing the second Try instance.</param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances,
    ///     where the second Try instance is returned.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TB>> ZipRightAsync<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Task<Try<TB>> otherAsync
    )
        where TA : notnull
        where TB : notnull
    {
        return await selfAsync.ZipWithAsync(otherAsync, (_, b) => b).ConfigureAwait(false);
    }

    /// <summary>
    ///     Applies a zipper function to the values of two Try instances, where the second Try instance is returned.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <param name="selfAsync">A task representing the first Try instance.</param>
    /// <param name="other">The second Try instance.</param>
    /// <returns>
    ///     A task representing a Try instance containing the result of applying the zipper function to the values of the two
    ///     Try instances, where the second Try instance is returned.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<TB>> ZipRight<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Try<TB> other
    )
        where TA : notnull
        where TB : notnull
    {
        return await selfAsync.ZipWith(other, (_, b) => b).ConfigureAwait(false);
    }

    /// <summary>
    ///     Combines the values of two Try instances into a tuple.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first Try instance.</typeparam>
    /// <typeparam name="TB">The type of the second Try instance.</typeparam>
    /// <param name="self">The first Try instance.</param>
    /// <param name="other">The second Try instance.</param>
    /// <returns>
    ///     A Try instance containing a tuple of the values from the two Try instances.
    ///     If either Try instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static Try<(TA, TB)> Zip<TA, TB>(
        this Try<TA> self,
        Try<TB> other
    )
        where TA : notnull
        where TB : notnull
    {
        return self.ZipWith(other, (a, b) => (a, b));
    }

    /// <summary>
    ///     Asynchronously combines the values of two <see cref="Try{T}" /> instances into a tuple.
    ///     If either <see cref="Try{T}" /> instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first <see cref="Try{T}" /> instance.</typeparam>
    /// <typeparam name="TB">The type of the second <see cref="Try{T}" /> instance.</typeparam>
    /// <param name="self">The first <see cref="Try{T}" /> instance.</param>
    /// <param name="otherAsync">A task representing the second <see cref="Try{T}" /> instance.</param>
    /// <returns>
    ///     A task representing a <see cref="Try{T}" /> instance containing a tuple of the values from the two
    ///     <see cref="Try{T}" /> instances.
    ///     If either <see cref="Try{T}" /> instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<(TA, TB)>> ZipAsync<TA, TB>(
        this Try<TA> self,
        Task<Try<TB>> otherAsync
    )
        where TA : notnull
        where TB : notnull
    {
        return await self.ZipWithAsync(otherAsync, (a, b) => (a, b)).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously combines the values of two <see cref="Try{T}" /> instances into a tuple.
    ///     If either <see cref="Try{T}" /> instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first <see cref="Try{T}" /> instance.</typeparam>
    /// <typeparam name="TB">The type of the second <see cref="Try{T}" /> instance.</typeparam>
    /// <param name="selfAsync">A task representing the first <see cref="Try{T}" /> instance.</param>
    /// <param name="otherAsync">A task representing the second <see cref="Try{T}" /> instance.</param>
    /// <returns>
    ///     A task representing a <see cref="Try{T}" /> instance containing a tuple of the values from the two
    ///     <see cref="Try{T}" /> instances.
    ///     If either <see cref="Try{T}" /> instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<(TA, TB)>> ZipAsync<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Task<Try<TB>> otherAsync
    )
        where TA : notnull
        where TB : notnull
    {
        return await selfAsync.ZipWithAsync(otherAsync, (a, b) => (a, b)).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously combines the values of two <see cref="Try{T}" /> instances into a tuple.
    ///     If either <see cref="Try{T}" /> instance is in a Failure state, the result will also be in a Failure state.
    /// </summary>
    /// <typeparam name="TA">The type of the first <see cref="Try{T}" /> instance.</typeparam>
    /// <typeparam name="TB">The type of the second <see cref="Try{T}" /> instance.</typeparam>
    /// <param name="selfAsync">A task representing the first <see cref="Try{T}" /> instance.</param>
    /// <param name="other">The second <see cref="Try{T}" /> instance.</param>
    /// <returns>
    ///     A task representing a <see cref="Try{T}" /> instance containing a tuple of the values from the two
    ///     <see cref="Try{T}" /> instances.
    ///     If either <see cref="Try{T}" /> instance is in a Failure state, the result will also be in a Failure state.
    /// </returns>
    public static async Task<Try<(TA, TB)>> Zip<TA, TB>(
        this Task<Try<TA>> selfAsync,
        Try<TB> other
    )
        where TA : notnull
        where TB : notnull
    {
        return await selfAsync.ZipWith(other, (a, b) => (a, b)).ConfigureAwait(false);
    }
}