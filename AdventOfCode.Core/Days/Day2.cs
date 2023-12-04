namespace AdventOfCode.Core.Days;

public class Day2 : IDay
{
    public static string SolvePart1(string input)
    {
        Selection expected = new(
        [
            new Cubes(Colour.Red, 12),
            new Cubes(Colour.Green, 13),
            new Cubes(Colour.Blue, 14)
        ]);

        return input.SplitLines()
            .Select(Parse)
            .Where(game => game.Selections.All(
                selection =>
                    selection.TotalRed <= expected.TotalRed &&
                    selection.TotalBlue <= expected.TotalBlue &&
                    selection.TotalGreen <= expected.TotalGreen)
            )
            .Sum(game => game.Number)
            .ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    public static Game Parse(string line) => Parser.Parse(line);

    public enum Colour
    {
        Red = 1,
        Green = 2,
        Blue = 3
    }

    public record Cubes(Colour Colour, long Amount);

    public record Selection(List<Cubes> Cubes)
    {
        public long TotalRed => Total(Colour.Red);
        public long TotalBlue => Total(Colour.Blue);
        public long TotalGreen => Total(Colour.Green);

        private long Total(Colour colour) => Cubes.Where(c => c.Colour == colour).Sum(c => c.Amount);
    }

    public record Game(long Number, List<Selection> Selections);

    static class Parser
    {
        private static readonly Parser<Game> Game;

        static Parser()
        {
            var cubes = Terms.Integer()
                .And(Terms.Text("red").Or(Terms.Text("blue")).Or(Terms.Text("green"))
                .Then(p => Enum.Parse<Colour>(p, true)))
                .Then(p => new Cubes(p.Item2, p.Item1));
            var selection = Separated(Terms.Text(","), cubes).Then(p => new Selection(p));
            var selections = Separated(Terms.Text(";"), selection);
            var game = Terms.Text("Game")
                .SkipAnd(Terms.Integer())
                .AndSkip(Terms.Text(":"))
                .And(selections)
                .Then(p => new Game(p.Item1, p.Item2));

            Game = game;
        }

        public static Game Parse(string input) => Game.Parse(input);
    }
}
