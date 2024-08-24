namespace back.zone.core.Monads.EitherMonad;

public static class EitherUnWrap
{
    public static TR? Unwrap<TL, TR>(
        this Either<TL, TR> either
    )
        where TL : notnull
        where TR : notnull
    {
        return either.Fold(
            _ => default!,
            right => right
        );
    }

    public static async Task<TR?> UnwrapAsync<TL, TR>(
        this Task<Either<TL, TR>> eitherAsync
    )
        where TL : notnull
        where TR : notnull
    {
        return (await eitherAsync.ConfigureAwait(false)).Unwrap();
    }
}