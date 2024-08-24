namespace back.zone.core.Monads.OptionMonad;

public static class OptionUnWrap
{
    public static TA? UnWrap<TA>(
        this Option<TA> option
    )
        where TA : notnull
    {
        return option.Fold(
            () => default!,
            value => value
        );
    }

    public static async Task<TA?> UnWrapAsync<TA>(
        this Task<Option<TA>> option
    )
        where TA : notnull
    {
        return (await option.ConfigureAwait(false)).UnWrap();
    }
}