using System.Collections.Immutable;

namespace back.zone.net.http.utils.collections;

public static class Collections
{
    public static ref T SelectByReference<T>(ref T t)
    {
        return ref t;
    }

    public static ImmutableArray<A> Flatten<A>(params ImmutableArray<A>[] arrays)
    {
        var result = ImmutableArray.CreateBuilder<A>();

        foreach (var array in arrays) result.AddRange(array);

        return result.ToImmutable();
    }
}