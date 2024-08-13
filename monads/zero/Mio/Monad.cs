using back.zone.monads.Types;

namespace back.zone.monads.zero.Mio;

public readonly struct Mio<TA> where TA : notnull
{
    public bool IsSuccess { get; }
    public TA? Value { get; init; }
    public Exception? Exception { get; init; }

    public Mio(TA value)
    {
        Value = value;
        IsSuccess = true;
        Exception = null;
    }

    public Mio(Exception exception)
    {
        Value = default;
        IsSuccess = false;
        Exception = exception;
    }

    public static implicit operator Mio<TA>(TA value)
    {
        return new Mio<TA>(value);
    }

    public static implicit operator Mio<TA>(Exception exception)
    {
        return new Mio<TA>(exception);
    }

    public Mio<TB> FlatMap<TB>(
        Continuation<TA, Mio<TB>> continuation
    )
        where TB : notnull
    {
        try
        {
            return IsSuccess
                ? continuation(Value!)
                : Exception!;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<Mio<TB>> FlatMapAsync<TB>(
        Continuation<TA, Task<Mio<TB>>> continuation
    )
        where TB : notnull
    {
        try
        {
            return await continuation(Value!).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public Mio<TB> Map<TB>(
        Continuation<TA, TB> continuation
    )
        where TB : notnull
    {
        try
        {
            return IsSuccess
                ? continuation(Value!)
                : Exception!;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<Mio<TB>> MapAsync<TB>(
        Continuation<TA, Task<TB>> continuation
    )
        where TB : notnull
    {
        try
        {
            return await continuation(Value!).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public TU GetOrElse<TU>(
        TU other
    )
        where TU : TA
    {
        return IsSuccess ? (TU)Value! : other;
    }
}