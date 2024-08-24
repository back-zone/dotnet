namespace back.zone.core.Monads.TryMonad;

public static class TryUnWrap
{
    public static TA? UnWrap<TA>(
        this Try<TA> tryA
    )
        where TA : notnull
    {
        return tryA.Fold(
            _ => default!,
            value => value
        );
    }

    public static async Task<TA?> UnwrapAsync<TA>(
        this Task<Try<TA>> tryAsync
    )
        where TA : notnull
    {
        return (await tryAsync.ConfigureAwait(false)).UnWrap();
    }
}