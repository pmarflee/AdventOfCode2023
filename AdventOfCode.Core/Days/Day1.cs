namespace AdventOfCode.Core.Days;

public class Day1 : IDay
{
    static readonly Dictionary<string, char> Digits = new()
    {
        ["one"] = '1',
        ["two"] = '2',
        ["three"] = '3',
        ["four"] = '4',
        ["five"] = '5',
        ["six"] = '6',
        ["seven"] = '7',
        ["eight"] = '8',
        ["nine"] = '9',
    };

    public static string SolvePart1(string input)
    {
        return Solve(input, 1);
    }

    public static string SolvePart2(string input)
    {
        return Solve(input, 2);
    }

    static string Solve(string input, int part)
    {
        return input.SplitLines()
            .Select(line => GetCalibrationValue(line, part))
            .Sum()
            .ToString();
    }

    static long GetCalibrationValue(string line, int part)
    {
        static long ConvertToNumber(char c) => (long)char.GetNumericValue(c);

        var digits = GetDigits(line, part);

        return (ConvertToNumber(digits[0]) * 10) + ConvertToNumber(digits[^1]);
    }

    public static List<char> GetDigits(string line, int part)
    {
        var chars = line.AsSpan();
        List<char> digits = [];

        for (var start = 0; start < chars.Length; start++)
        {
            if (char.IsAsciiDigit(chars[start]))
            {
                digits.Add(chars[start]);
            }
            else if (part == 2)
            {
                foreach (var pair in Digits)
                {
                    if (chars[start..].StartsWith(pair.Key))
                    {
                        digits.Add(pair.Value);
                        break;
                    }
                }
            }
        }

        return digits;
    }
}
