using System.Diagnostics;

var table = new Table { Title = new("Advent of Code 2023") };

table.AddColumns(
    new TableColumn("Day"),
    new TableColumn("Part"),
    new TableColumn("Result"),
    new TableColumn("Time Taken"));

var stopwatch = new Stopwatch();
stopwatch.Start();

table.AddRow(1, 1, Day1.SolvePart1);
table.AddRow(1, 2, Day1.SolvePart2);
table.AddRow(2, 1, Day2.SolvePart1);
table.AddRow(2, 2, Day2.SolvePart2);

stopwatch.Stop();

table.AddRow("-", "-", "-", $"[bold lime]{stopwatch.Elapsed}[/]");

AnsiConsole.Write(table);