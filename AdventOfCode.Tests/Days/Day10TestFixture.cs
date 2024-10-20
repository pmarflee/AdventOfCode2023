using static AdventOfCode.Core.Days.Day10;

namespace AdventOfCode.Tests.Days;

public class Day10TestFixture
{
    static readonly string[] InputStrings =
        [
        $"""
        .....
        .F-7.
        .|.|.
        .L-J.
        .....
        """,
        $"""
        ..F7.
        .FJ|.
        SJ.L7
        |F--J
        LJ...
        """
        ];

    public static IReadOnlyList<IReadOnlyList<TileType>> TestCase1 =
    [
        [ TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground ],
        [ TileType.Ground, TileType.BendSouthEast, TileType.HorizontalPipe, TileType.BendSouthWest, TileType.Ground ],
        [ TileType.Ground, TileType.VerticalPipe, TileType.Ground, TileType.VerticalPipe, TileType.Ground ],
        [ TileType.Ground, TileType.BendNorthEast, TileType.HorizontalPipe, TileType.BendNorthWest, TileType.Ground ],
        [ TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground ],
    ];

    public static IReadOnlyList<IReadOnlyList<TileType>> TestCase2 =
    [
        [ TileType.Ground, TileType.Ground, TileType.BendSouthEast, TileType.BendSouthWest, TileType.Ground ],
        [ TileType.Ground, TileType.BendSouthEast, TileType.BendNorthWest, TileType.VerticalPipe, TileType.Ground ],
        [ TileType.Start, TileType.BendNorthWest, TileType.Ground, TileType.BendNorthEast, TileType.BendSouthWest ],
        [ TileType.VerticalPipe, TileType.BendSouthEast, TileType.HorizontalPipe, TileType.HorizontalPipe, TileType.BendNorthWest ],
        [ TileType.BendNorthEast, TileType.BendNorthWest, TileType.Ground, TileType.Ground, TileType.Ground ]
    ];

    [Theory]
    [MemberData(nameof(GetParserTestCases))]
    public void TestParser(string input, IReadOnlyList<IReadOnlyList<TileType>> tiles)
    {
        Parse(input).Should().BeEquivalentTo(tiles);
    }

    [Fact]
    public void ShouldSolvePart1()
    {
        var grid = new Grid(TestCase2);
        var solver = new Part1Solver(grid);

        solver.Solve().Should().Be(8);
    }

    public static IEnumerable<object[]> GetParserTestCases()
    {
        return InputStrings.Zip([ TestCase1, TestCase2 ], (s, i) => new object[] { s, i });
    }
}
