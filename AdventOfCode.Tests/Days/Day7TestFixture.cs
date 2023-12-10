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
        new([
            CamelCards.Card.Three,
            CamelCards.Card.Two,
            CamelCards.Card.T,
            CamelCards.Card.Three,
            CamelCards.Card.K
         ], 765),
        new([
            CamelCards.Card.T,
            CamelCards.Card.Five,
            CamelCards.Card.Five,
            CamelCards.Card.J,
            CamelCards.Card.Five
         ], 684),
        new([
            CamelCards.Card.K,
            CamelCards.Card.K,
            CamelCards.Card.Six,
            CamelCards.Card.Seven,
            CamelCards.Card.Seven
         ], 28),
        new([
            CamelCards.Card.K,
            CamelCards.Card.T,
            CamelCards.Card.J,
            CamelCards.Card.J,
            CamelCards.Card.T
         ], 220),
        new([
            CamelCards.Card.Q,
            CamelCards.Card.Q,
            CamelCards.Card.Q,
            CamelCards.Card.J,
            CamelCards.Card.A
         ], 483),
    ];

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
