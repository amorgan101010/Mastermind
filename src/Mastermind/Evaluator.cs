using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind;

public class Evaluator
{
    public GuessEvaluation Evaluate(GuessAndSecret guessAndSecret)
    {
        return Evaluate(Disorder(guessAndSecret));
    }

    // I assume I can easily pass in an unordered guess and secret
    // if/when I'm using this to evaluate a guess in a game...
    // I was just given lists in the original problem.
    public GuessEvaluation Evaluate(UnorderedGuessAndSecret guessAndSecret)
    {
        var guessAndSecretSansBulls =
            RemoveBulls(
                guessAndSecret);

        var bullCount =
            GetBullCount(
                guessAndSecret.Guess,
                guessAndSecretSansBulls.Guess);

        var cowCount =
            GetCowCount(
                guessAndSecretSansBulls);

        return new GuessEvaluation(bullCount, cowCount);
    }

    private int GetBullCount(
        Dictionary<int, int> guess, List<int> guessSansBulls)
    {
        return guess.Count - guessSansBulls.Count;
    }

    private int GetCowCount(
        GuessAndSecret guessAndSecretSansBulls)
    {
        return guessAndSecretSansBulls.Guess
            .Intersect(
                guessAndSecretSansBulls.Secret)
            .Select(
                distinctCows =>
                    GetCowMinimumFrequency(
                        distinctCows,
                        guessAndSecretSansBulls))
            .Sum();
    }

    private int GetCowMinimumFrequency(
        int distinctCow, GuessAndSecret guessAndSecretSansBulls)
    {
        return Math.Min(
            CowFrequency(
                distinctCow,
                guessAndSecretSansBulls.Guess),
            CowFrequency(
                distinctCow,
                guessAndSecretSansBulls.Secret));
    }

    private GuessAndSecret RemoveBulls(UnorderedGuessAndSecret unorderedGuessAndSecret)
    {
        return new GuessAndSecret(
                RemoveBulls(
                    unorderedGuessAndSecret.Guess,
                    unorderedGuessAndSecret.Secret),
                RemoveBulls(
                    unorderedGuessAndSecret.Secret,
                    unorderedGuessAndSecret.Guess));
    }

    private List<int> RemoveBulls(
        Dictionary<int, int> left, Dictionary<int, int> right)
    {
        return left
            .Except(right)
            .Select(x => x.Value)
            .ToList();
    }

    private int CowFrequency(int element, List<int> sequence)
    {
        return sequence
            .Where(s => s == element)
            .ToList()
            .Count;
    }

    private UnorderedGuessAndSecret Disorder(
        GuessAndSecret guessAndSecret)
    {
        return new(
            guessAndSecret
                .Guess
                    .Select((g, i) => new { g, i })
                    .ToDictionary(x => x.i, x => x.g),
            guessAndSecret
                .Secret
                    .Select((s, i) => new { s, i })
                    .ToDictionary(x => x.i, x => x.s)
        );
    }
}

public record GuessAndSecret(List<int> Guess, List<int> Secret);

public record UnorderedGuessAndSecret(Dictionary<int, int> Guess, Dictionary<int, int> Secret);

public record WeakElementFrequencies(int GuessFrequency, int SecretFrequency);

public record GuessEvaluation(int Bulls, int Cows);
