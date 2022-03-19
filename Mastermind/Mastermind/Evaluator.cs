using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind;

public class Evaluator
{
    public string Evaluate(GuessAndSecret guessAndSecret)
    {
        var secretSansStrongElements =
            guessAndSecret
                .Secret
                    .Where(
                        (s, i) => s != guessAndSecret.Guess[i])
                    .ToList();

        var guessSansStrongElements =
            guessAndSecret
                .Guess
                    .Where(
                        (g, i) => g != guessAndSecret.Secret[i])
                    .ToList();

        var strongCount =
            guessAndSecret.Guess.Count - guessSansStrongElements.Count;

        var distinctWeakElements =
            guessSansStrongElements
                .Where(g => secretSansStrongElements.Contains(g))
                .Distinct()
                .ToList();

        var weakCount = distinctWeakElements
            .Select(e => GetMinimumWeakCountForElement(e, guessAndSecret))
            .Sum();

        return $"{strongCount} strong, {weakCount} weak";
    }

    private int GetMinimumWeakCountForElement(
        int element, GuessAndSecret guessAndSecret)
    {
        return Math.Min(
            ElementFrequency(element, guessAndSecret.Secret),
            ElementFrequency(element, guessAndSecret.Guess));
    }

    private int ElementFrequency(int element, List<int> sequence)
    {
        return sequence
            .Where(s => s == element)
            .ToList()
            .Count;
    }
}

public record GuessAndSecret(List<int> Guess, List<int> Secret);
