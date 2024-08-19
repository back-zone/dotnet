using back.zone.core.Types;

namespace back.zone.core.Monads.EitherMonad;

public static class EitherFlatMap
{
    public static Either<TL, TR1> FlatMap<TL, TR, TR1>(
        this Either<TL, TR> self,
        Continuation<TR, Either<TL, TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return EitherRunTime.RunEither(self, continuation);
    }

    public static async Task<Either<TL, TR1>> FlatMapAsync<TL, TR, TR1>(
        this Either<TL, TR> self,
        Continuation<TR, Task<Either<TL, TR1>>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Either<TL, TR1>> FlatMapAsync<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Continuation<TR, Task<Either<TL, TR1>>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }

    public static async Task<Either<TL, TR1>> FlatMap<TL, TR, TR1>(
        this Task<Either<TL, TR>> self,
        Continuation<TR, Either<TL, TR1>> continuation
    )
        where TL : notnull
        where TR : notnull
        where TR1 : notnull
    {
        return await EitherRunTime.RunEitherAsync(self, continuation).ConfigureAwait(false);
    }
}