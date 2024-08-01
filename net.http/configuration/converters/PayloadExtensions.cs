using back.zone.monads.optionmonad;
using back.zone.net.http.models.payload;
using monads.iomonad;

namespace back.zone.net.http.configuration.converters;

public static class PayloadExtensions
{
    public static Payload<A> ToPayload<A>(
        this IO<A> ioA
    )
        where A : notnull
    {
        return ioA.fold(
            Payload<A>.FailFromException,
            data => Payload<A>.SucceedWithData(option.some(data))
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