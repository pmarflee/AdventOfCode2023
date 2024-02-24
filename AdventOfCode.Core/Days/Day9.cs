namespace AdventOfCode.Core.Days;

public class Day9 : IDay
{
    public static string SolvePart1(string input)
    {
        var lines = Parse(input);
        var sum = 0L;

        foreach (var line in lines)
        {
            List<List<long>> diffs = [ line ];
            List<long> diff;
            var previous = line;

            do
            {
                diff = previous.OverlappingPairs().Select(pair => pair.Item2 - pair.Item1).ToList();

                diffs.Add(diff);

                previous = diff;
            } while (diff.Any(number => number != 0));

            diff.Add(0);

            for (var i = diffs.Count - 2; i >= 0; i--)
            {
                diff = diffs[i];
                previous = diffs[i + 1];

                diff.Add(diff[^1] + previous[^1]);
            }

            sum += diff[^1];
        }

        return sum.ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    public static List<List<long>> Parse(string input)
    {
        var parser = OneOrMany(Terms.Integer(NumberOptions.AllowSign));

        return input.SplitLines(StringSplitOptions.RemoveEmptyEntries).Select(parser.Parse).ToList();
    }
}
