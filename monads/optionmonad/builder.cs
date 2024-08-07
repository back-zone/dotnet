using back.zone.monads.iomonad;
using back.zone.monads.optionmonad.subtypes;

namespace back.zone.monads.optionmonad;

public static class option
{
    /// <summary>
    ///     Creates an Option instance from a function that returns a value.
    /// </summary>
    /// <typeparam name="A">The type of the value to be wrapped in the Option.</typeparam>
    /// <param name="builder">A function that returns a value of type A.</param>
    /// <returns>An Option instance containing the result of the builder function, or None if the function throws an exception.</returns>
    public static Option<A> of<A>(
        Func<A> builder
    )
        where A : notnull
    {
        return io.of(builder).toOption();
    }

    /// <summary>
    ///     Creates an Option instance from an asynchronous function that returns a value.
    /// </summary>
    /// <typeparam name="A">The type of the value to be wrapped in the Option.</typeparam>
    /// <param name="builder">An asynchronous function that returns a value of type A.</param>
    /// <returns>
    ///     An Option instance containing the result of the builder function, or None if the function throws an exception.
    ///     The result is wrapped in a Task to support asynchronous operations.
    /// </returns>
    public static async Task<Option<A>> ofAsync<A>(
        Task<A> builder
    )
        where A : notnull
    {
        return await io.ofAsync(builder).toOptionAsync();
    }

    public static async Task<Option<A>> ofAsync<A>(
        Task<Option<A>> builder
    )
        where A : notnull
    {
        try
        {
            var product = await builder;
            return product;
        }
        catch (Exception _)
        {
            return none<A>();
        }
    }

    /// <summary>
    ///     Creates an Option instance containing a specified value.
    /// </summary>
    /// <typeparam name="A">The type of the value to be wrapped in the Option.</typeparam>
    /// <param name="a">The value to be wrapped in the Option.</param>
    /// <returns>An Option instance containing the specified value.</returns>
    public static Option<A> some<A>(
        A a
    )
        where A : notnull
    {
        return new Some<A>(a);
    }

    /// <summary>
    ///     Creates an Option instance representing no value.
    /// </summary>
    /// <typeparam name="A">The type of the value to be wrapped in the Option.</typeparam>
    /// <returns>An Option instance representing no value, i.e., None.</returns>
    public static Option<A> none<A>() where A : notnull
    {
        return new None<A>();
    }
}