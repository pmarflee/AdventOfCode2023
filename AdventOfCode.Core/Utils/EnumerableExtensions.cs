namespace AdventOfCode.Core.Utils;

internal static class EnumerableExtensions
{
    public static IEnumerable<(T, T)> OverlappingPairs<T>(this IEnumerable<T> values)
    {
        var enumerator = values.GetEnumerator();
        T? previous = default;
        var i = 0;

        while (enumerator.MoveNext())
        {
            if (i++ > 0)
            {
                yield return (previous!, enumerator.Current);
            }

            previous = enumerator.Current;
        }
    }

    public static IEnumerable<(T, T)> NonOverlappingPairs<T>(this IEnumerable<T> values)
    {
        var enumerator = values.GetEnumerator();

        while (true)
        {
            if (!enumerator.MoveNext()) yield break;

            var previous = enumerator.Current;

            if (!enumerator.MoveNext()) yield break;

            yield return (previous, enumerator.Current);
        }
    }
}
