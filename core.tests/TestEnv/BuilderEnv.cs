using back.zone.core.Monads.TryMonad;

namespace core.tests.TestEnv;

public class BuilderEnv
{
    public int IntValue { get; } = 42;
    public string IntAsString { get; } = "42";
    public string Hello { get; } = "Hello";
    public string World { get; } = "World";

    public string FailureMessage { get; } = "#failed_with_exception#";

    public int BuildInt()
    {
        return IntValue;
    }

    public int FaultyInt()
    {
        throw new Exception(FailureMessage);
    }

    public string BuildString()
    {
        return Hello + " " + World;
    }

    public async Task<int> BuildIntAsync()
    {
        return await Task.FromResult(IntValue);
    }

    public async Task<int> DoubleIntAsync(int value)
    {
        return await Task.FromResult(value * 2);
    }

    public async Task<Try<int>> DoubIntAsyncFB(int value)
    {
        return await Task.FromResult(Try.Succeed(value * 2));
    }

    public async Task<int> FaultyIntAsync()
    {
        throw new Exception(FailureMessage);
    }

    public async Task<string> BuildStringAsync()
    {
        return await Task.FromResult(BuildString());
    }
}