using static AdventOfCode.Core.Days.Day8;

namespace AdventOfCode.Tests.Days;

public class Day8TestFixture
{
    const string InputString =
        $"""
        RL

        AAA = (BBB, CCC)
        BBB = (DDD, EEE)
        CCC = (ZZZ, GGG)
        DDD = (DDD, DDD)
        EEE = (EEE, EEE)
        GGG = (GGG, GGG)
        ZZZ = (ZZZ, ZZZ)
        """;

    static readonly Documents Input = new(
        [Instruction.Right, Instruction.Left],
        new Dictionary<string, (string, string)> 
        {
            ["AAA"] = ("BBB", "CCC"),
            ["BBB"] = ("DDD", "EEE"),
            ["CCC"] = ("ZZZ", "GGG"),
            ["DDD"] = ("DDD", "DDD"),
            ["EEE"] = ("EEE", "EEE"),
            ["GGG"] = ("GGG", "GGG"),
            ["ZZZ"] = ("ZZZ", "ZZZ")
        });

    [Fact]
    public void TestParser()
    {
        ParseInput(InputString).Should().BeEquivalentTo(Input);
    }

    [Fact]
    public void TestPart1()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void TestPart2()
    {
        throw new NotImplementedException();
    }
}
