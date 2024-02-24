using static AdventOfCode.Core.Days.Day9;

namespace AdventOfCode.Tests.Days;

public class Day9TestFixture
{
    static readonly string Input =
        $"""
        0 3 6 9 12 15
        1 3 6 10 15 21
        10 13 16 21 30 45
        """;

    static readonly List<List<long>> Numbers =
    [
        [ 0, 3, 6, 9, 12, 15 ],
        [ 1, 3, 6, 10, 15, 21 ],
        [ 10, 13, 16, 21, 30, 45 ]
    ];

    [Fact]
    public void TestParser()
    {
        Parse(Input).Should().BeEquivalentTo(Numbers);
    }

    [Fact]
    public void TestParsingNegativeNumbers()
    {
        List<List<long>> numbers = [ [1L, 5L, -10L, 4L, -15L] ];

        Parse("1 5 -10 4 -15").Should().BeEquivalentTo(numbers);
    }

    [Fact]
    public void TestPart1()
    {
        SolvePart1(Input).Should().Be("114");
    }
}
