var table = new Table { Title = new("Advent of Code 2023") };

table.AddColumns(
    new TableColumn("Day"),
    new TableColumn("Part"),
    new TableColumn("Result"),
    new TableColumn("Time Taken"));

table.AddRow(1, 1, Day1.SolvePart1);
table.AddRow(1, 2, Day1.SolvePart2);
table.AddRow(2, 1, Day2.SolvePart1);

AnsiConsole.Write(table);