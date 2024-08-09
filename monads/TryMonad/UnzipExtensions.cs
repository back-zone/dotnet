using back.zone.monads.Types;

namespace back.zone.monads.TryMonad;

public static class TryExtensions
{
    public static Try<TC> Unzip<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Continuation<(TA, TB), TC> continuation)
        where TC : notnull
    {
        if (self.IsFailure()) return self.Exception();

        try
        {
            return continuation(self.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TC> Unzip<TA, TB, TC>(
        this Try<(TA, TB)> self,
        Func<TA, TB, TC> continuation
    )
        where TC : notnull
    {
        if (self.IsFailure()) return self.Exception();

        try
        {
            var (a, b) = self.Value();
            return continuation(a, b);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnzipAsync<TA, TB, TC>(
        this Task<Try<(TA, TB)>> self,
        Continuation<(TA, TB), TC> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var currentTask = await self;
            return currentTask.IsFailure()
                ? currentTask.Exception()
                : continuation(currentTask.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnzipAsync<TA, TB, TC>(
        this Task<Try<(TA, TB)>> self,
        Continuation<(TA, TB), Task<TC>> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var currentTask = await self;
            return currentTask.IsFailure()
                ? currentTask.Exception()
                : await continuation(currentTask.Value());
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnzipAsync<TA, TB, TC>(
        this Task<Try<(TA, TB)>> self,
        Func<TA, TB, TC> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var currentTask = await self;
            if (currentTask.IsFailure()) return currentTask.Exception();

            var (a, b) = currentTask.Value();
            return continuation(a, b);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TC>> UnzipAsync<TA, TB, TC>(
        this Task<Try<(TA, TB)>> self,
        Func<TA, TB, Task<TC>> continuation
    )
        where TA : notnull
        where TB : notnull
        where TC : notnull
    {
        try
        {
            var currentTask = await self;
            if (currentTask.IsFailure()) return currentTask.Exception();

            var (a, b) = currentTask.Value();
            return await continuation(a, b);
        }
        catch (Exception e)
        {
            return e;
        }
    }
}