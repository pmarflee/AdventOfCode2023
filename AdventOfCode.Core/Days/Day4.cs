namespace AdventOfCode.Core.Days;

public class Day4 : IDay
{
    public static string SolvePart1(string input)
    {
        return ParseCards(input).Sum(card => card.Score).ToString();
    }

    public static string SolvePart2(string input)
    {
        var cards = ParseCards(input)
            .Select(card => new[] { card.CountOfMatches, 1 })
            .ToArray();
        var count = 0;

        for (var i = 0; i < cards.Length; i++)
        {
            var cardCount = cards[i][1];

            for (var j = 0; j < cardCount; j++)
            {
                for (var k = 1; k <= cards[i][0]; k++)
                {
                    cards[i + k][1] = cards[i + k][1] + 1;
                }
            }

            count += cards[i][1];
        }

        return count.ToString();
    }

    public readonly struct Card
    {
        public Card(int number, HashSet<int> winningNumbers, List<int> myNumbers)
        {
            Number = number;
            CountOfMatches = winningNumbers.Intersect(myNumbers).Count();
            Score = CountOfMatches == 0 ? 0 : (int)Math.Pow(2, CountOfMatches - 1);
        }

        public int Number { get; }
        public int CountOfMatches { get; }
        public int Score { get; }
    }

    public static IEnumerable<Card> ParseCards(string input) => input.SplitLines().Select(Parser.Parse);

    static class Parser
    {
        public static readonly Parser<Card> Card;

        static Parser()
        {
            var numbers = Separated(Literals.WhiteSpace(), Terms.Integer());
            var card = Terms.Text("Card")
                .SkipAnd(Terms.Integer())
                .AndSkip(Terms.Text(":"))
                .And(numbers)
                .AndSkip(Terms.Text("|"))
                .And(numbers)
                .Then(p => new Card(
                    (int)p.Item1, 
                    new(p.Item2.Select(Convert.ToInt32)), 
                    p.Item3.Select(Convert.ToInt32).ToList()));

            Card = card;
        }

        public static Card Parse(string input) => Card.Parse(input);
    }
}
