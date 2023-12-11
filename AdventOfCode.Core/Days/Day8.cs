namespace AdventOfCode.Core.Days;

public class Day8 : IDay
{
    public enum Instruction
    {
        Left = 1,
        Right = 2
    }

    public class Desert(List<Instruction> instructions,
                           IDictionary<string, (string, string)> network)
    {
        private readonly List<Instruction> _instructions = instructions;
        private readonly IDictionary<string, (string, string)> _network = network;

        private int _index = 0;

        public string CurrentNode { get; private set; } = "AAA";

        public int Steps { get; private set; }

        public void Navigate()
        {
            var (left, right) = _network[CurrentNode];

            CurrentNode = _instructions[_index] switch
            {
                Instruction.Left => left,
                Instruction.Right => right,
                _ => throw new NotImplementedException()
            };

            _index = (_index + 1) % _instructions.Count;

            Steps++;
        }
    }

    public static string SolvePart1(string input)
    {
        var desert = ParseInput(input);

        while (desert.CurrentNode != "ZZZ")
        {
            desert.Navigate();
        }

        return desert.Steps.ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    public static Desert ParseInput(string input) => Parser.Parse(input);

    static class Parser
    {
        static readonly Parser<Desert> Input;

        static Parser()
        {
            var leftInstruction = Literals.Char('L').Discard(Instruction.Left);
            var rightInstruction = Literals.Char('R').Discard(Instruction.Right);
            var instructions = OneOrMany(leftInstruction.Or(rightInstruction));
            var label = Terms.Pattern(char.IsAsciiLetterUpper, 3, 3).Then(p => p.Span.ToString());
            var node = label
                .AndSkip(Terms.Char('='))
                .And(Between(Terms.Char('('), Separated(Terms.Char(','), label), Terms.Char(')')));
            var network = OneOrMany(node)
                .Then(p => p.ToFrozenDictionary(n => n.Item1, n => (n.Item2[0], n.Item2[1])));

            Input = instructions
                .AndSkip(Literals.WhiteSpace(true))
                .And(network).Then(p => new Desert(p.Item1, p.Item2));
        }

        public static Desert Parse(string input) => Input.Parse(input);
    }
}
