using System.Diagnostics;

namespace AdventOfCode.Runner.Utils;

internal static class TableExtensions
{
    internal static Table AddRow(this Table table, int day, int part, Func<string, string> func)
    {
        var input = File.ReadAllText($"./Data/Day{day}.txt");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = func(input);

        stopwatch.Stop();

        table.AddRow(day.ToString(), part.ToString(), result, stopwatch.Elapsed.ToString());

        return table;
    }
}
