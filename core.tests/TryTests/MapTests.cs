using back.zone.core.Monads.TryMonad;
using core.tests.TestEnv;

namespace core.tests.TryTests;

public class MapTests
{
    public readonly BuilderEnv Env = new();

    [Fact]
    public void Should_Map_Success_Value()
    {
        var result = Try.Succeed(Env.IntValue).Map(x => x * 2);

        Assert.IsType<Try<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(Env.IntValue * 2, result.TryGetValue(out var value) ? value : default);
    }

    [Fact]
    public async Task Should_Map_Async_Then_Sync_Success_Value()
    {
        static int DoubleInt(int value)
        {
            return value * 2;
        }

        var result = await Try
            .Succeed(Env.IntValue)
            .MapAsync(Env.DoubleIntAsync)
            .Map(DoubleInt);

        Assert.IsType<Try<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(Env.IntValue * 2 * 2, result.GetOrElse(0));
    }

    [Fact]
    public void Should_Map_Failure()
    {
        var result = Try.Succeed(Env.IntValue).Map(x =>
        {
            throw new Exception(Env.FailureMessage);
            return x;
        });

        Assert.IsType<Try<int>>(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(Env.FailureMessage, result.TryGetException(out var exception) ? exception.Message : default);
    }

    [Fact]
    public void Should_Recover_From_Failure()
    {
        var result = Try.Succeed(Env.IntValue).Map(_ =>
            {
                throw new Exception(Env.FailureMessage);
                return Env.IntValue;
            })
            .Map(x => x * 2)
            .Recover(_ => 1)
            .Map(x => x * Env.IntValue);

        Assert.IsType<Try<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(Env.IntValue, result.GetOrElse(0));
    }
}