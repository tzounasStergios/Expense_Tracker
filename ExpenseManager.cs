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
        ConsoleUI.Success("Expense Successfully Written to List!!");
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
            ConsoleUI.ShowExpenses(expenses);
        }
        else
        {
            ConsoleUI.Warning("Nothing in List Expenses!!");
            return;
        }
    }

    public void FindMaxAmountExpense()
    {
        Expense? maxExpense =
        expenses.MaxBy(exp => exp.Amount);
        if (maxExpense is null)
        {
            ConsoleUI.Warning("Nothing in List Expenses!!");
            return;
        }
        ConsoleUI.Info("MAX Amount was in Expense :");
        ConsoleUI.ShowExpenses(new List<Expense> { maxExpense });
    }

    public void FindMinAmountExpense()
    {
        Expense? minExpense =
        expenses.MinBy(exp => exp.Amount);
        if (minExpense is null)
        {
            ConsoleUI.Warning("Nothing in List Expenses!!");
            return;
        }
        ConsoleUI.Info("MIN Amount was in Expense :");
        ConsoleUI.ShowExpenses(new List<Expense> { minExpense });
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
            ConsoleUI.Warning($"Given Category : {categoryName} Not found in Expense List!!");
            return;
        }
        else
        {
            ConsoleUI.ShowExpenses(categorizedExpenses);
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
            ConsoleUI.Warning($"Given ID : {id} Not found in Expense List!!");
            return;
        }
        ConsoleUI.ShowExpenses(new List<Expense> { expense });
    }

    public void RemoveExpenseByID(int id)
    {
        Expense expenseID =
            expenses.Where(exp => exp.Id == id).FirstOrDefault();

        if (expenseID is null)
        {
            ConsoleUI.Warning($"Given ID : {id} Not FOUND in Expense List!!");
            return;
        }
        else
        {
            expenses.Remove(expenseID);
            ConsoleUI.Success($"Expense : ID {expenseID.Id} ({expenseID.Name}) Succesfully REMOVED From List !!");
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
        ConsoleUI.Success("Expense List Successfully Saved To ExpensesTracker.json !!");
    }

    public async Task LoadExpensesFromJsonAsync()
    {
        string loadedJsonData = await File.ReadAllTextAsync("ExpensesTracker.json");
        List<Expense> loadedExpenses = JsonSerializer.Deserialize<List<Expense>>(loadedJsonData);
        if (loadedExpenses.Count != 0 && loadedExpenses is not null)
        {
            ConsoleUI.ShowExpenses(loadedExpenses);
        }
        else
        {
            ConsoleUI.Warning("Json File IS Empty!!");
        }
    }

}