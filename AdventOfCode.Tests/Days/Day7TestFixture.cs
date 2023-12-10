using static AdventOfCode.Core.Days.Day7;

namespace AdventOfCode.Tests.Days;

public class Day7TestFixture
{
    private const string InputString =
        $"""
        32T3K 765
        T55J5 684
        KK677 28
        KTJJT 220
        QQQJA 483
        """;

    private readonly List<Hand> Input =
    [
        new([ Card.Three, Card.Two, Card.T, Card.Three, Card.K ], 765),
        new([ Card.T, Card.Five, Card.Five, Card.J, Card.Five ], 684),
        new([ Card.K, Card.K, Card.Six, Card.Seven, Card.Seven ], 28),
        new([ Card.K, Card.T, Card.J, Card.J, Card.T ], 220),
        new([ Card.Q, Card.Q, Card.Q, Card.J, Card.A ], 483)
    ];

    [Fact]
    public void TestParser()
    {
        ParseInput(InputString).Should().BeEquivalentTo(Input);
    }

    [Fact]
    public void TestPart1()
    {
        SolvePart1(InputString).Should().Be("6440");
    }

    [Fact]
    public void TestPart2()
    {
        throw new NotImplementedException();
    }
}
