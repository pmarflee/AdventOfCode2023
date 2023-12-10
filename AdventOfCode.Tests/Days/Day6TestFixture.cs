using static AdventOfCode.Core.Days.Day6;

namespace AdventOfCode.Tests.Days;

public class Day6TestFixture
{
    private const string InputString =
        $"""
        Time:      7  15   30
        Distance:  9  40  200
        """;

    private static readonly List<Race> Input =
    [
        new(7, 9),
        new(15, 40),
        new(30, 200)
    ];

    [Fact]
    public void TestParser()
    {
        var input = ParseInput(InputString);

        Assert.Equivalent(Input, input);
    }

    [Fact]
    public void TestPart1()
    {
        Assert.Equal("288", SolvePart1(InputString));
    }

    [Fact]
    public void TestPart2()
    {
        throw new NotImplementedException();
    }
}
