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
        """
        ];

    static readonly Grid[] Grids =
        [
            new Grid(
            [
                [ Ground.Instance, Ground.Instance, Ground.Instance, Ground.Instance, Ground.Instance ],
                [ Ground.Instance, Start.Instance, HorizontalPipe.Instance, BendSouthWest.Instance, Ground.Instance ],
                [ Ground.Instance, VerticalPipe.Instance, Ground.Instance, VerticalPipe.Instance, Ground.Instance ],
                [ Ground.Instance, BendNorthEast.Instance, HorizontalPipe.Instance, BendNorthWest.Instance, Ground.Instance ],
                [ Ground.Instance, Ground.Instance, Ground.Instance, Ground.Instance, Ground.Instance ]
            ])
        ];

    [Theory]
    [MemberData(nameof(GetParserTestCases))]
    public void TestParser(string input, Grid grid)
    {
        Parse(input).Lines.Should().BeEquivalentTo(grid.Lines);
    }

    public static IEnumerable<object[]> GetParserTestCases()
    {
        return InputStrings.Zip(Grids, (s, i) => new object[] { s, i });
    }
}
