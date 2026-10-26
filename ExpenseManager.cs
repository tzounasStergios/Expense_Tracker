using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
public class ExpenseManager : IExpenseManager
{
    private List<Expense> expenses = new List<Expense>();

    public void WriteExpenseToList(Expense expense)
    {
        expenses.Add(expense);
        PrintMessage("[ OK ]", "Expense Successfully Written to List!!", ConsoleColor.Green);
        return;
    }

    /// <summary>
    /// Prints out Expense List details !!
    /// Search if List is Not Empty and Informs the User
    /// </summary>
    public void PrintExpenses()
    {
        if (expenses.Count != 0 && expenses != null)
        {
            PrintExpenseTable(expenses);
        }
        else
        {
            PrintMessage("[WARN]", "Nothing in List Expenses!!", ConsoleColor.Yellow);
            return;
        }
    }

    public void FindMaxAmountExpense()
    {
        Expense? maxExpense =
        expenses.MaxBy(exp => exp.Amount);
        if (maxExpense is null)
        {
            PrintMessage("[WARN]", "Nothing in List Expenses!!", ConsoleColor.Yellow);
            return;
        }
        PrintMessage("[INFO]", "MAX Amount was in Expense :", ConsoleColor.Cyan);
        PrintExpenseTable(new List<Expense> { maxExpense });
    }

    public void FindMinAmountExpense()
    {
        Expense? minExpense =
        expenses.MinBy(exp => exp.Amount);
        if (minExpense is null)
        {
            PrintMessage("[WARN]", "Nothing in List Expenses!!", ConsoleColor.Yellow);
            return;
        }
        PrintMessage("[INFO]", "MIN Amount was in Expense :", ConsoleColor.Cyan);
        PrintExpenseTable(new List<Expense> { minExpense });
    }
    /// <summary>
    /// Searches Users' Given Category Name --> Prints Expenses Details from that Category
    /// If User gave Wrong Category Name --> Prints appropriate Not Found Message
    /// </summary>
    /// <param name="categoryName"> User Category Input </param>
    public void FindExpensesByCategory(string categoryName)
    {
        List<Expense> categorizedExpenses =
            expenses.Where(exp => exp.Category.Name == categoryName).ToList();

        if (categorizedExpenses.Count == 0 || categorizedExpenses is null)
        {
            PrintMessage("[WARN]", $"Given Category : {categoryName} Not found in Expense List!!", ConsoleColor.Yellow);
            return;
        }
        else
        {
            PrintExpenseTable(categorizedExpenses);
            return;
        }
    }

    /// <summary>
    /// Tries to find an Expense from the List by ID Given from the User !!
    /// </summary>
    /// <param name="id"> User Inputs ID </param>
    public void FindExpenseByID(int id)
    {
        Expense expense =
        expenses.Where(exp => exp.Id == id).FirstOrDefault();
        if (expense is null)
        {
            PrintMessage("[WARN]", $"Given ID : {id} Not found in Expense List!!", ConsoleColor.Yellow);
            return;
        }
        PrintExpenseTable(new List<Expense> { expense });
    }

    public void RemoveExpenseByID(int id)
    {
        Expense expenseID =
            expenses.Where(exp => exp.Id == id).FirstOrDefault();

        if (expenseID is null)
        {
            PrintMessage("[WARN]", $"Given ID : {id} Not FOUND in Expense List!!", ConsoleColor.Yellow);
            return;
        }
        else
        {
            expenses.Remove(expenseID);
            PrintMessage("[ OK ]", $"Expense : ID {expenseID.Id} ({expenseID.Name}) Succesfully REMOVED From List !!", ConsoleColor.Green);
            return;
        }
    }

    /// <summary>
    /// Serializes the Expenses List into a JSON string and saves it asynchronously to a local file !!
    /// </summary>
    public async Task SaveExpensesInJsonAsync()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonData = JsonSerializer.Serialize(expenses, options);
        await File.WriteAllTextAsync("ExpensesTracker.json", jsonData);
        PrintMessage("[ OK ]", "Expense List Successfully Saved To ExpensesTracker.json !!", ConsoleColor.Green);
    }

    public async Task LoadExpensesFromJsonAsync()
    {
        string loadedJsonData = await File.ReadAllTextAsync("ExpensesTracker.json");
        List<Expense> loadedExpenses = JsonSerializer.Deserialize<List<Expense>>(loadedJsonData);
        if (loadedExpenses is not null && loadedExpenses.Count != 0)
        {
            PrintExpenseTable(loadedExpenses);
        }
        else
        {
            PrintMessage("[WARN]", "Json File IS Empty!!", ConsoleColor.Yellow);
        }
    }

    /// <summary>
    /// Prints a message with a colored tag in front of it, e.g. [ OK ] in Green
    /// </summary>
    private void PrintMessage(string tag, string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(tag + " ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    /// <summary>
    /// Prints the given expenses inside a table
    /// {value,-6} = left aligned in 6 characters, {value,12} = right aligned in 12 characters
    /// </summary>
    private void PrintExpenseTable(List<Expense> expensesToPrint)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌────────┬────────────────────────┬──────────────┬────────────────────┐");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"│ {"ID",-6} │ {"NAME",-22} │ {"AMOUNT",12} │ {"CATEGORY",-18} │");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("├────────┼────────────────────────┼──────────────┼────────────────────┤");
        Console.ResetColor();

        foreach (Expense expense in expensesToPrint)
        {
            Console.WriteLine($"│ {expense.Id,-6} │ {expense.Name,-22} │ {expense.Amount,12:F2} │ {expense.Category?.Name,-18} │");
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("└────────┴────────────────────────┴──────────────┴────────────────────┘");

        if (expensesToPrint.Count > 1)
        {
            decimal total = expensesToPrint.Sum(exp => exp.Amount);
            Console.WriteLine($"{expensesToPrint.Count} expenses | Total : {total:F2}");
        }
        Console.ResetColor();
    }

}
