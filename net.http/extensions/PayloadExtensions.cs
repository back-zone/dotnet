using back.zone.core.Monads.OptionMonad;
using back.zone.core.Monads.TryMonad;
using back.zone.net.http.models.payload;

namespace back.zone.net.http.extensions;

public static class PayloadExtensions
{
    public static Payload<TA> ToPayload<TA>(
        this Try<TA> tryA
    )
        where TA : notnull
    {
        return tryA.Fold(
            Payload.FailFromException<TA>,
            data => Payload.SucceedWithData(Option.Some(data))
        );
    }

    public static async Task<Payload<TA>> ToPayloadAsync<TA>(
        this Task<Try<TA>> tryAsync
    )
        where TA : notnull
    {
        return (await tryAsync).ToPayload();
    }
}