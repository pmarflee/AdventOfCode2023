namespace AdventOfCode.Tests.Days;

using Colour = Day2.Colour;
using Game = Day2.Game;

public class Day2TestFixture
{
    private static readonly string[] Lines =
    [
        "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
        "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
        "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
        "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
        "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
    ];

    private static readonly Game[] Games =
    [
        new(1,
        [
            new([new(Colour.Blue, 3), new(Colour.Red, 4)]),
            new([new(Colour.Red, 1), new(Colour.Green, 2), new(Colour.Blue, 6)]),
            new([new(Colour.Green, 2)])
        ]),
        new(2,
        [
            new([new(Colour.Blue, 1), new(Colour.Green, 2)]),
            new([new(Colour.Green, 3), new(Colour.Blue, 4), new(Colour.Red, 1)]),
            new([new(Colour.Green, 1), new(Colour.Blue, 1)])
        ]),
        new(3,
        [
            new([new(Colour.Green, 8), new(Colour.Blue, 6), new(Colour.Red, 20)]),
            new([new(Colour.Blue, 5), new(Colour.Red, 4), new(Colour.Green, 13)]),
            new([new(Colour.Green, 5), new(Colour.Red, 1)])
        ]),
        new(4,
        [
            new([new(Colour.Green, 1), new(Colour.Red, 3), new(Colour.Blue, 6)]),
            new([new(Colour.Green, 3), new(Colour.Red, 6)]),
            new([new(Colour.Green, 3), new(Colour.Blue, 15), new(Colour.Red, 14)])
        ]),
        new(5,
        [
            new([new(Colour.Red, 6), new(Colour.Blue, 1), new(Colour.Green, 3)]),
            new([new(Colour.Blue, 2), new(Colour.Red, 1), new(Colour.Green, 2)])
        ])
    ];

    [Theory]
    [MemberData(nameof(GetParserTestCases))]
    public void TestParser(string line, Game expected)
    {
        Assert.Equivalent(expected, Day2.Parse(line));
    }

    [Fact]
    public void TestPart1()
    {
        Assert.Equal("8", Day2.SolvePart1(Lines.JoinLines()));
    }

    public static IEnumerable<object[]> GetParserTestCases()
    {
        return Lines.Zip(Games, (line, game) => new object[] { line, game });
    }
}
