# Mastermind

Currently, this contains an `Evaluator` class with a variety of implementations of a "guess evaluator" for the game Mastermind (with numbers representing the colored pegs of a physical version). Given a **secret** sequence of integers, a **guess** sequence of the same length is evaluated for correctness compared to the secret. The method returns a string containing two values: the number of _strong_ and _weak_ guess elements. A **strong guess** is the correct number at the correct position in the sequence; a **weak guess** is a number present in the secret, but in a different position in the secret than in the guess.

It is maybe the wrong direction to move when I should be working on a bunch of small exercises, but I would like to implement a playable Mastermind game. I think that would lead me to a "best" solution ~~in a way that the constraints of my first exposure obsured~~.

I am looking at [an online version of the game](https://webgamesonline.com/mastermind/index.php) and it is only giving me a "strong" and "weak" count in the same way my evaluator currently does. LOL...I can also view the code of the online version, it is plain PHP. Interestingly, they use "bulls" and "cows" rather than "strong" and "weak"...Wikipedia has the explanation for that variable name choice!

> It resembles an earlier pencil and paper game called Bulls and Cows that may date back a century.

The implementation I played in my browser doesn't feel like a great example (aside from the opportunity to play the game and get a sense of what I'm making), lots of nested looping.

## How to Run

Since this is a class library (first one I've made!), the only thing to run are the tests. They should work with `dotnet test` from the command line, VS Code's C# extension's inline buttons for test running, and Visual Studio's Test Explorer.

### Dependencies

I have `dotnet` and `dotnet-sdk` 6 installed via Chocolatey, which is likely necessary for running from the command line or in Visual Studio Code. I think Visual Studio will prompt about installing those things if they're not present, and maybe use its own version. I'm not 100% sure about the previous statement about Visual Studio because I installed it after I already had VS Code set up.

## Implementation Notes

### Scaffolding projects and solution

While I work on this sort of stuff often, it is rarely scaffolded from scratch.

#### Guides

- [Unit Testing with DotNet Test](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)

- [Testing with CLI](https://docs.microsoft.com/en-us/dotnet/core/tutorials/testing-with-cli)

- [Tutorial: Test a .NET class library using Visual Studio Code](https://docs.microsoft.com/en-us/dotnet/core/tutorials/testing-library-with-visual-studio-code?pivots=dotnet-6-0)

### Testing multiple implementations

I am certain there is a better way to achieve this (similarly shaped to the Rules pattern), but for the time being it was simplest to label all my tests `Theory` and pass in the method names of the various implementations. Then I use `.Invoke` to call them on my SUT.

- [Stack Overflow Post that led me down this path](https://stackoverflow.com/a/3254840)

### ProduceReferenceAssembly issue

After following the various guides above, I was able to run my tests in Visual Studio and with the command line. However, the inline "Run Test"/"Debug Test"/"Run All Tests" buttons in VS Code couldn't run the tests without errors.

The solution ended up being adding `<ProduceReferenceAssembly>false</ProduceReferenceAssembly>` to both `.csproj` files. Not sure why it was an issue, or how I avoided it last time I tried following those guides.

- [Stack Overflow Post that gave me that line](https://stackoverflow.com/a/67940310)

### Testing Multiple Implementations, Take 2

I think the funk of my first solution indicates I am not using inheritence in a situation that suites it. Rather than have one class full of uniquely named methods that all represent the same action, I should have a bunch of child classes with different overrides for the same method.

### Table Flip

Not sure why I was obsessing over testing my known-to-be-bad past implementations. I finally got all the tests passing by breaking the problem down into small steps! And then slowly reducing the complexity. It's nice and functional now!
