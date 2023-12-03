namespace AdventOfCode.Core.Utils;

public static class LineSplitter
{
    public static IEnumerable<string> SplitLines(this string input)
    {
        return input.Split(Environment.NewLine);
    }

    public static string JoinLines(this IEnumerable<string> input)
    {
        return string.Join(Environment.NewLine, input);
    }
}
