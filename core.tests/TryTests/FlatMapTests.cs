using back.zone.core.Monads.OptionMonad;
using back.zone.core.Monads.TryMonad;
using core.tests.TestEnv;

namespace core.tests.TryTests;

public class FlatMapTests
{
    public readonly BuilderEnv Env = new();

    [Fact]
    public void Should_FlatMap_Success_Value()
    {
        var result = Try.Succeed(Env.IntValue).FlatMap(x => Try.Succeed(x * 2));

        Assert.IsType<Try<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(Env.IntValue * 2, result.TryGetValue(out var value) ? value : default);
    }

    [Fact]
    public async Task Should_FlatMap_Async_Then_Sync_Success_Value()
    {
        static Try<int> DoubleInt(int value)
        {
            return Try.Succeed(value * 2);
        }

        var result = await Try
            .Succeed(Env.IntValue)
            .FlatMapAsync(Env.DoubIntAsyncFB)
            .FlatMap(DoubleInt);

        Assert.IsType<Try<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(Env.IntValue * 2 * 2, result.GetOrElse(0));
    }

    [Fact]
    public void Should_FlatMap_Success_Value_With_Failure()
    {
        var result = Try.Succeed(Env.IntValue).FlatMap(x => Try.Fail<int>(new Exception(Env.FailureMessage)));

        var resultOpt = Option.Async(() => Task.FromResult("asd"))
            .FlatMapAsync(MaybeParse);

        async Task<Option<int>> MaybeParse(string s)
        {
            return await Task.FromResult(int.Parse(s));
        }

        Assert.IsType<Try<int>>(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(Env.FailureMessage, result.TryGetException(out var exception) ? exception.Message : default);
    }

    [Fact]
    public async Task Should_FlatMap_Async_Then_Sync_Success_Value_With_Failure()
    {
        static int DoubleInt(int value, string failureMessage)
        {
            throw new Exception(failureMessage);
            return value * 2;
        }

        var result = await Try
            .Succeed(Env.IntValue)
            .FlatMapAsync(Env.DoubIntAsyncFB)
            .Zip(Try.Succeed(Env.FailureMessage))
            .UnZip(DoubleInt);

        Assert.IsType<Try<int>>(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(Env.FailureMessage, result.TryGetException(out var exception) ? exception.Message : default);
    }
}