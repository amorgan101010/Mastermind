using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind;

public class Evaluator
{
    public string Evaluate(GuessAndSecret guessAndSecret)
    {

        var guessAndSecretSansStrongElements =
            RemoveStrongElements(guessAndSecret);

        var strongCount =
            guessAndSecret.Guess.Count - guessAndSecretSansStrongElements.Guess.Count;

        var distinctWeakElements =
            guessAndSecretSansStrongElements.Guess
                .Intersect(
                    guessAndSecretSansStrongElements.Secret)
                .ToList();

        var weakCount = distinctWeakElements
            .Select(e => GetMinimumWeakCountForElement(e, guessAndSecretSansStrongElements))
            .Sum();

        return $"{strongCount} strong, {weakCount} weak";
    }

    private GuessAndSecret RemoveStrongElements(GuessAndSecret guessAndSecret)
    {
        return new GuessAndSecret(
            guessAndSecret
                .Guess
                    .Where(
                        (g, i) => g != guessAndSecret.Secret[i])
                    .ToList(),
            guessAndSecret
                .Secret
                    .Where(
                        (s, i) => s != guessAndSecret.Guess[i])
                    .ToList()
        );
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
