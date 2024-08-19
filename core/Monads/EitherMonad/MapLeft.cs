using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherMapLeft
{
    public static Either<TL1, TR> MapLeft<TL, TL1, TR>(
        this Either<TL, TR> self,
        Continuation<TL, TL1> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return EitherRunTime.RunEither(self, continuation);
    }

    public static async Task<Either<TL1, TR>> MapLeftAsync<TL, TL1, TR>(
        this Either<TL, TR> self,
        Continuation<TL, Task<TL1>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Either<TL1, TR>> MapLeftAsync<TL, TL1, TR>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, Task<TL1>> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Either<TL1, TR>> MapLeftAsync<TL, TL1, TR>(
        this Task<Either<TL, TR>> self,
        Continuation<TL, TL1> continuation
    )
        where TL : notnull
        where TL1 : notnull
        where TR : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }
}