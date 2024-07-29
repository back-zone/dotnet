using monads.iomonad;

namespace monads.optionmonad;

public static class conversions
{
    public static IO<A> asIO<A>(this Option<A> option)
    {
        return option.fold(
            io.fail<A>("Option is None"),
            io.succeed
        );
    }

    public static async Task<IO<A>> asIOAsync<A>(this Task<Option<A>> optionTask)
    {
        return await optionTask.foldAsync(
            io.fail<A>("Option is None"),
            async a => await Task.FromResult(io.succeed(a))
        );
    }
}