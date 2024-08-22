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

    public record Hand(List<Card> Cards, long Bid)
    {
        public override string ToString()
        {
            return string.Concat(Cards.Select(c => CardsToCharacters[c]));
        }
    }

    public static string SolvePart1(string input)
    {
        return Solve(input, new Part1TypeGrouper(), Comparer<Card>.Default);
    }

    public static string SolvePart2(string input)
    {
        return Solve(input, new Part2TypeGrouper(), new SecondOrderingRuleComparer());
    }

    static string Solve(string input, ITypeGrouper typeGrouper, IComparer<Card> secondOrderingRuleComparer)
    {
        return ParseInput(input)
            .Order(new HandComparer(typeGrouper, secondOrderingRuleComparer))
            .Select((hand, i) => hand.Bid * (i + 1))
            .Sum()
            .ToString();
    }

    public static IReadOnlyList<Hand> ParseInput(string input) => Parser.Parse(input);

    class HandComparer(ITypeGrouper typeGrouper, IComparer<Card> secondOrderingRuleComparer) : Comparer<Hand>
    {
        private readonly ITypeGrouper _typeGrouper = typeGrouper;
        private readonly IComparer<Card> _secondOrderingRuleComparer = secondOrderingRuleComparer;

        private readonly Dictionary<Hand, Type> _handTypes = [];

        public override int Compare(Hand? x, Hand? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            var xType = GetType(x);
            var yType = GetType(y);

            if (xType > yType) return 1;
            if (xType < yType) return -1;

            return x.Cards.Zip(y.Cards, (c, o) => new { Card = c, Other = o })
                .Select(pair => _secondOrderingRuleComparer.Compare(pair.Card, pair.Other))
                .FirstOrDefault(v => v != 0);
        }

        private Type GetType(Hand hand)
        {
            if (_handTypes.TryGetValue(hand, out var cachedType)) return cachedType;

            var set = _typeGrouper.Group(hand.Cards);
            var (_, type) = Scorers.First(scorer => scorer.Item1(set));

            _handTypes.TryAdd(hand, type);

            return type;
        }
    }

    interface ITypeGrouper
    {
        List<int> Group(List<Card> cards);
    }

    class Part1TypeGrouper : ITypeGrouper
    {
        public List<int> Group(List<Card> cards)
        {
            return [.. cards
                .GroupBy(c => c)
                .Select(g => g.Count())
                .OrderByDescending(c => c)];
        }
    }

    class Part2TypeGrouper : ITypeGrouper
    {
        public List<int> Group(List<Card> cards)
        {
            var groups = cards
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());

            if (groups.TryGetValue(Card.J, out var jokers))
            {
                var itemToUpdate = groups
                    .Where(kv => kv.Key != Card.J)
                    .OrderByDescending(kv => kv.Value)
                    .FirstOrDefault();

                if (itemToUpdate.Key != default)
                {
                    groups[itemToUpdate.Key] = groups[itemToUpdate.Key] + jokers;
                    groups.Remove(Card.J);
                }
            }

            return groups
                .OrderByDescending(kv => kv.Value)
                .Select(kv => kv.Value)
                .ToList();
        }
    }

    class SecondOrderingRuleComparer : Comparer<Card>
    {
        public override int Compare(Card x, Card y)
        {
            return (x, y) switch
            {
                var (x1, y1) when x1 == y1 => 0,
                (Card.J, _) => -1,
                (_, Card.J) => 1,
                var (x1, y1) => x1.CompareTo(y1)
            };
        }
    }

    static class Parser
    {
        static readonly Parser<IReadOnlyList<Hand>> Input;

        static Parser()
        {
            Input = OneOrMany(
                Terms.Pattern(Characters.Contains)
                    .Then(p => p.Span.ToArray().Select(c => CharactersToCards[c]).ToList())
                .And(Terms.Integer())
                .Then(p => new Hand(p.Item1, p.Item2)));
        }

        public static IReadOnlyList<Hand> Parse(string input) => Input.Parse(input)!;
    }
}
