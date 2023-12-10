namespace AdventOfCode.Core.Days;

public class Day6 : IDay
{
    public record Race(int Time, int Distance);

    public static string SolvePart1(string input)
    {
        return ParseInput(input)
            .Select(race => Enumerable.Range(1, race.Time - 1)
                                      .Select(seconds => seconds * (race.Time - seconds))
                                      .Count(distance => distance > race.Distance))
            .Aggregate((acc, count) => acc * count).ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    public static List<Race> ParseInput(string input) => Parser.Parse(input);

    class Parser
    {
        static readonly Parser<List<Race>> Input;

        static Parser()
        {
            var times = Terms.Text("Time:").SkipAnd(OneOrMany(Terms.Integer()));
            var distances = Terms.Text("Distance:").SkipAnd(OneOrMany(Terms.Integer()));

            Input = times.And(distances)
                .Then(p => p.Item1.Zip(p.Item2, (t, d) => new Race((int)t, (int)d)).ToList());
        }

        public static List<Race> Parse(string input) => Input.Parse(input);
    }
}
