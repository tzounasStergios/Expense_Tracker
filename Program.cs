using System;
using System.Text;

class Program
{
    public static async Task Main(string[] args)
    {
        // Needed so the box drawing characters of the menu show correctly
        Console.OutputEncoding = Encoding.UTF8;

        // An instance to the Expense Manager Object
        // Easily Manage Expenses
        ExpenseManager manager = new ExpenseManager();

        // Temporary Variables
        int tmpId;
        string tmpName;
        decimal tmpAmount;
        string tmpCategoryName;
        bool loop = true;

        while (loop)
        {
            ConsoleUI.ShowMenu();
            try
            {
                int option = Convert.ToInt32(ConsoleUI.Prompt("Option (1 - 10)"));
                switch (option)
                {
                    case 1:
                        ConsoleUI.Section("1. Add Expense");
                        tmpId = Convert.ToInt32(ConsoleUI.Prompt("Give Expense ID"));
                        tmpName = ConsoleUI.Prompt("Give Expense Name");
                        tmpAmount = Convert.ToDecimal(ConsoleUI.Prompt("Give Expense Amount"));
                        tmpCategoryName = ConsoleUI.Prompt("Give Expense Category");
                        Category category = new Category(name: tmpCategoryName);
                        Expense expense = new Expense(tmpId, tmpName, tmpAmount, category);
                        manager.WriteExpenseToList(expense);
                        break;
                    case 2:
                        ConsoleUI.Section("2. Print Expense List");
                        manager.PrintExpenses();
                        break;
                    case 3:
                        ConsoleUI.Section("3. Search Expense By ID");
                        tmpId = Convert.ToInt32(ConsoleUI.Prompt("Give Expense ID"));
                        manager.FindExpenseByID(tmpId);
                        break;
                    case 4:
                        ConsoleUI.Section("4. Remove Expense By ID");
                        tmpId = Convert.ToInt32(ConsoleUI.Prompt("Give Expense ID"));
                        manager.RemoveExpenseByID(tmpId);
                        break;
                    case 5:
                        ConsoleUI.Section("5. View Expense List By Category");
                        tmpCategoryName = ConsoleUI.Prompt("Give Category Name");
                        manager.FindExpensesByCategory(tmpCategoryName);
                        break;
                    case 6:
                        ConsoleUI.Section("6. Find MAX Expense");
                        manager.FindMaxAmountExpense();
                        break;
                    case 7:
                        ConsoleUI.Section("7. Find MIN Expense");
                        manager.FindMinAmountExpense();
                        break;
                    case 8:
                        ConsoleUI.Section("8. Save Expense List To JSON");
                        await manager.SaveExpensesInJsonAsync();
                        break;
                    case 9:
                        ConsoleUI.Section("9. Load Expense List From JSON");
                        await manager.LoadExpensesFromJsonAsync();
                        break;
                    case 10:
                        ConsoleUI.Section("10. Exit");
                        loop = false;
                        break;
                    default:
                        ConsoleUI.Warning("Please choose a number between 1 and 10.");
                        break;
                }
            }
            catch (FormatException ex)
            {
                ConsoleUI.Error($"Invalid Format : {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                ConsoleUI.Error($"Invalid File Exception : {ex.Message}");
            }
            catch (Exception ex)
            {
                ConsoleUI.Error($"General Exception : {ex.Message}");
            }
        }
        ConsoleUI.Info("Console App Termination !!");
        Console.ReadKey();
    }
}
