namespace AdventOfCode.Core.Days;

public class Day8 : IDay
{
    public enum Instruction
    {
        Left = 1,
        Right = 2
    }

    public record Documents(IList<Instruction> Instructions,
                            IDictionary<string, (string, string)> Network);

    abstract class Navigator(Documents documents)
    {
        protected readonly Documents Documents = documents;
        protected int Index { get; private set; }

        public long Steps { get; protected set; }

        protected static string GetNextNode(Instruction instruction, string left, string right)
        {
            return instruction switch
            {
                Instruction.Left => left,
                Instruction.Right => right,
                _ => throw new NotImplementedException()
            };
        }

        public void Update()
        {
            Navigate();

            Index = (Index + 1) % Documents.Instructions.Count;
            Steps++;
        }

        protected abstract void Navigate();

        public abstract bool HasReachedDestination { get; }
    }

    class Camel(Documents documents) : Navigator(documents)
    {
        private string _location = "AAA";

        public override bool HasReachedDestination => _location == "ZZZ";

        protected override void Navigate()
        {
            var (left, right) = Documents.Network[_location];

            _location = GetNextNode(Documents.Instructions[Index], left, right); 
        }
    }

    class Ghost(Documents documents) : Navigator(documents)
    {
        private readonly string[] _locations = documents.Network.Keys
                .Where(key => key.EndsWith('A'))
                .ToArray();

        public override bool HasReachedDestination => _locations.All(location => location.EndsWith('Z'));

        protected override void Navigate()
        {
            var instruction = Documents.Instructions[Index];

            for (int i = 0; i < _locations.Length; i++)
            {
                var (left, right) = Documents.Network[_locations[i]];

                _locations[i] = GetNextNode(instruction, left, right);
            }
        }
    }

    public static string SolvePart1(string input) => Solve(input, 1);

    public static string SolvePart2(string input) => Solve(input, 2);

    static string Solve(string input, int part)
    {
        var documents = ParseInput(input);
        var navigator = part switch
        {
            1 => (Navigator)new Camel(documents),
            2 => new Ghost(documents),
            _ => throw new NotImplementedException()
        };

        do
        {
            navigator.Update();
        }
        while (!navigator.HasReachedDestination);

        return navigator.Steps.ToString();
    }

    public static Documents ParseInput(string input) => Parser.Parse(input);

    static class Parser
    {
        static readonly Parser<Documents> Input;

        static Parser()
        {
            var leftInstruction = Literals.Char('L').Discard(Instruction.Left);
            var rightInstruction = Literals.Char('R').Discard(Instruction.Right);
            var instructions = OneOrMany(leftInstruction.Or(rightInstruction));
            var label = Terms.Pattern(char.IsAsciiLetterOrDigit, 3, 3).Then(p => p.Span.ToString());
            var node = label
                .AndSkip(Terms.Char('='))
                .And(Between(Terms.Char('('), Separated(Terms.Char(','), label), Terms.Char(')')));
            var network = OneOrMany(node)
                .Then(p => p.ToFrozenDictionary(n => n.Item1, n => (n.Item2[0], n.Item2[1])));

            Input = instructions
                .AndSkip(Literals.WhiteSpace(true))
                .And(network).Then(p => new Documents(p.Item1, p.Item2));
        }

        public static Documents Parse(string input) => Input.Parse(input);
    }
}
