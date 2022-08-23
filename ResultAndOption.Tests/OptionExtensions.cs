namespace ResultAndOption.Tests;

public static class OptionExtensions
{
    public static OptionAssertions<T> Should<T>(this Option<T> option) => new OptionAssertions<T>(option);
}