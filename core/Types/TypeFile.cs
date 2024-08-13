namespace back.zone.core.Types;

public delegate TA Effect<out TA>()
    where TA : notnull;

public delegate TB Continuation<in TA, out TB>(TA a)
    where TA : notnull
    where TB : notnull;

public delegate TC Zipper<in TA, in TB, out TC>(TA a, TB b)
    where TA : notnull
    where TB : notnull;

public delegate TC Zip<TA, TB, out TC>((TA, TB) tuple);

public delegate void Callback<in TA>(TA a)
    where TA : notnull;

public delegate void RegisterCallback<out TA>(Callback<TA> register)
    where TA : notnull;