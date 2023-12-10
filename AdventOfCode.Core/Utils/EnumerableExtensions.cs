using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Core.Utils;

internal static class EnumerableExtensions
{
    public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> values)
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
