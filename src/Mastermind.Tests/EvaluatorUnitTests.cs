using System.Collections.Generic;
using Xunit;

namespace Mastermind.Tests;

public class EvaluatorUnitTests
{
    public List<int> _defaultSecret = new List<int>() { 22, 6, 8, 3, 3 };

    [Fact]
    public void HandlesGuess_WithTotallyWrongElements()
    {
        var guess = new List<int>() { 21, 5, 7, 2, 1 };
        var secret = new List<int>() { 22, 6, 8, 3, 3 };
        var expected = new GuessEvaluation(0, 0);

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesCorrectGuess()
    {
        var secret = new List<int>() { 22, 6, 8, 3, 3 };
        var guess = secret;

        var expected = new GuessEvaluation(5, 0);

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
        var secret = new List<int>() { 22, 6, 8, 3, 3 };
        var expected = new GuessEvaluation(1, 0);

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
        var secret = new List<int>() { 22, 6, 8, 3, 3 };
        var expected = new GuessEvaluation(0, 1);

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
        var secret = new List<int>() { 22, 6, 8, 3, 3 };
        var expected = new GuessEvaluation(1, 0);

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
        var secret = new List<int>() { 22, 6, 8, 3, 3 };
        var expected = new GuessEvaluation(1, 0);

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
        var secret = new List<int>() { 22, 6, 8, 3, 3 };
        var expected = new GuessEvaluation(1, 0);

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
        var secret = new List<int>() { 22, 6, 8, 3, 3 };
        var expected = new GuessEvaluation(0, 2);

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
        var expected = new GuessEvaluation(0, 2);

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    // TODO: versions of the two tests above
    // but with a strong guess thrown in
    [Fact]
    public void HandlesGuess_WithMoreDuplicateWeakGuessElements_ThanDuplicateMatchingSecretElements_AndMatchingStrongElement()
    {
        var guess = new List<int>() { 1, 3, 3, 3, 1 };
        var secret = new List<int>() { 22, 6, 8, 3, 3 };
        var expected = new GuessEvaluation(1, 1);

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesGuess_WithFewerDuplicateWeakGuessElements_ThanDuplicateMatchingSecretElements_AndMatchingStrongElement()
    {
        var guess = new List<int>() { 1, 3, 3, 7, 1 };
        var secret = new List<int>() { 6, 6, 3, 3, 3 };
        var expected = new GuessEvaluation(1, 1);

        var sut = new Evaluator();

        var actual =
            sut.Evaluate(
                new GuessAndSecret(guess, secret));

        Assert.Equal(expected, actual);
    }
}