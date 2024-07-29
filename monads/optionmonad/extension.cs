namespace monads.optionmonad;

public static class extension
{
    public static async Task<Option<B>> flatMapAsync<A, B>(
        this Task<Option<A>> optionTask,
        Func<A, Task<Option<B>>> asyncFlatMapper)
    {
        var currentTask = await optionTask;

        return await currentTask.flatMapAsync(asyncFlatMapper);
    }

    public static async Task<Option<B>> mapAsync<A, B>(
        this Task<Option<A>> optionTask,
        Func<A, Task<B>> asyncMapper)
    {
        var currentTask = await optionTask;
        return await currentTask.mapAsync(asyncMapper);
    }

    public static async Task<Option<B>> asThisAsync<A, B>(
        this Task<Option<A>> optionTask,
        Task<B> b)
    {
        var currentTask = await optionTask;
        return await currentTask.asThisAsync(b);
    }

    public static async Task<Option<C>> zipWithAsync<A, B, C>(
        this Task<Option<A>> optionTask,
        Option<B> other, Func<A, B, Task<C>> asyncZipper)
    {
        var currentTask = await optionTask;
        return await currentTask.zipWithAsync(other, asyncZipper);
    }

    public static async Task<Option<B>> zipRightAsync<A, B>(
        this Task<Option<A>> optionTask,
        Option<B> other)
    {
        var currentTask = await optionTask;
        return await currentTask.zipRightAsync(other);
    }

    public static async Task<Option<(A, B)>> zipAsync<A, B>(
        this Task<Option<A>> optionTask,
        Option<B> other)
    {
        var currentTask = await optionTask;
        return await currentTask.zipAsync(other);
    }

    public static async Task<Option<B>> andThenAsync<A, B>(
        this Task<Option<A>> optionTask,
        Option<B> other)
    {
        var currentTask = await optionTask;
        return await currentTask.andThenAsync(other);
    }

    public static async Task<B> foldAsync<A, B>(
        this Task<Option<A>> optionTask,
        Task<B> none, Func<A, Task<B>> some)
    {
        var currentTask = await optionTask;
        return await currentTask.foldAsync(none, some);
    }

    public static async Task<B> foldAsync<A, B>(
        this Task<Option<A>> optionTask,
        B none, Func<A, Task<B>> some)
    {
        var currentTask = await optionTask;
        return await currentTask.foldAsync(none, some);
    }

    public static async Task<B> foldAsync<A, B>(
        this Task<Option<A>> optionTask,
        Task<B> none, Func<A, B> some)
    {
        var currentTask = await optionTask;
        return await currentTask.foldAsync(none, some);
    }

    public static async Task<Option<A>> orElseAsync<A>(
        this Task<Option<A>> optionTask,
        A a)
    {
        var currentTask = await optionTask;
        return await currentTask.orElseAsync(a);
    }

    public static async Task<A> getOrElseAsync<A>(
        this Task<Option<A>> optionTask,
        A a)
    {
        var currentTask = await optionTask;
        return await currentTask.getOrElseAsync(a);
    }
}