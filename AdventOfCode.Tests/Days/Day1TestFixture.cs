namespace AdventOfCode.Tests.Days;

public class Day1TestFixture
{
    [Theory]
    [MemberData(nameof(GetPart1TestCases))]
    public void TestPart1(string input, string expected)
    {
        Assert.Equal(expected, Day1.SolvePart1(input));
    }

    [Theory]
    [MemberData(nameof(GetPart2TestCases))]
    public void TestPart2(string input, string expected)
    {
        Assert.Equal(expected, Day1.SolvePart2(input));
    }

    [Theory]
    [MemberData(nameof(GetDigitsTestCases))]
    public void TestGetDigits(string line, IEnumerable<char> expected)
    {
        Assert.Equal(expected, Day1.GetDigits(line, 2));
    }

    public static IEnumerable<object[]> GetPart1TestCases()
    {
        yield return new object[]
        {
            new[] {
                "1abc2",
                "pqr3stu8vwx",
                "a1b2c3d4e5f",
                "treb7uchet"
            }.JoinLines(),
            "142"
        };
    }

    public static IEnumerable<object[]> GetPart2TestCases()
    {
        yield return new object[]
        {
            new[] {
                "two1nine",
                "eightwothree",
                "abcone2threexyz",
                "xtwone3four",
                "4nineeightseven2",
                "zoneight234",
                "7pqrstsixteen"
            }.JoinLines(),
            "281"
        };
    }

    public static IEnumerable<object[]> GetDigitsTestCases()
    {
        return [
            ["gsjgklneight6zqfz", new[] { '8', '6' }],
            ["two2geight", new[] {'2', '2', '8'}],
            ["g4", new[] {'4'}],
            ["threeninedtr7219", new[] { '3', '9', '7', '2', '1', '9' }],
            ["tmoneightzstdjqjncnkpkknzoneonethreefive7", new[] { '1', '8', '1', '1', '3', '5', '7' }]
        ];
    }
}
