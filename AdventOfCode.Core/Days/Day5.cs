namespace AdventOfCode.Core.Days;

public class Day5 : IDay
{
    public static string SolvePart1(string input)
    {
        return ParseInput(input)
            .GetLowestLocation()
            .ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    public static Almanac ParseInput(string input) => Parser.Parse(input);

    public record Seed(long Number);

    public record Line(long SourceRangeStart, long DestinationRangeStart, long RangeLength)
    {
        public long SourceRangeEnd = SourceRangeStart + RangeLength - 1;
    };

    public record Map(string Name, List<Line> Lines)
    {
        public long Convert(long number)
        {
            var line = Lines.FirstOrDefault(l => 
                l.SourceRangeStart <= number && 
                l.SourceRangeEnd >= number);

            return line != null 
                ? line.DestinationRangeStart + (number - line.SourceRangeStart) 
                : number;
        }
    }

    public record Almanac(List<Seed> Seeds, List<Map> Maps)
    {
        public long GetLowestLocation()
        {
            return Seeds.Select(seed => Maps.Aggregate(seed.Number, (number, map) => map.Convert(number)))
                        .Min();
        }
    }

    class Parser
    {
        public static readonly Parser<Almanac> Input;

        static Parser()
        {
            var seeds = Terms.Text("seeds:").SkipAnd(OneOrMany(Terms.Integer().Then(p => new Seed(p))));
            var name = Literals.Pattern(c => char.IsLetter(c) || c == ' ' || c == '-')
                .Then(id => id.Span.ToString())
                .AndSkip(Terms.Char(':'));
            var line = Terms.Integer()
                .And(Terms.Integer())
                .And(Terms.Integer())
                .Then(p => new Line(p.Item2, p.Item1, p.Item3));
            var map = name.And(OneOrMany(line))
                .Then(p => new Map(p.Item1, [.. p.Item2.OrderBy(l => l.SourceRangeStart)]));

            Input = seeds.AndSkip(Literals.WhiteSpace(true)).And(Separated(Literals.WhiteSpace(true), map)).Then(p => new Almanac(p.Item1, p.Item2));
        }

        public static Almanac Parse(string input) => Input.Parse(input);
    }
}
