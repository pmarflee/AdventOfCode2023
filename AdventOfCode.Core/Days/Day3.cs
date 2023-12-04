namespace AdventOfCode.Core.Days;

public class Day3 : IDay
{
    public static string SolvePart1(string input)
    {
        return CreateSchematic(input).GetPartNumbers().Sum().ToString();
    }

    public static string SolvePart2(string input)
    {
        var schematic = CreateSchematic(input);

        return (from number in schematic.Numbers
                from symbol in number.GetAdjacentSymbols(schematic.Symbols)
                where symbol.Value == '*'
                group number by symbol into grouped
                where grouped.Count() == 2
                select grouped.First().Value * grouped.Last().Value).Sum().ToString();
    }

    public static EngineSchematic CreateSchematic(string input) => new(input.SplitLines());

    public class Number(long row, Range columns, long value)
    {
        private readonly HashSet<(long, long)> AdjacentLocations = new(GetAdjacentLocations(row, columns));

        public long Value { get; } = value;

        public bool IsPartNumber(IEnumerable<Symbol> symbols)
        {
            return GetAdjacentSymbols(symbols).Any();
        }

        public IEnumerable<Symbol> GetAdjacentSymbols(IEnumerable<Symbol> symbols)
        {
            return symbols.Where(symbol => AdjacentLocations.Contains((symbol.Row, symbol.Column)));
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

    public record Symbol(char Value, long Row, long Column);

    public class EngineSchematic
    {
        private readonly List<Number> _numbers = [];
        private readonly List<Symbol> _symbols = [];

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
                        _numbers.Add(new(row, digitColumns[0]..digitColumns[^1], value.Value));

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
                            _symbols.Add(new(c, row, column));
                        }
                    }
                }

                TryAddNumber();
            }
        }

        public IEnumerable<long> GetPartNumbers() => from number in _numbers
                                                     where number.IsPartNumber(_symbols)
                                                     select number.Value;

        public IEnumerable<Symbol> Symbols => _symbols;
        public IEnumerable<Number> Numbers => _numbers;
    }
}
