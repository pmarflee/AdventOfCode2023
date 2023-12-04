namespace AdventOfCode.Core.Days;

public class Day2 : IDay
{
    public static string SolvePart1(string input)
    {
        throw new NotImplementedException();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    private static List<Game> Parse(IEnumerable<string> lines)
    {
        return lines.Select(Parse).ToList();
    }

    public static Game Parse(string line) => Parser.Parse(line);

    public enum Colour
    {
        Red = 1,
        Green = 2,
        Blue = 3
    }

    public record Cubes(Colour Colour, long Amount);

    public record Game(long Number, List<List<Cubes>> Selections);

    static class Parser
    {
        private static readonly Parser<Game> Game;

        static Parser()
        {
            var cubes = Terms.Integer()
                .And(Terms.Text("red").Or(Terms.Text("blue")).Or(Terms.Text("green"))
                .Then(p => Enum.Parse<Colour>(p, true)))
                .Then(p => new Cubes(p.Item2, p.Item1));
            var selection = Separated(Terms.Text(","), cubes);
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
