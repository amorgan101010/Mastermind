using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind;

public class Evaluator
{
    public GuessEvaluation Evaluate(GuessAndSecret guessAndSecret)
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
        List<int> guess, List<int> guessSansBulls)
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

    private GuessAndSecret RemoveBulls(GuessAndSecret guessAndSecret)
    {
        var enrichedGuessAndSecret = Enrich(guessAndSecret);

        return new GuessAndSecret(
                RemoveBulls(
                    enrichedGuessAndSecret.Guess,
                    enrichedGuessAndSecret.Secret),
                RemoveBulls(
                    enrichedGuessAndSecret.Secret,
                    enrichedGuessAndSecret.Guess));
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

    private EnrichedGuessAndSecret Enrich(
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

public record EnrichedGuessAndSecret(Dictionary<int, int> Guess, Dictionary<int, int> Secret);

public record WeakElementFrequencies(int GuessFrequency, int SecretFrequency);

public record GuessEvaluation(int Bulls, int Cows);
