using System.Collections.Immutable;

namespace AdventOfCode.Core.Days;

public class Day7 : IDay
{
    public record Hand(List<CamelCards.Card> Cards, long Bid);

    public class CamelCards
    {
        public enum Card
        {
            Two = 1,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            T,
            J,
            Q,
            K,
            A
        }

        public static readonly char[] Characters = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];

        public static readonly IDictionary<char, Card> Cards = Characters
            .Zip(Enum.GetValues<Card>(), (c, card) => new { Character = c, Card = card })
            .ToImmutableDictionary(pair => pair.Character, pair => pair.Card);
    }

    public static string SolvePart1(string input)
    {
        throw new NotImplementedException();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    public static List<Hand> ParseInput(string input) => Parser.Parse(input);

    static class Parser
    {
        static readonly Parser<List<Hand>> Input;

        static Parser()
        {
            Input = OneOrMany(
                Terms.Pattern(CamelCards.Characters.Contains)
                    .Then(p => p.Span.ToArray().Select(c => CamelCards.Cards[c]).ToList())
                .And(Terms.Integer())
                .Then(p => new Hand(p.Item1, p.Item2)));
        }

        public static List<Hand> Parse(string input) => Input.Parse(input);
    }
}
