using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind;

public class Evaluator
{
    public GuessEvaluation Evaluate(GuessAndSecret guessAndSecret)
    {

        var guessAndSecretSansStrongElements =
            RemoveStrongElements(
                guessAndSecret);

        var bullCount =
            CalculateStrongCount(
                guessAndSecret.Guess,
                guessAndSecretSansStrongElements.Guess);

        var cowCount =
            CalculateWeakCount(
                guessAndSecretSansStrongElements);

        return new GuessEvaluation(bullCount, cowCount);
    }

    private int CalculateStrongCount(
        List<int> guess, List<int> guessSansStrongElements)
    {
        return guess.Count - guessSansStrongElements.Count;
    }

    private int CalculateWeakCount(
        GuessAndSecret guessAndSecretSansStrongElements)
    {
        return guessAndSecretSansStrongElements.Guess
            .Intersect(
                guessAndSecretSansStrongElements.Secret)
            .Select(
                distinctWeakElement =>
                    GetWeakElementMinimumFrequency(
                        distinctWeakElement,
                        guessAndSecretSansStrongElements))
            .Sum();
    }

    private int GetWeakElementMinimumFrequency(
        int distinctWeakElement, GuessAndSecret guessAndSecretSansStrongElements)
    {
        return Math.Min(
            ElementFrequency(
                distinctWeakElement,
                guessAndSecretSansStrongElements.Guess),
                        
            ElementFrequency(
                distinctWeakElement,
                guessAndSecretSansStrongElements.Secret));
    }

    private GuessAndSecret RemoveStrongElements(GuessAndSecret guessAndSecret)
    {
        var enrichedGuessAndSecret = Enrich(guessAndSecret);

        return new GuessAndSecret(
                enrichedGuessAndSecret.Guess
                    .Except(enrichedGuessAndSecret.Secret)
                    .Select(x => x.Value)
                    .ToList(),
                enrichedGuessAndSecret.Secret
                    .Except(enrichedGuessAndSecret.Guess)
                    .Select(x => x.Value)
                    .ToList());
    }

    private int ElementFrequency(int element, List<int> sequence)
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
