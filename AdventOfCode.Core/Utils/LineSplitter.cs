namespace AdventOfCode.Core.Utils;

internal static class LineSplitter
{
    public static IEnumerable<string> SplitLines(this string input)
    {
        return input.Split(Environment.NewLine);
    }
}
