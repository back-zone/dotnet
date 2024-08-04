using back.zone.monads.iomonad;
using back.zone.monads.optionmonad;
using back.zone.net.http.models.payload;

namespace back.zone.net.http.extensions;

public static class PayloadExtensions
{
    public static Payload<A> ToPayload<A>(
        this IO<A> ioA
    )
        where A : notnull
    {
        return ioA.fold(
            Payload.FailFromException<A>,
            data => Payload.SucceedWithData(option.some(data))
        );
    }

    public static async Task<Payload<A>> ToPayloadAsync<A>(
        this Task<IO<A>> asyncIo
    )
        where A : notnull
    {
        var currentTask = await asyncIo;

        return currentTask.ToPayload();
    }
}