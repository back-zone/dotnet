using back.zone.monads.TryMonad;
using back.zone.monads.TryMonad.SubTypes;
using Xunit.Abstractions;

namespace back.zone.monads.tests.iotests;

public class CoreFunctionalityTests(ITestOutputHelper testOutputHelper)
{
    private static async Task<Try<int>> BuildInt(string s)
    {
        return await Task.FromResult(int.Parse(s));
    }

    private Try<int> BuildIntSync(string s)
    {
        return int.Parse(s);
    }


    [Fact]
    public void TestMap()
    {
    }

    [Fact]
    public async Task TestFlatMap()
    {
        var comp =
            await Try.Succeed("42")
                .FlatMapAsync(BuildInt)
                .MapAsync(x => x * 2);


        Assert.IsType<Success<int>>(comp);
    }
}