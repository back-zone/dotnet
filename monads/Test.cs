using monads.iomonad;
using monads.result;

namespace monads;

public static class Test
{
    public static async void Main()
    {
        async Task<int> asyncZort(int n)
        {
            return await Task.FromResult(n);
        }

        Func<string, int> convertToInt = int.Parse;

        var xy = await io
            .succeed(1)
            .mapAsync(asyncZort)
            .mapAsync(asyncZort);

        Console.WriteLine("type of xy: " + xy.GetType());

        // var zortingen = io.succeed("hello").map(convertToInt);

        Console.WriteLine("Testing Result monad...");
    }
}