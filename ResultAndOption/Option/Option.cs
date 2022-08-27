namespace System;

public sealed class None
{
    public static None Value { get; } = new None();

    private None() { }
}

public static class Option
{
    public static Option<T> Some<T>(T value) => new Option<T>(value);

    public static None None => None.Value;
}

public readonly struct Option<T> : IEquatable<Option<T>>
{
    private readonly T _value;
    private readonly bool _isSome;
    
    public static readonly Option<T> None = default;

    public Option(T value)
    {
        _value = value;
        _isSome = value is not null;
    }
    
    public T GetValueOrDefault() => _value;
    
    public T GetValueOrDefault(T @default) => _isSome ? _value : @default;

    public void Switch(Action<T> some, Action none)
    {
        if (_isSome)
        {
            some(_value);
        }
        else
        {
            none();
        }
    }
    
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        return _isSome ? some(_value) : none();
    }
    
    public override bool Equals(object obj)
    {
        return obj is Option<T> other && Equals(other);
    }
    
    public bool Equals(Option<T> other)
    {
        if (_isSome)
        {
            T value = _value;
            return other.Match(x => x.Equals(value), () => false);
        }
        
        return other.Match(_ => false, () => true);
    }

    public override int GetHashCode() => _isSome ? _value.GetHashCode() : 0;

    public override string ToString()
    {
        return _isSome ? $"{{ Value: {_value} }}" : nameof(None);
    }
    
    public static bool operator ==(Option<T> one, Option<T> other)
    {
        return one.Equals(other);
    }

    public static bool operator !=(Option<T> one, Option<T> other)
    {
        return !(one == other);
    }
    
    public static bool operator ==(Option<T> one, None _)
    {
        return !one._isSome;
    }

    public static bool operator !=(Option<T> one, None other)
    {
        return !(one == other);
    }

    public static implicit operator Option<T>(None _) => None;
    
    public static implicit operator Option<T>(T value) => Option.Some(value);
}