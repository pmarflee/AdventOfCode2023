namespace AdventOfCode.Core.Days;

public class Day10 : IDay
{
    [Flags]
    public enum Direction
    {
        North = 1,
        East = 2,
        South = 4,
        West = 8
    }

    public abstract class Tile;

    public sealed class Ground : Tile
    {
        Ground() { }

        public static readonly Ground Instance = new();
    }

    public abstract class Pipe(Direction direction) : Tile
    {
        public Direction Direction { get; } = direction;
    }

    public sealed class VerticalPipe : Pipe
    {
        VerticalPipe() : base(Direction.North | Direction.South) { }

        public static readonly VerticalPipe Instance = new();
    }

    public sealed class HorizontalPipe : Pipe
    {
        HorizontalPipe() : base(Direction.East | Direction.West) { }

        public static readonly HorizontalPipe Instance = new();
    }

    public sealed class BendNorthEast : Pipe
    {
        BendNorthEast() : base(Direction.North | Direction.East) { }

        public static readonly BendNorthEast Instance = new();
    }

    public sealed class BendNorthWest : Pipe
    {
        BendNorthWest() : base(Direction.North | Direction.West) { }

        public static readonly BendNorthWest Instance = new();
    }

    public sealed class BendSouthEast : Pipe
    {
        BendSouthEast() : base(Direction.South | Direction.East) { }

        public static readonly BendSouthEast Instance = new();
    }

    public sealed class BendSouthWest : Pipe
    {
        BendSouthWest() : base(Direction.South | Direction.West) { }

        public static readonly BendSouthWest Instance = new();
    }

    public sealed class Start : Pipe
    {
        Start() : base(Direction.North | Direction.East | Direction.South | Direction.West) { }

        public static readonly Start Instance = new();
    }

    public class Grid(List<List<Tile>> lines)
    {
        public List<List<Tile>> Lines => lines;

        public Tile this[int x, int y]
        {
            get
            {
                return lines[y][x];
            }
        }
    }

    public static string SolvePart1(string input)
    {
        throw new NotImplementedException();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    public static Grid Parse(string input) => Parser.Grid.Parse(input);

    static class Parser
    {
        public static readonly Parser<Grid> Grid;

        static Parser()
        {
            var verticalPipe = Literals.Char('|').Then(_ => (Tile)VerticalPipe.Instance);
            var horizontalPipe = Literals.Char('-').Then(_ => (Tile)HorizontalPipe.Instance);
            var bendNorthEast = Literals.Char('L').Then(_ => (Tile)BendNorthEast.Instance);
            var bendNorthWest = Literals.Char('J').Then(_ => (Tile)BendNorthWest.Instance);
            var bendSouthWest = Literals.Char('7').Then(_ => (Tile)BendSouthWest.Instance);
            var bendSouthEast = Literals.Char('F').Then(_ => (Tile)BendSouthEast.Instance);
            var ground = Literals.Char('.').Then(_ => (Tile)Ground.Instance);
            var start = Literals.Char('S').Then(_ => (Tile)Start.Instance);
            var tile = OneOf(verticalPipe,
                             horizontalPipe,
                             bendNorthEast,
                             bendNorthWest,
                             bendSouthWest,
                             bendSouthEast,
                             ground,
                             start);
            var tiles = OneOrMany(tile);

            Grid = Separated(Literals.Text(Environment.NewLine), tiles).Then(lines => new Grid(lines));
        }
    }
}
