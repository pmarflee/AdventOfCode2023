namespace AdventOfCode.Core.Types;

internal class Digits
{
    private readonly List<char> _digits = [];

    public void Add(char digit)
    {
        if (!char.IsAsciiDigit(digit))
        {
            throw new ArgumentException("Character must be a digit", nameof(digit));
        }

        _digits.Add(digit);
    }

    public long Value
    {
        get
        {
            long value = 0;

            for (var i = 1; i <= _digits.Count; i++)
            {
                value += (long)char.GetNumericValue(_digits[^i]) * (long)Math.Pow(10, i - 1);
            }

            return value;
        }
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
