using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherZip
{
    /// <summary>
    ///     Applies a zipper function to the right values of two Either instances, if both instances are in the Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">The first Either instance.</param>
    /// <param name="other">The second Either instance.</param>
    /// <param name="zipper">The zipper function to apply to the Right values.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new Either instance in the Right state with the result of the
    ///     zipper function.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If either instance is in the Left state and does not contain a Left value.
    /// </exception>
    public static Either<TL, TR2> ZipWith<TL, TR, TR1, TR2>(
        this Either<TL, TR> self,
        Either<TL, TR1> other,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? other.TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : other.TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Applies a zipper function to the right values of two Either instances asynchronously, if both instances are in the
    ///     Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">The first Either instance.</param>
    /// <param name="otherAsync">A task representing the second Either instance.</param>
    /// <param name="zipper">The zipper function to apply to the Right values.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new Either instance in the Right state with the result of the
    ///     zipper function.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If either instance is in the Left state and does not contain a Left value.
    /// </exception>
    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Either<TL, TR> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Applies a zipper function to the right values of two Either instances asynchronously, if both instances are in the
    ///     Right state. If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">The first Either instance.</param>
    /// <param name="other">The second Either instance.</param>
    /// <param name="zipperAsync">The asynchronous zipper function to apply to the Right values.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new Either instance in the Right state with the result of the
    ///     zipper function. If either instance is in the Left state, returns the Left value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If either instance is in the Left state and does not contain a Left value.
    /// </exception>
    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Either<TL, TR> self,
        Either<TL, TR1> other,
        Zipper<TR, TR1, Task<TR2>> zipperAsync
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? other.TryGetRight(out var otherRight)
                    ? await zipperAsync(right, otherRight).ConfigureAwait(false)
                    : other.TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Applies a zipper function to the right values of two Either instances asynchronously, if both instances are in the
    ///     Right state. If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">The first Either instance.</param>
    /// <param name="otherAsync">A task representing the second Either instance.</param>
    /// <param name="zipperAsync">The asynchronous zipper function to apply to the Right values.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new Either instance in the Right state with the result of the
    ///     zipper function. If either instance is in the Left state, returns the Left value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If either instance is in the Left state and does not contain a Left value.
    /// </exception>
    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Either<TL, TR> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, Task<TR2>> zipperAsync
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return self.TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? await zipperAsync(right, otherRight).ConfigureAwait(false)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : self.TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return self.TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Applies a zipper function to the right values of two Either instances asynchronously, if both instances are in the
    ///     Right state. If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">A task representing the first Either instance.</param>
    /// <param name="other">The second Either instance.</param>
    /// <param name="zipper">The zipper function to apply to the Right values.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new Either instance in the Right state with the result of the
    ///     zipper function. If either instance is in the Left state, returns the Left value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If either instance is in the Left state and does not contain a Left value.
    /// </exception>
    public static async Task<Either<TL, TR2>> ZipWith<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR1> other,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? other.TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : other.TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Applies a zipper function to the right values of two Either instances asynchronously, if both instances are in the
    ///     Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">A task representing the first Either instance.</param>
    /// <param name="otherAsync">A task representing the second Either instance.</param>
    /// <param name="zipper">The zipper function to apply to the Right values.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new Either instance in the Right state with the result of the
    ///     zipper function.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If either instance is in the Left state and does not contain a Left value.
    /// </exception>
    public static async Task<Either<TL, TR2>> ZipWith<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Applies a zipper function to the right values of two Either instances asynchronously, if both instances are in the
    ///     Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">A task representing the first Either instance.</param>
    /// <param name="other">The second Either instance.</param>
    /// <param name="zipperAsync">The asynchronous zipper function to apply to the Right values.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new Either instance in the Right state with the result of the
    ///     zipper function.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If either instance is in the Left state and does not contain a Left value.
    /// </exception>
    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR1> other,
        Zipper<TR, TR1, Task<TR2>> zipperAsync
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? other.TryGetRight(out var otherRight)
                    ? await zipperAsync(right, otherRight).ConfigureAwait(false)
                    : other.TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously applies a zipper function to the right values of two <see cref="Either{TL, TR}" /> instances,
    ///     if both instances are in the Right state. If either instance is in the Left state, the function returns the Left
    ///     value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">A task representing the first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="otherAsync">A task representing the second <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="zipperAsync">The asynchronous zipper function to apply to the Right values.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, TR2}" /> instance in the Right state
    ///     with the result of the zipper function.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If either instance is in the Left state and does not contain a Left value.
    /// </exception>
    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, Task<TR2>> zipperAsync
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? await zipperAsync(right, otherRight).ConfigureAwait(false)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously applies a zipper function to the right values of two <see cref="Either{TL, TR}" /> instances,
    ///     if both instances are in the Right state. If either instance is in the Left state, the function returns the Left
    ///     value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <typeparam name="TR2">The type of the result of the zipper function.</typeparam>
    /// <param name="self">A task representing the first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="otherAsync">A task representing the second <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="zipper">The zipper function to apply to the Right values.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, TR2}" /> instance in the Right state
    ///     with the result of the zipper function.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If either instance is in the Left state and does not contain a Left value.
    /// </exception>
    public static async Task<Either<TL, TR2>> ZipWithAsync<TL, TR, TR1, TR2>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> otherAsync,
        Zipper<TR, TR1, TR2> zipper
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
        where TR2 : notnull
    {
        try
        {
            return (await self.ConfigureAwait(false)).TryGetRight(out var right)
                ? (await otherAsync.ConfigureAwait(false)).TryGetRight(out var otherRight)
                    ? zipper(right, otherRight)
                    : (await otherAsync).TryGetLeft(out var otherLeft)
                        ? otherLeft
                        : throw new InvalidOperationException("#no_right_or_left_exception#")
                : (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                    ? left
                    : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
        catch (Exception)
        {
            return (await self.ConfigureAwait(false)).TryGetLeft(out var left)
                ? left
                : throw new InvalidOperationException("#no_left_or_right_exception#");
        }
    }

    /// <summary>
    ///     Asynchronously combines the right values of two <see cref="Either{TL, TR}" /> instances, if both instances are in
    ///     the Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <param name="self">A task representing the first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="other">The second <see cref="Either{TL, TR}" /> instance.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, TR1}" /> instance in the Right state
    ///     with the second Right value.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    public static Either<TL, TR1> ZipRight<TL, TR, TR1>(
        this Either<TL, TR> self,
        Either<TL, TR1> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return self.ZipWith(other, (_, b) => b);
    }

    /// <summary>
    ///     Asynchronously combines the right values of two <see cref="Either{TL, TR}" /> instances, if both instances are in
    ///     the Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <param name="self">A task representing the first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="other">A task representing the second <see cref="Either{TL, TR}" /> instance.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, TR1}" /> instance in the Right state
    ///     with the second Right value.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    public static async Task<Either<TL, TR1>> ZipRightAsync<TL, TR, TR1>(
        this Either<TL, TR> self,
        Task<Either<TL, TR1>> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await self.ZipWithAsync(other, (_, b) => b).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously combines the right values of two <see cref="Either{TL, TR}" /> instances, if both instances are in
    ///     the Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <param name="self">A task representing the first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="other">A task representing the second <see cref="Either{TL, TR}" /> instance.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, TR1}" /> instance in the Right state
    ///     with the second Right value.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    public static async Task<Either<TL, TR1>> ZipRightAsync<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await self.ZipWithAsync(other, (_, b) => b).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously combines the right values of two <see cref="Either{TL, TR}" /> instances, if both instances are in
    ///     the Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <param name="self">A task representing the first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="other">The second <see cref="Either{TL, TR}" /> instance.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, TR1}" /> instance in the Right state
    ///     with the second Right value.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    public static async Task<Either<TL, TR1>> ZipRight<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR1> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return (await self.ConfigureAwait(false)).ZipWith(other, (_, b) => b);
    }

    /// <summary>
    ///     Combines the right values of two <see cref="Either{TL, TR}" /> instances, if both instances are in the Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <param name="self">The first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="other">The second <see cref="Either{TL, TR}" /> instance.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, (TR, TR1)}" /> instance in the Right
    ///     state
    ///     with a tuple containing the second Right values.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    public static Either<TL, (TR, TR1)> Zip<TL, TR, TR1>(
        this Either<TL, TR> self,
        Either<TL, TR1> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return self.ZipWith(other, (a, b) => (a, b));
    }

    /// <summary>
    ///     Asynchronously combines the right values of two <see cref="Either{TL, TR}" /> instances, if both instances are in
    ///     the Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <param name="self">The first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="other">A task representing the second <see cref="Either{TL, TR}" /> instance.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, (TR, TR1)}" /> instance in the Right
    ///     state
    ///     with a tuple containing the second Right values.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    public static async Task<Either<TL, (TR, TR1)>> ZipAsync<TL, TR, TR1>(
        this Either<TL, TR> self,
        Task<Either<TL, TR1>> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await self.ZipWithAsync(other, (a, b) => (a, b)).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously combines the right values of two <see cref="Either{TL, TR}" /> instances, if both instances are in
    ///     the Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <param name="self">A task representing the first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="other">A task representing the second <see cref="Either{TL, TR}" /> instance.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, (TR, TR1)}" /> instance in the Right
    ///     state
    ///     with a tuple containing the second Right values.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    public static async Task<Either<TL, (TR, TR1)>> ZipAsync<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Task<Either<TL, TR1>> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await self.ZipWithAsync(other, (a, b) => (a, b)).ConfigureAwait(false);
    }

    /// <summary>
    ///     Asynchronously combines the right values of two <see cref="Either{TL, TR}" /> instances, if both instances are in
    ///     the Right state.
    ///     If either instance is in the Left state, the function returns the Left value.
    /// </summary>
    /// <typeparam name="TL">The type of the Left value.</typeparam>
    /// <typeparam name="TR">The type of the first Right value.</typeparam>
    /// <typeparam name="TR1">The type of the second Right value.</typeparam>
    /// <param name="self">A task representing the first <see cref="Either{TL, TR}" /> instance.</param>
    /// <param name="other">The second <see cref="Either{TL, TR}" /> instance.</param>
    /// <returns>
    ///     If both instances are in the Right state, returns a new <see cref="Either{TL, (TR, TR1)}" /> instance in the Right
    ///     state
    ///     with a tuple containing the second Right values.
    ///     If either instance is in the Left state, returns the Left value.
    /// </returns>
    public static async Task<Either<TL, (TR, TR1)>> Zip<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Either<TL, TR1> other
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return (await self.ConfigureAwait(false)).ZipWith(other, (a, b) => (a, b));
    }
}