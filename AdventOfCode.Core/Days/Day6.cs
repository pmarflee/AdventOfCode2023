namespace AdventOfCode.Core.Days;

public class Day6 : IDay
{
    public record Race(long Time, long RecordDistance)
    {
        public long GetDistance(long seconds) => seconds * (Time - seconds);

        public long GetNumberOfWaysToBeatRecord()
        {
            long min = 0;
            long max = Time;

            while (min <= max)
            {
                long middle = (max + min) / 2;
                long distanceCurrent = GetDistance(middle);

                if (distanceCurrent > RecordDistance)
                {
                    long distancePrevious = GetDistance(middle - 1);
                    
                    if (distancePrevious <= RecordDistance)
                    {
                        return Time - (middle + middle) + 1;
                    }

                    max = middle - 1;
                }
                else
                {
                    min = middle + 1;
                }
            }

            throw new Exception("Unable to calculate result");
        }
    }

    public static string SolvePart1(string input)
    {
        return ParseInputPart1(input)
            .Select(race => race.GetNumberOfWaysToBeatRecord())
            .Aggregate((acc, count) => acc * count)
            .ToString();
    }

    public static string SolvePart2(string input)
    {
        return ParseInputPart2(input)
            .GetNumberOfWaysToBeatRecord()
            .ToString();
    }

    public static List<Race> ParseInputPart1(string input) => Parser.ParsePart1(input);

    public static Race ParseInputPart2(string input) => Parser.ParsePart2(input);

    class Parser
    {
        static readonly Parser<List<Race>> InputPart1;
        static readonly Parser<Race> InputPart2;

        static Parser()
        {
            var number = OneOrMany(Terms.Pattern(char.IsAsciiDigit))
                .Then(p => long.Parse(string.Concat(p.SelectMany(x => x.Span.ToArray()))));
            var numbers = OneOrMany(Terms.Integer());
            var timeLabel = Terms.Text("Time:");
            var times = timeLabel.SkipAnd(numbers);
            var time = timeLabel.SkipAnd(number);
            var distanceLabel = Terms.Text("Distance:");
            var distances = distanceLabel.SkipAnd(numbers);
            var distance = distanceLabel.SkipAnd(number);

            InputPart1 = times.And(distances)
                .Then(p => p.Item1.Zip(p.Item2, (t, d) => new Race((int)t, (int)d)).ToList());

            InputPart2 = time.And(distance).Then(p => new Race(p.Item1, p.Item2));
        }

        public static List<Race> ParsePart1(string input) => InputPart1.Parse(input);

        public static Race ParsePart2(string input) => InputPart2.Parse(input);
    }
}
