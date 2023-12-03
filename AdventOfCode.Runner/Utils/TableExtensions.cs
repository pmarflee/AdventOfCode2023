using System.Diagnostics;
using AdventOfCode.Runner.Utils;
using static AdventOfCode.Core.Utils.FileReader;

namespace AdventOfCode.Runner.Utils;

internal static class TableExtensions
{
    internal static Table AddRow(this Table table, int day, int part, Func<string, string> func)
    {
        var input = Read(day);

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = func(input);

        stopwatch.Stop();

        table.AddRow(day.ToString(), part.ToString(), result, stopwatch.Elapsed.ToString());

        return table;
    }
}
