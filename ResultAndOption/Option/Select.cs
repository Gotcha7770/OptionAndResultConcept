namespace System;

public static partial class Options
{
    public static Option<TResult> Select<T, TResult>(this Option<T> source, Func<T, TResult> selector)
    {
        return source.Match( 
            some: x => selector(x), 
            none: () => Option<TResult>.None);
    }
    
    public static Option<TResult> SelectMany<T, TResult>(this Option<T> source, Func<T, Option<TResult>> selector)
    {
        return source.Match(
            some: selector,
            none: () => Option<TResult>.None);
    }
    
    public static Option<TResult> SelectMany<T1, T2, TResult>(
        this Option<T1> source,
        Func<T1, Option<T2>> selector,
        Func<T1, T2, TResult> resultSelector)
    {
        return source.Match(
            some: x => selector(x).Select(y => resultSelector(x, y)),
            none: () => Option<TResult>.None);
    }
}