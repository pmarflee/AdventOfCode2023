namespace AdventOfCode.Tests.Days;

public class Day3TestFixture
{
    private static readonly string[] Lines =
    [
        "467..114..",
        "...*......",
        "..35..633.",
        "......#...",
        "617*......",
        ".....+.58.",
        "..592.....",
        "......755.",
        "...$.*....",
        ".664.598.."
    ];

    [Fact]
    public void TestPart1()
    {
        Assert.Equal("4361", Day3.SolvePart1(Lines.JoinLines()));
    }
}
