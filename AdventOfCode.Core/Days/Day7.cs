namespace AdventOfCode.Core.Days;

public class Day7 : IDay
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

    public enum Type
    {
        HighCard = 1,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    static readonly char[] Characters = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];

    static readonly Dictionary<char, Card> CharactersToCards;
    static readonly Dictionary<Card, char> CardsToCharacters;

    static Day7()
    {
        var cardsAndCharacters = Characters
            .Zip(Enum.GetValues<Card>(), (c, card) => new { Character = c, Card = card })
            .ToList();

        CharactersToCards = cardsAndCharacters.ToDictionary(pair => pair.Character, pair => pair.Card);
        CardsToCharacters = cardsAndCharacters.ToDictionary(pair => pair.Card, pair => pair.Character);
    }

    static bool FiveOfAKind(List<int> counts) => NOfAKind(counts, [5]);

    static bool FourOfAKind(List<int> counts) => NOfAKind(counts, [4, 1]);

    static bool FullHouse(List<int> counts) => NOfAKind(counts, [3, 2]);

    static bool ThreeOfAKind(List<int> counts) => NOfAKind(counts, [3, 1, 1]);

    static bool TwoPair(List<int> counts) => NOfAKind(counts, [2, 2, 1]);

    static bool OnePair(List<int> counts) => NOfAKind(counts, [2, 1, 1, 1]);

    static bool HighCard(List<int> counts) => NOfAKind(counts, [1, 1, 1, 1, 1]);

    static bool NOfAKind(List<int> counts, int[] expected) => counts.SequenceEqual(expected);

    static readonly (Func<List<int>, bool>, Type)[] Scorers =
    [
        (FiveOfAKind, Type.FiveOfAKind),
        (FourOfAKind, Type.FourOfAKind),
        (FullHouse, Type.FullHouse),
        (ThreeOfAKind, Type.ThreeOfAKind),
        (TwoPair, Type.TwoPair),
        (OnePair, Type.OnePair),
        (HighCard, Type.HighCard)
    ];

    public record Hand(List<Card> Cards, long Bid) : IComparable<Hand>
    {
        Type Type
        {
            get
            {
                var set = Cards
                    .GroupBy(c => c)
                    .Select(g => g.Count())
                    .OrderByDescending(c => c)
                    .ToList();

                var (_, type) = Scorers.First(scorer => scorer.Item1(set));

                return type;
            }
        }

        public int CompareTo(Hand? other)
        {
            if (other == null) return 1;

            var type = Type;
            var otherType = other.Type;

            if (type > otherType) return 1;
            if (type < otherType) return -1;

            return Cards.Zip(other.Cards, (c, o) => new { Card = c, Other = o })
                .Select(pair => pair.Card.CompareTo(pair.Other))
                .FirstOrDefault(v => v != 0);
        }

        public override string ToString()
        {
            return string.Concat(Cards.Select(c => CardsToCharacters[c]));
        }
    }

    public static string SolvePart1(string input)
    {
        var hands = ParseInput(input);

        hands.Sort();

        return hands.Select((hand, i) => hand.Bid * (i + 1)).Sum().ToString();
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
                Terms.Pattern(Characters.Contains)
                    .Then(p => p.Span.ToArray().Select(c => CharactersToCards[c]).ToList())
                .And(Terms.Integer())
                .Then(p => new Hand(p.Item1, p.Item2)));
        }

        public static List<Hand> Parse(string input) => Input.Parse(input);
    }
}
