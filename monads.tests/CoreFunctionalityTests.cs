using monads.iomonad;
using monads.iomonad.subtypes;

namespace monads.tests;

public class CoreFunctionalityTests
{
    [Fact]
    public void Should_succeed_with_42()
    {
        var result = io.succeed(42);
        var found = result.fold(_ => 0, n => n);

        Assert.IsType<Success<int>>(result);
        Assert.Equal(42, found);
    }

    [Fact]
    public async Task Should_succeed_with_42_async()
    {
        var task = Task.FromResult(42);
        var result = await io.ofAsync(task);
        var found = await result.foldAsync(_ => Task.FromResult(0), Task.FromResult);

        Assert.IsType<Success<int>>(result);
        Assert.Equal(42, found);
    }

    [Fact]
    public void Should_fail_with_exception()
    {
        var exception = new InvalidOperationException("#boom#");
        var result = io.fail<string>(exception);
        var found = result.fold(e => e, _ => new Exception("#value#"));

        Assert.IsType<Failure<string>>(result);
        Assert.IsType<InvalidOperationException>(found);
        Assert.Equal(exception, found);
    }

    [Fact]
    public void Should_recover_from_exception()
    {
        var exception = new Exception("#boom#");
        var result = io.fail<int>(exception).recover(_ => 42);
        var found = result.fold(e => 0, n => n);

        Assert.IsType<Success<int>>(result);
        Assert.Equal(42, found);
    }
}