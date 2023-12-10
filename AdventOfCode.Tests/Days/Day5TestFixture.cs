using static AdventOfCode.Core.Days.Day5;

namespace AdventOfCode.Tests.Days;

public class Day5TestFixture
{
    private const string InputString =
        $"""
        seeds: 79 14 55 13

        seed-to-soil map:
        50 98 2
        52 50 48

        soil-to-fertilizer map:
        0 15 37
        37 52 2
        39 0 15

        fertilizer-to-water map:
        49 53 8
        0 11 42
        42 0 7
        57 7 4

        water-to-light map:
        88 18 7
        18 25 70

        light-to-temperature map:
        45 77 23
        81 45 19
        68 64 13

        temperature-to-humidity map:
        0 69 1
        1 0 69

        humidity-to-location map:
        60 56 37
        56 93 4
        """;

    private static readonly Almanac Input = new(
        [79, 14, 55, 13],
        [
            new Map(
                "seed-to-soil map",
                [
                    new Line(98, 50, 2),
                    new Line(50, 52, 48)
                ]),
            new Map(
                "soil-to-fertilizer map",
                [
                    new Line(15, 0, 37),
                    new Line(52, 37, 2),
                    new Line(0, 39, 15)
                ]),
            new Map(
                "fertilizer-to-water map",
                [
                    new Line(53, 49, 8),
                    new Line(11, 0, 42),
                    new Line(0, 42, 7),
                    new Line(7, 57, 4)
                ]),
            new Map(
                "water-to-light map",
                [
                    new Line(18, 88, 7),
                    new Line(25, 18, 70)
                ]),
            new Map(
                "light-to-temperature map",
                [
                    new Line(77, 45, 23),
                    new Line(45, 81, 19),
                    new Line(64, 68, 13)
                ]),
            new Map(
                "temperature-to-humidity map",
                [
                    new Line(69, 0, 1),
                    new Line(0, 1, 69)
                ]),
            new Map(
                "humidity-to-location map",
                [
                    new Line(56, 60, 37),
                    new Line(93, 56, 4)
                ])
        ]);

    [Fact]
    public void TestParser()
    {
        var input = ParseInput(InputString);

        Assert.Equivalent(Input, input);
    }

    [Fact]
    public void TestPart1()
    {
        Assert.Equal("35", SolvePart1(InputString));
    }

    [Fact]
    public void TestPart2()
    {
        Assert.Equal("46", SolvePart2(InputString));
    }
}
