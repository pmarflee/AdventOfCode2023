using System.Reflection;

namespace AdventOfCode.Core.Utils;

public static class FileReader
{
    public static string Read(int day)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"{assembly.GetName().Name}.Data.Day{day}.txt";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream!);

        return reader.ReadToEnd();
    }
}
