namespace AdventOfCode.Core.Days;

public class Day9 : IDay
{
    public static string SolvePart1(string input)
    {
        return new Part1Solver(input).Solve();
    }

    public static string SolvePart2(string input)
    {
        return new Part2Solver(input).Solve();
    }

    public static List<LinkedList<long>> Parse(string input)
    {
        var parser = OneOrMany(Terms.Integer(NumberOptions.AllowSign))
            .Then(list => new LinkedList<long>(list));

        return input.SplitLines(StringSplitOptions.RemoveEmptyEntries).Select(parser.Parse).ToList();
    }

    abstract class Solver(string input)
    {
        public string Solve()
        {
            return (from line in Parse(input)
                    let diffs = CreateDiffs(line)
                    select Solve(diffs)).Sum().ToString();
        }

        long Solve(List<LinkedList<long>> diffs)
        {
            var diff = diffs[^1];

            AddValue(diff, 0);

            for (var i = diffs.Count - 2; i >= 0; i--)
            {
                diff = diffs[i];
                var previous = diffs[i + 1];

                AddValue(diff, previous);
            }

            return GetValue(diff);
        }

        private static List<LinkedList<long>> CreateDiffs(LinkedList<long> line)
        {
            List<LinkedList<long>> diffs = [ line ];
            LinkedList<long> diff;
            var previous = line;

            do
            {
                diff = new LinkedList<long>(previous.OverlappingPairs().Select(pair => pair.Item2 - pair.Item1));

                diffs.Add(diff);

                previous = diff;
            } while (diff.Any(number => number != 0));

            return diffs;
        }

        protected abstract void AddValue(LinkedList<long> diff, long value);

        protected abstract void AddValue(LinkedList<long> diff, LinkedList<long> previous);

        protected abstract long GetValue(LinkedList<long> diff);
    }

    class Part1Solver(string input) : Solver(input)
    {
        protected override void AddValue(LinkedList<long> diff, long value)
        {
            diff.AddLast(value);
        }

        protected override void AddValue(LinkedList<long> diff, LinkedList<long> previous)
        {
            diff.AddLast(diff.Last!.Value + previous.Last!.Value);
        }

        protected override long GetValue(LinkedList<long> diff)
        {
            return diff.Last!.Value;
        }
    }

    class Part2Solver(string input) : Solver(input)
    {
        protected override void AddValue(LinkedList<long> diff, long value)
        {
            diff.AddFirst(value);
        }

        protected override void AddValue(LinkedList<long> diff, LinkedList<long> previous)
        {
            diff.AddFirst(diff.First!.Value - previous.First!.Value);
        }

        protected override long GetValue(LinkedList<long> diff)
        {
            return diff.First!.Value;
        }
    }
}
