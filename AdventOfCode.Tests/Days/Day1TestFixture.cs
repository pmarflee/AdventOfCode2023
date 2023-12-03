namespace AdventOfCode.Tests.Days;

public class Day1TestFixture
{
    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void TestPart1(string input, string expected)
    {
        Assert.Equal(expected, Day1.SolvePart1(input));
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        yield return new object[]
        {
            string.Join(Environment.NewLine, [ "1abc2", "pqr3stu8vwx", "a1b2c3d4e5f", "treb7uchet" ]),
            "142"
        };
    }
}
