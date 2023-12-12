using static AdventOfCode.Core.Days.Day8;

namespace AdventOfCode.Tests.Days;

public class Day8TestFixture
{
    static readonly string[] InputStrings =
    [
        $"""
        RL

        AAA = (BBB, CCC)
        BBB = (DDD, EEE)
        CCC = (ZZZ, GGG)
        DDD = (DDD, DDD)
        EEE = (EEE, EEE)
        GGG = (GGG, GGG)
        ZZZ = (ZZZ, ZZZ)
        """,
        $"""
        LLR

        AAA = (BBB, BBB)
        BBB = (AAA, ZZZ)
        ZZZ = (ZZZ, ZZZ)
        """
    ];

    static readonly Documents[] Inputs =
    [
        new([Instruction.Right, Instruction.Left],
            new Dictionary<string, (string, string)> 
            {
                ["AAA"] = ("BBB", "CCC"),
                ["BBB"] = ("DDD", "EEE"),
                ["CCC"] = ("ZZZ", "GGG"),
                ["DDD"] = ("DDD", "DDD"),
                ["EEE"] = ("EEE", "EEE"),
                ["GGG"] = ("GGG", "GGG"),
                ["ZZZ"] = ("ZZZ", "ZZZ")
            }),
        new([Instruction.Left, Instruction.Left, Instruction.Right],
            new Dictionary<string, (string, string)> 
            {
                ["AAA"] = ("BBB", "BBB"),
                ["BBB"] = ("AAA", "ZZZ"),
                ["ZZZ"] = ("ZZZ", "ZZZ")
            })
    ];

    [Theory]
    [MemberData(nameof(GetParserTestCases))]
    public void TestParser(string input, Documents expected)
    {
        ParseInput(input).Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GetPart1TestCases))]
    public void TestPart1(string input, string expected)
    {
        SolvePart1(input).Should().Be(expected);
    }

    [Fact]
    public void TestPart2()
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<object[]> GetParserTestCases()
    {
        return InputStrings.Zip(Inputs, (s, i) => new object[] { s, i });
    }

    public static IEnumerable<object[]> GetPart1TestCases()
    {
        return InputStrings.Zip(["2", "6"], (s, i) => new object[] { s, i });
    }
}
