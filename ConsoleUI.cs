public static class ConsoleUI
{
    private const int MenuWidth = 44;

    private static readonly string[] MenuItems =
    {
        "1. Add Expense",
        "2. Print Expense List",
        "3. Search Expense By ID",
        "4. Remove Expense By ID",
        "5. View Expense List By Category",
        "6. Find MAX Expense",
        "7. Find MIN Expense",
        "8. Save Expense List To JSON",
        "9. Load Expense List From JSON",
        "10. Exit"
    };

    public static void ShowMenu()
    {
        Console.WriteLine();
        WriteLineColored("╔" + new string('═', MenuWidth) + "╗", ConsoleColor.Cyan);
        WriteLineColored("║" + CenterText("WELCOME TO EXPENSE TRACKER", MenuWidth) + "║", ConsoleColor.Cyan);
        WriteLineColored("╠" + new string('═', MenuWidth) + "╣", ConsoleColor.Cyan);

        foreach (string item in MenuItems)
        {
            WriteColored("║", ConsoleColor.Cyan);
            Console.Write(" " + item.PadRight(MenuWidth - 1));
            WriteLineColored("║", ConsoleColor.Cyan);
        }

        WriteLineColored("╚" + new string('═', MenuWidth) + "╝", ConsoleColor.Cyan);
    }

    public static void Section(string title)
    {
        Console.WriteLine();
        int fillLength = Math.Max(0, MenuWidth + 2 - title.Length - 4);
        WriteLineColored($"── {title} " + new string('─', fillLength), ConsoleColor.Yellow);
    }

    public static string? Prompt(string label)
    {
        WriteColored($"{label} : ", ConsoleColor.Cyan);
        return Console.ReadLine();
    }

    public static void Success(string message) => WriteTagged("[ OK ]", message, ConsoleColor.Green);

    public static void Info(string message) => WriteTagged("[INFO]", message, ConsoleColor.Cyan);

    public static void Warning(string message) => WriteTagged("[WARN]", message, ConsoleColor.Yellow);

    public static void Error(string message) => WriteTagged("[FAIL]", message, ConsoleColor.Red);

    public static void ShowExpenses(IReadOnlyCollection<Expense> expenses)
    {
        string[] headers = { "ID", "Name", "Amount", "Category" };

        List<string[]> rows = expenses
            .Select(e => new[] { $"{e.Id}", e.Name ?? "", $"{e.Amount:F2}", e.Category?.Name ?? "" })
            .ToList();

        int[] widths = headers
            .Select((header, i) => Math.Max(header.Length, rows.Count == 0 ? 0 : rows.Max(row => row[i].Length)))
            .ToArray();

        WriteLineColored(BorderLine('┌', '┬', '┐', widths), ConsoleColor.DarkGray);
        WriteTableRow(headers, widths, ConsoleColor.Yellow);
        WriteLineColored(BorderLine('├', '┼', '┤', widths), ConsoleColor.DarkGray);
        foreach (string[] row in rows)
        {
            WriteTableRow(row, widths, ConsoleColor.White);
        }
        WriteLineColored(BorderLine('└', '┴', '┘', widths), ConsoleColor.DarkGray);

        if (expenses.Count > 1)
        {
            decimal total = expenses.Sum(e => e.Amount ?? 0m);
            WriteLineColored($"{expenses.Count} expenses | Total : {total:F2}", ConsoleColor.DarkGray);
        }
    }

    private static string BorderLine(char left, char middle, char right, int[] widths)
    {
        return left + string.Join(middle, widths.Select(w => new string('─', w + 2))) + right;
    }

    private static void WriteTableRow(string[] cells, int[] widths, ConsoleColor cellColor)
    {
        WriteColored("│", ConsoleColor.DarkGray);
        for (int i = 0; i < cells.Length; i++)
        {
            // Amount column (index 2) is right aligned, the rest are left aligned
            string text = i == 2 ? cells[i].PadLeft(widths[i]) : cells[i].PadRight(widths[i]);
            WriteColored($" {text} ", cellColor);
            WriteColored("│", ConsoleColor.DarkGray);
        }
        Console.WriteLine();
    }

    private static string CenterText(string text, int width)
    {
        int leftPadding = (width - text.Length) / 2;
        return text.PadLeft(leftPadding + text.Length).PadRight(width);
    }

    private static void WriteTagged(string tag, string message, ConsoleColor color)
    {
        WriteColored(tag + " ", color);
        Console.WriteLine(message);
    }

    private static void WriteColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    private static void WriteLineColored(string text, ConsoleColor color)
    {
        WriteColored(text, color);
        Console.WriteLine();
    }
}
