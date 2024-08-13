namespace back.zone.core.Types;

public delegate TA Effect<out TA>()
    where TA : notnull;

public delegate TA OptionalEffect<out TA>();

public delegate TB Continuation<in TA, out TB>(TA a)
    where TA : notnull
    where TB : notnull;

public delegate TB OptionalContinuation<in TA, out TB>(TA a);

public delegate TC Zipper<in TA, in TB, out TC>(TA a, TB b)
    where TA : notnull
    where TB : notnull;

public delegate TC OptionalZipper<in TA, in TB, out TC>(TA a, TB b);

public delegate TC Zip<TA, TB, out TC>((TA, TB) tuple);

public delegate TC OptionalZip<TA, TB, out TC>((TA, TB) tuple);

public delegate void Callback<in TA>(TA a)
    where TA : notnull;

public delegate void RegisterCallback<out TA>(Callback<TA> register)
    where TA : notnull;