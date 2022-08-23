namespace System;

public static partial class Options
{
    public static Option<TResult> Select<T, TResult>(this Option<T> source, Func<T, TResult> selector)
    {
        return source switch
        {
            Option<T>.Some(T value) => selector(value),
            //some value => selector(value)
            _ => Option.None
            //none => none
        };
    }
    
    public static Option<TResult> SelectMany<T, TResult>(this Option<T> source, Func<T, Option<TResult>> selector)
    {
        return source switch
        {
            Option<T>.Some(T value) => selector(value),
            _ => Option.None
        };
    }
    
    public static Option<TResult> SelectMany<T1, T2, TResult>(
        this Option<T1> source,
        Func<T1, Option<T2>> selector,
        Func<T1, T2, TResult> resultSelector)
    {
        return source switch
        {
            Option<T1>.Some(T1 value) => source.SelectMany(selector).Select(x => resultSelector(value, x)),
            _ => Option.None
        };
    }
}