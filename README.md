# Monads Library

This is a simple monads library written in C#. The library provides functionalities for working with monads, such as the `IO` monad.

## Installation

To use this library, you can add it as a reference to your project. You can download the source code or build the library yourself.

## Naming Conventions

To avoid confusion and maintain consistency, the naming conventions in this library follow a specific pattern:

- Functions that operate on the `IO` monad are named using the prefix `io`. For example, `io.map`, `io.flatMap`, `io.succeed`, and `io.fail`.
- Functions that operate on other monads (e.g., `Maybe`, `Result`) are named without the prefix. For example, `map`, `flatMap`, `some`, and `none`.

This naming convention helps users understand which functions are specific to the `IO` monad and which functions are general-purpose.

## Usage

Here's an example of how to use the `IO` monad in your code:

```csharp
using monads.iomonad;

public class Program
{
    public static void Main()
    {
        var result = io.succeed(42)
            .map(n => n * 2)
            .flatMap(n => io.succeed(n + 10));

        var value = result.Fold(_ => 0, n => n);
        Console.WriteLine(value); // Output: 94
    }
}