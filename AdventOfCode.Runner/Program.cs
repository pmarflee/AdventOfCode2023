using System.Diagnostics;

var table = new Table { Title = new("[underline]Advent of Code 2023[/]") };

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
table.AddRow(3, 1, Day3.SolvePart1);
table.AddRow(3, 2, Day3.SolvePart2);
table.AddRow(4, 1, Day4.SolvePart1);
table.AddRow(4, 2, Day4.SolvePart2);
table.AddRow(5, 1, Day5.SolvePart1);
table.AddRow(5, 2, Day5.SolvePart2);
table.AddRow(6, 1, Day6.SolvePart1);
table.AddRow(6, 2, Day6.SolvePart2);
table.AddRow(7, 1, Day7.SolvePart1);
table.AddRow(7, 2, Day7.SolvePart2);
table.AddRow(8, 1, Day8.SolvePart1);
table.AddRow(9, 1, Day9.SolvePart1);
table.AddRow(9, 2, Day9.SolvePart2);
table.AddRow(10, 1, Day10.SolvePart1);

stopwatch.Stop();

table.AddRow("-", "-", "-", $"[bold lime]{stopwatch.Elapsed}[/]");

AnsiConsole.Write(table);