using System.Net.Sockets;

namespace AdventOfCode.Core.Days;

public class Day5 : IDay
{
    public static string SolvePart1(string input)
    {
        var almanac = ParseInput(input);

        return almanac.SeedNumbers
            .Select(seed => almanac.Maps.Aggregate(seed, (number, map) =>
            {
                var line = map.Lines.FirstOrDefault(l =>
                    l.SourceRangeStart <= number &&
                    l.SourceRangeEnd >= number);

                return line != default ? line.Convert(number) : number;
            }))
            .Min()
            .ToString();
    }

    public static string SolvePart2(string input)
    {
        var almanac = ParseInput(input);

        return almanac.SeedNumbers
            .Pairwise()
            .SelectMany(pair => almanac.Maps.Aggregate(
                new List<(long, long)> { pair },
                (pairs, map) =>
                {
                    List<(long, long)> newPairs = [];

                    for (var i = 0; i < pairs.Count; i++)
                    {
                        var (start, count) = pairs[i];
                        var matched = false;
                        var end = start + count - 1;

                        foreach (var line in map.Lines)
                        {
                            if (line.SourceRangeStart <= end && line.SourceRangeEnd >= start) // ranges overlap
                            {
                                var maxStart = Math.Max(start, line.SourceRangeStart);
                                var minEnd = Math.Min(end, line.SourceRangeEnd);
                                var newStart = line.Convert(maxStart);
                                var newCount = minEnd - maxStart + 1;

                                newPairs.Add((newStart, newCount));

                                if (newCount != count)
                                {
                                    pairs.Add((maxStart > start ? start : start + newCount, count - newCount));
                                }

                                matched = true;

                                break;
                            }
                        }

                        if (!matched)
                        {
                            newPairs.Add((start, count));
                        }
                    }

                    return newPairs;
                }))
            .MinBy(pair => pair.Item1)
            .Item1.ToString();
    }

    public static Almanac ParseInput(string input) => Parser.Parse(input);

    public record struct Line(long SourceRangeStart, long DestinationRangeStart, long RangeLength)
    {
        public long SourceRangeEnd = SourceRangeStart + RangeLength - 1;
        public readonly long Convert(long number) => number + DestinationRangeStart - SourceRangeStart;
    };

    public record Map(string Name, List<Line> Lines);

    public record Almanac(List<long> SeedNumbers, List<Map> Maps);

    class Parser
    {
        public static readonly Parser<Almanac> Input;

        static Parser()
        {
            var seeds = Terms.Text("seeds:").SkipAnd(OneOrMany(Terms.Integer()));
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
