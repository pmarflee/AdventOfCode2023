using static AdventOfCode.Core.Days.Day6;

namespace AdventOfCode.Tests.Days;

public class Day6TestFixture
{
    private const string InputString =
        $"""
        Time:      7  15   30
        Distance:  9  40  200
        """;

    private static readonly List<Race> InputPart1 =
    [
        new(7, 9),
        new(15, 40),
        new(30, 200)
    ];

    [Fact]
    public void TestPart1Parser()
    {
        var input = ParseInputPart1(InputString);

        Assert.Equivalent(InputPart1, input);
    }

    [Fact]
    public void TestPart2Parser()
    {
        var input = ParseInputPart2(InputString);

        Assert.Equivalent(new Race(71530, 940200), input);
    }

    [Fact]
    public void TestPart1()
    {
        Assert.Equal("288", SolvePart1(InputString));
    }

    [Fact]
    public void TestPart2()
    {
        Assert.Equal("71503", SolvePart2(InputString));
    }
}
