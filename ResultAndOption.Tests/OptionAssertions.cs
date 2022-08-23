using FluentAssertions;
using FluentAssertions.Execution;

namespace ResultAndOption.Tests;

public class OptionAssertions<T>
{
    private readonly Option<T> _option;
    
    public OptionAssertions(Option<T> option) => _option = option;

    [CustomAssertion]
    public AndConstraint<OptionAssertions<T>> Be(Option<T> other, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(_option == other)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:Option} to be {0}{reason}, but found {1}.", other, _option);
            
        return new AndConstraint<OptionAssertions<T>>(this);
    }
    
    [CustomAssertion]
    public void BeNone(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(_option is Option<T>.None)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:option} to be None{reason} but found {0}.");
    }

    [CustomAssertion]
    public void BeSome(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(_option is Option<T>.Some)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:option} to be Some{reason} but found {0}.");
    }

    [CustomAssertion]
    public void BeSome(T expected, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(_option is Option<T>.Some(T value) && value.Equals(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:option} to be Some{reason} but found {0}.");
    }
}