using System.Collections.Generic;
using Xunit;

namespace Mastermind.Tests;

public class EvaluatorUnitTests
{
    public List<int> _defaultSecret = new List<int>() { 22, 6, 8, 3, 3 };

    // TODO: Fill out the tests
    [Theory]
    [InlineData("NiaveImplementation")]
    //[InlineData("BetterImplementation")]
    //[InlineData("CoolImplementation")]
    //[InlineData("EdgyImplementation")]
    //[InlineData("RobustImplementation")]
    public void HandlesGuess_WithTotallyWrongElements(string implementationMethodName)
    {
        var guess = new List<int>() { 21, 5, 7, 2, 1 };
        var secret = _defaultSecret;
        var expected = "0 strong, 0 weak";

        var sut = new Evaluator();

        // I would not normally do this reflection stuff, because
        // I wouldn't normally have multiple functionally identical implementations.
        // Well, they're _intended_ to be identical, but some don't cover edge cases.
        var actual =
            typeof(Evaluator)
                .GetMethod(implementationMethodName)
                .Invoke(sut, new [] { guess, secret });

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("NiaveImplementation")]
    [InlineData("BetterImplementation")]
    [InlineData("CoolImplementation")]
    [InlineData("EdgyImplementation")]
    [InlineData("RobustImplementation")]
    public void HandlesGuess_WithSingleStrongGuessElement(string implementationMethodName)
    {
        Assert.Empty("TODO: Implement");
    }

    [Theory]
    [InlineData("NiaveImplementation")]
    [InlineData("BetterImplementation")]
    [InlineData("CoolImplementation")]
    [InlineData("EdgyImplementation")]
    [InlineData("RobustImplementation")]
    public void HandlesGuess_WithSingleWeakGuessElement(string implementationMethodName)
    {
        Assert.Empty("TODO: Implement");
    }

    [Theory]
    [InlineData("NiaveImplementation")]
    [InlineData("BetterImplementation")]
    [InlineData("CoolImplementation")]
    [InlineData("EdgyImplementation")]
    [InlineData("RobustImplementation")]
    public void HandlesGuess_WithWeakGuessElement_SupercededByLaterStrongGuessElement(string implementationMethodName)
    {
        Assert.Empty("TODO: Implement");
    }

    [Theory]
    [InlineData("NiaveImplementation")]
    [InlineData("BetterImplementation")]
    [InlineData("CoolImplementation")]
    [InlineData("EdgyImplementation")]
    [InlineData("RobustImplementation")]
    public void HandlesGuess_WithStrongGuessElement_OverridingLaterWeakGuessElement(string implementationMethodName)
    {
        Assert.Empty("TODO: Implement");
    }

    [Theory]
    [InlineData("NiaveImplementation")]
    [InlineData("BetterImplementation")]
    [InlineData("CoolImplementation")]
    [InlineData("EdgyImplementation")]
    [InlineData("RobustImplementation")]
    public void HandlesGuess_WithSingleStrongGuessElement_OverridingManyWeakGuessElements(string implementationMethodName)
    {
        Assert.Empty("TODO: Implement");
    }

    [Theory]
    [InlineData("NiaveImplementation")]
    [InlineData("BetterImplementation")]
    [InlineData("CoolImplementation")]
    [InlineData("EdgyImplementation")]
    [InlineData("RobustImplementation")]
    public void HandlesGuess_WithMoreDuplicateWeakGuessElements_ThanDuplicateMatchingSecretElements(string implementationMethodName)
    {
        Assert.Empty("TODO: Implement");
    }

    [Theory]
    [InlineData("NiaveImplementation")]
    [InlineData("BetterImplementation")]
    [InlineData("CoolImplementation")]
    [InlineData("EdgyImplementation")]
    [InlineData("RobustImplementation")]
    public void HandlesGuess_WithFewerDuplicateWeakGuessElements_ThanDuplicateMatchingSecretElements(string implementationMethodName)
    {
        Assert.Empty("TODO: Implement");
    }
}