namespace AdventOfCode.Core.Utils;

public static class LineSplitter
{
    public static List<string> SplitLines(this string input, StringSplitOptions options = StringSplitOptions.None)
    {
        return [.. input.Split(Environment.NewLine, options)];
    }

    public static string JoinLines(this IEnumerable<string> input)
    {
        return string.Join(Environment.NewLine, input);
    }
}
