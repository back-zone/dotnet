using monads.iomonad;

namespace monads.result;

public static class ioextension
{
    public static async Task<IO<B>> mapAsync<A, B>(
        this Task<IO<A>> ioTask,
        Func<A, Task<B>> asyncMapper)
    {
        var currentTask = await ioTask;
        return await currentTask.mapAsync(asyncMapper);
    }
}