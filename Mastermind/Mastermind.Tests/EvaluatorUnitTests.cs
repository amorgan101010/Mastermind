using System.Collections.Generic;
using Xunit;

namespace Mastermind.Tests;

// Any commented out InlineData indicates the method either doesn't exist
// or the test scenario fails for that implementation.

public class EvaluatorUnitTests
{
    public List<int> _defaultSecret = new List<int>() { 22, 6, 8, 3, 3 };

    [Fact]
    public void HandlesGuess_WithTotallyWrongElements()
    {
        var guess = new List<int>() { 21, 5, 7, 2, 1 };
        var secret = _defaultSecret;
        var expected = "0 strong, 0 weak";

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesGuess_WithSingleStrongGuessElement()
    {
        var guess = new List<int>() { 22, 5, 7, 2, 1 };
        var secret = _defaultSecret;
        var expected = "1 strong, 0 weak";

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesGuess_WithSingleWeakGuessElement()
    {
        var guess = new List<int>() { 5, 22, 7, 2, 1 };
        var secret = _defaultSecret;
        var expected = "0 strong, 1 weak";

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesGuess_WithWeakGuessElement_SupercededByLaterStrongGuessElement()
    {
        var guess = new List<int>() { 8, 0, 8, 2, 1 };
        var secret = _defaultSecret;
        var expected = "1 strong, 0 weak";

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesGuess_WithStrongGuessElement_OverridingLaterWeakGuessElement()
    {
        var guess = new List<int>() { 22, 22, 7, 2, 1 };
        var secret = _defaultSecret;
        var expected = "1 strong, 0 weak";

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesGuess_WithSingleStrongGuessElement_OverridingManyWeakGuessElements()
    {
        var guess = new List<int>() { 8, 8, 8, 8, 1 };
        var secret = _defaultSecret;
        var expected = "1 strong, 0 weak";

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesGuess_WithMoreDuplicateWeakGuessElements_ThanDuplicateMatchingSecretElements()
    {
        var guess = new List<int>() { 3, 3, 3, 7, 1 };
        var secret = _defaultSecret;
        var expected = "0 strong, 2 weak";

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesGuess_WithFewerDuplicateWeakGuessElements_ThanDuplicateMatchingSecretElements()
    {
        var guess = new List<int>() { 3, 3, 7, 7, 1 };
        var secret = new List<int>() { 6, 6, 3, 3, 3 };
        var expected = "0 strong, 2 weak";

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }
}