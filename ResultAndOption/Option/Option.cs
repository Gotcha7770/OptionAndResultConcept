namespace System;

public sealed class Option
{
    private static readonly Option _none = new Option();
    
    private Option() { }
    
    public static Option<T> Some<T>(T value) => new Option<T>.Some(value);

    public static Option None => _none;
}

public abstract record Option<T>
{
    internal static readonly None NoneValue = new None();
    
    public sealed record Some(T Value) : Option<T>;

    public sealed record None : Option<T> 
    {
        internal None() {}
    }
    
    public T GetValueOrDefault()
    {
        return this switch
        {
            Some(T value) => value,
            _ => default
        };
    }

    public static implicit operator Option<T>(Option _) => NoneValue;
    
    public static implicit operator Option<T>(T value) => Option.Some(value);
}