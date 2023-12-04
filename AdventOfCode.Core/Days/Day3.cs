namespace AdventOfCode.Core.Days;

public class Day3 : IDay
{
    public static string SolvePart1(string input)
    {
        return new EngineSchematic(input.SplitLines()).PartNumbers.Sum().ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    class Number(long row, Range columns, long value)
    {
        private readonly HashSet<(long, long)> AdjacentLocations = new(GetAdjacentLocations(row, columns));

        public long Value { get; } = value;

        public bool IsPartNumber(List<(long, long)> symbolLocations)
        {
            return symbolLocations.Any(AdjacentLocations.Contains);
        }

        private static IEnumerable<(long, long)> GetAdjacentLocations(long row, Range columns)
        {
            IEnumerable<(long, long)> GetLocations(long row)
            {
                for (var column = columns.Start.Value - 1; column <= columns.End.Value + 1; column++)
                {
                    yield return (row, column);
                }
            }

            foreach (var location in GetLocations(row - 1))
            {
                yield return location;
            }

            yield return (row, columns.Start.Value - 1);
            yield return (row, columns.End.Value + 1);

            foreach (var location in GetLocations(row + 1))
            {
                yield return location;
            }
        }
    }

    public class EngineSchematic
    {
        private readonly List<Number> Numbers = [];
        private readonly List<(long, long)> SymbolLocations = [];

        public EngineSchematic(List<string> lines)
        {
            for (var row = 0; row < lines.Count; row++)
            {
                Digits? digits = null;
                List<int> digitColumns = [];

                void TryAddNumber()
                {
                    var value = digits?.Value;

                    if (value.HasValue)
                    {
                        Numbers.Add(new(row, digitColumns[0]..digitColumns[^1], value.Value));

                        digits = null;
                        digitColumns.Clear();
                    }
                }

                for (var column = 0; column < lines[row].Length; column++)
                {
                    var c = lines[row][column];

                    if (char.IsAsciiDigit(c))
                    {
                        (digits ??= new()).Add(c);
                        digitColumns.Add(column);
                    }
                    else
                    {
                        TryAddNumber();

                        if (c != '.')
                        {
                            SymbolLocations.Add((row, column));
                        }
                    }
                }

                TryAddNumber();
            }
        }

        public IEnumerable<long> PartNumbers =>
            from number in Numbers
            where number.IsPartNumber(SymbolLocations)
            select number.Value;
    }
}
