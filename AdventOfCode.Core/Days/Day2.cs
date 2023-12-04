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

        return ParseGames(input)
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
        return ParseGames(input)
            .Select(game => new
            {
                MinRed = game.Selections.Max(s => s.TotalRed),
                MinBlue = game.Selections.Max(s => s.TotalBlue),
                MinGreen = game.Selections.Max(s => s.TotalGreen)
            })
            .Sum(s => s.MinRed * s.MinBlue * s.MinGreen)
            .ToString();
    }

    public static IEnumerable<Game> ParseGames(string input) => input.SplitLines().Select(ParseGame);

    public static Game ParseGame(string line) => Parser.Parse(line);

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

        private long Total(Colour colour) => Cubes.FirstOrDefault(c => c.Colour == colour)?.Amount ?? 0;
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
