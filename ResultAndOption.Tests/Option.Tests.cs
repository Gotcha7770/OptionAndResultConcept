using FluentAssertions;
using static System.Option<int>;

namespace ResultAndOption.Tests;

/// <summary>
/// Design requirements
/// </summary>
public class OptionTests
{
    [Fact]
    public void Default_Option_ShouldBeNone()
    {
        Option<int> option = default;
        
        option.Should().BeNone();
    }
    
    [Fact]
    public void GetValueOrDefault_None_ReturnsDefault()
    {
        Option<int> option = Option.None;
        int result = option.GetValueOrDefault();
        
        result.Should().Be(0);
    }
    
    [Fact]
    public void GetValueOrDefault_Some_ReturnsValue()
    {
        var option = Option.Some(42);
        int result = option.GetValueOrDefault();
        
        result.Should().Be(42);
    }

    [Fact]
    public void Option_PatternMatching()
    {
        Option<int> option = Option.None;
        (option is not None).Should().BeFalse();
        (option is Some(42)).Should().BeFalse();
    }
    
    [Fact]
    public void Option_Switch_Statement()
    {
        var option = Option.Some(42);

        switch (option)
        {
            case Some(int value):
                break;
            
            default:
                throw new InvalidOperationException();
        }
    }

    [Fact]
    public void Option_Switch_Expression()
    {
        var option = Option.Some(42);
        int result = option switch
        {
            Some(int value) => value,
            _ => 0
        };
        
        result.Should().Be(42);
        
        option = Option.None;
        result = option switch
        {
            None => 0,
            _ => -1
        };
        
        result.Should().Be(0);
    }

    [Fact]
    public void Option_LINQ()
    {
        var result = from x in Option.Some(1)
                     from y in Option.Some(2)
                     select x + y;
        
        result.Should().BeSome(3);
    }
}