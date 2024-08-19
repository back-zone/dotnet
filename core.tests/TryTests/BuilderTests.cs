using back.zone.core.Monads.TryMonad;
using core.tests.TestEnv;

namespace core.tests.TryTests;

public class BuilderTests
{
    public readonly BuilderEnv Env = new();

    [Fact]
    public void Should_Succeed_With_Valid_Value()
    {
        var result = Try.Succeed(Env.IntValue);

        Assert.IsType<Try<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(Env.IntValue, result.TryGetValue(out var value) ? value : default);
    }

    [Fact]
    public void Should_Fail_With_Valid_Exception()
    {
        int DoubleIt(int i)
        {
            throw new InvalidOperationException(Env.FailureMessage);
            return i * 2;
        }

        async Task<int> ParseIntAsync(string s)
        {
            return await Task.FromResult(int.Parse(s));
        }

        var result = Try.Fail<int>(new Exception(Env.FailureMessage));

        Assert.IsType<Try<int>>(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(Env.FailureMessage, result.TryGetException(out var exception) ? exception.Message : default);
    }

    [Fact]
    public void Should_Succeed_With_Valid_Effect()
    {
        var result = Try.Effect(Env.BuildInt);

        Assert.IsType<Try<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(Env.IntValue, result.TryGetValue(out var value) ? value : default);
    }

    [Fact]
    public void Should_Recover_From_Faulty_Effect()
    {
        var result = Try.Effect(Env.FaultyInt);

        Assert.IsType<Try<int>>(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(Env.FailureMessage, result.TryGetException(out var exception) ? exception.Message : default);
    }

    [Fact]
    public async Task Should_Succeed_With_Valid_Async_Effect()
    {
        var result = await Try.Async(Env.BuildIntAsync);

        Assert.IsType<Try<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(Env.IntValue, result.TryGetValue(out var value) ? value : default);
    }

    [Fact]
    public async Task Should_Recover_From_Faulty_Async_Effect()
    {
        var result = await Try.Async(Env.FaultyIntAsync);

        Assert.IsType<Try<int>>(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(Env.FailureMessage, result.TryGetException(out var exception) ? exception.Message : default);
    }
}