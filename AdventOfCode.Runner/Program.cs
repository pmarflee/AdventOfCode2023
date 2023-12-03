var table = new Table { Title = new("Advent of Code 2023") };

table.AddColumns(
    new TableColumn("Day"),
    new TableColumn("Part"),
    new TableColumn("Result"),
    new TableColumn("Time Taken"));

table.AddRow(1, 1, Day1.SolvePart1);

AnsiConsole.Write(table);