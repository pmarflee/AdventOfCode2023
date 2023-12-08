﻿using System.Diagnostics;

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

stopwatch.Stop();

table.AddRow("-", "-", "-", $"[bold lime]{stopwatch.Elapsed}[/]");

AnsiConsole.Write(table);