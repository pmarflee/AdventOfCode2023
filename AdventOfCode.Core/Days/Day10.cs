namespace AdventOfCode.Core.Days;

public class Day10 : IDay
{
    public readonly record struct Direction(DirectionType Type, int RowOffset, int ColumnOffset, DirectionType Opposite)
    {
        public bool IsOppositeOf(Direction other) => Directions[Opposite] == other;

        public static readonly Direction None = new();
        public static readonly Direction North = new(DirectionType.North, -1, 0, DirectionType.South);
        public static readonly Direction South = new(DirectionType.South, 1, 0, DirectionType.North);
        public static readonly Direction East = new(DirectionType.East, 0, 1, DirectionType.West);
        public static readonly Direction West = new(DirectionType.West, 0, -1, DirectionType.East);
    }

    static readonly IDictionary<DirectionType, Direction> Directions =
        new[] { Direction.North, Direction.South, Direction.East, Direction.West }
            .ToDictionary(x => x.Type, x => x)
            .AsReadOnly();

    public enum TileType
    {
        Start = 1,
        Ground,
        HorizontalPipe,
        VerticalPipe,
        BendNorthEast,
        BendNorthWest,
        BendSouthEast,
        BendSouthWest
    }

    public enum DirectionType
    {
        North = 1,
        East = 2,
        South = 4,
        West = 8
    }

    public abstract class Tile
    {
        public abstract ICollection<Direction> Exits { get; }
        public virtual bool CanConnect(Direction direction) => false;
    }

    public sealed class Ground : Tile
    {
        public override ICollection<Direction> Exits => [];

        public static readonly Ground Instance = new();
    }

    public sealed class Pipe(TileType type, ICollection<Direction> exits) : Tile
    {
        public TileType Type { get; } = type;
        public override ICollection<Direction> Exits { get; } = exits;

        public Direction GetNextDirection(Direction current) =>
            Exits.First(next => next != current);

        public override bool CanConnect(Direction direction) =>
            Exits.Any(exit => Directions[exit.Opposite] == direction);

        public static readonly Pipe Horizontal = new(TileType.HorizontalPipe, [Direction.East, Direction.West]);
        public static readonly Pipe Vertical = new(TileType.VerticalPipe, [Direction.North, Direction.South]);
        public static readonly Pipe BendNorthEast = new(TileType.BendNorthEast, [Direction.North, Direction.East]);
        public static readonly Pipe BendNorthWest = new(TileType.BendNorthWest, [Direction.North, Direction.West]);
        public static readonly Pipe BendSouthEast = new(TileType.BendSouthEast, [Direction.South, Direction.East]);
        public static readonly Pipe BendSouthWest = new(TileType.BendSouthWest, [Direction.South, Direction.West]);
        public static readonly Pipe Start = new(TileType.Start, [Direction.North, Direction.South, Direction.East, Direction.West]);
    }

    public class TileInstance(int row, int column, Tile type)
    {
        public int Row { get; } = row;
        public int Column { get; } = column;
        public Tile Type { get; } = type;
        public IEnumerable<Direction> Exits => Type.Exits;
        public bool CanConnect(Direction direction) => Type.CanConnect(direction);
    }

    public class Grid
    {
        private readonly TileInstance[,] _tiles;

        public Grid(IReadOnlyList<IReadOnlyList<TileType>> lines)
        {
            _tiles = new TileInstance[lines.Count, lines[0].Count];

            for (var row = 0; row < lines.Count; row++)
            {
                for (var column = 0; column < lines[row].Count; column++)
                {
                    var tile = _tiles[row, column] = CreateTileInstance(
                        lines[row][column], row, column);

                    if (tile.Type == Pipe.Start)
                    {
                        Start = Start == null
                            ? tile
                            : throw new ArgumentException(
                                "Grid must only have one start position",
                                nameof(lines));
                    }
                }
            }

            Rows = _tiles.Length;
            Columns = _tiles.GetLength(1);

            if (Start == null)
            {
                throw new InvalidOperationException("Grid must have a start position");
            }
        }

        static TileInstance CreateTileInstance(TileType type, int row, int column) => type switch
        {
            TileType.Start => new TileInstance(row, column, Pipe.Start),
            TileType.Ground => new TileInstance(row, column, Ground.Instance),
            TileType.HorizontalPipe => new TileInstance(row, column, Pipe.Horizontal),
            TileType.VerticalPipe => new TileInstance(row, column, Pipe.Vertical),
            TileType.BendNorthEast => new TileInstance(row, column, Pipe.BendNorthEast),
            TileType.BendNorthWest => new TileInstance(row, column, Pipe.BendNorthWest),
            TileType.BendSouthEast => new TileInstance(row, column, Pipe.BendSouthEast),
            TileType.BendSouthWest => new TileInstance(row, column, Pipe.BendSouthWest),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        public TileInstance Start { get; }
        public int Rows { get; }
        public int Columns { get; }

        public TileInstance? this[int row, int column] =>
            row < 0 || row > Rows - 1 || column < 0 || column > Columns - 1
                ? null
                : _tiles[row, column];
    }

    public static string SolvePart1(string input)
    {
        var grid = new Grid(Parse(input));

        return new Part1Solver(grid).Solve().ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    public static IReadOnlyList<IReadOnlyList<TileType>> Parse(string input) => 
        Parser.Tiles.Parse(input)!;

    public abstract class Solver(Grid grid)
    {
        protected Grid Grid { get; } = grid;
        protected TileInstance Current { get; set; } = grid.Start;
        protected Direction Direction { get; set; } = Direction.None;
        public int Count { get; private set; }

        protected void MoveNext()
        {
            var result = (from direction in Current.Exits
                          where !direction.IsOppositeOf(Direction)
                          let nextRow = Current.Row + direction.RowOffset
                          let nextColumn = Current.Column + direction.ColumnOffset
                          let next = Grid[nextRow, nextColumn]
                          where next?.CanConnect(direction) ?? false
                          select new { Tile = next, Direction = direction }).First();

            Current = result.Tile;
            Direction = result.Direction;
            Count++;
        }
    }

    public class Part1Solver(Grid grid) : Solver(grid)
    {
        public int Solve()
        {
            do
            {
                MoveNext();
            } while (Current != Grid.Start);

            return Count / 2;
        }
    }

    static class Parser
    {
        public static readonly Parser<IReadOnlyList<IReadOnlyList<TileType>>> Tiles;

        static Parser()
        {
            var verticalPipe = Literals.Char('|').Then(_ => TileType.VerticalPipe);
            var horizontalPipe = Literals.Char('-').Then(_ => TileType.HorizontalPipe);
            var bendNorthEast = Literals.Char('L').Then(_ => TileType.BendNorthEast);
            var bendNorthWest = Literals.Char('J').Then(_ => TileType.BendNorthWest);
            var bendSouthWest = Literals.Char('7').Then(_ => TileType.BendSouthWest);
            var bendSouthEast = Literals.Char('F').Then(_ => TileType.BendSouthEast);
            var ground = Literals.Char('.').Then(_ => TileType.Ground);
            var start = Literals.Char('S').Then(_ => TileType.Start);
            var tile = OneOf(verticalPipe,
                             horizontalPipe,
                             bendNorthEast,
                             bendNorthWest,
                             bendSouthWest,
                             bendSouthEast,
                             ground,
                             start);
            var tiles = OneOrMany(tile);

            Tiles = Separated(Literals.Text(Environment.NewLine), tiles)!;
        }
    }
}
