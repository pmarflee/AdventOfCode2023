namespace AdventOfCode.Core.Days;

public class Day1 : IDay
{
    public static string SolvePart1(string input)
    {
        return input.SplitLines()
            .Select(GetCalibrationValue)
            .Sum()
            .ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    static long GetCalibrationValue(string line)
    {
        static long ConvertToNumber(char c) => (long)char.GetNumericValue(c);

        char? firstDigit = null;
        char? lastDigit = null;

        foreach (char c in line)
        {
            if (!char.IsDigit(c)) continue;

            firstDigit ??= c;
            lastDigit = c;
        }

        return (ConvertToNumber(firstDigit!.Value) * 10) + ConvertToNumber(lastDigit!.Value);
    }
}
