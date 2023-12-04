namespace AdventOfCode.Core.Days;

public class Day4 : IDay
{
    public static string SolvePart1(string input)
    {
        return ParseCards(input).Sum(card => card.Score()).ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    public record Card(long Number, HashSet<long> WinningNumbers, List<long> MyNumbers)
    {
        public long Score()
        {
            var count = WinningNumbers.Intersect(MyNumbers).Count();

            return count == 0 ? 0 : (long)Math.Pow(2, count - 1);
        }
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
                .Then(p => new Card(p.Item1, new(p.Item2), p.Item3));

            Card = card;
        }

        public static Card Parse(string input) => Card.Parse(input);
    }
}
