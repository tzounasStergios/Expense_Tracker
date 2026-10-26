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
            // The Menu (printed in Cyan)
            // {"text",-42} means : write the text and fill the rest of the 42 characters with spaces
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║         WELCOME TO EXPENSE TRACKER         ║");
            Console.WriteLine("╠════════════════════════════════════════════╣");
            Console.WriteLine($"║ {"1. Add Expense",-42} ║");
            Console.WriteLine($"║ {"2. Print Expense List",-42} ║");
            Console.WriteLine($"║ {"3. Search Expense By ID",-42} ║");
            Console.WriteLine($"║ {"4. Remove Expense By ID",-42} ║");
            Console.WriteLine($"║ {"5. View Expense List By Category",-42} ║");
            Console.WriteLine($"║ {"6. Find MAX Expense",-42} ║");
            Console.WriteLine($"║ {"7. Find MIN Expense",-42} ║");
            Console.WriteLine($"║ {"8. Save Expense List To JSON",-42} ║");
            Console.WriteLine($"║ {"9. Load Expense List From JSON",-42} ║");
            Console.WriteLine($"║ {"10. Exit",-42} ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.ResetColor();
            try
            {
                Console.Write("Option (1 - 10) : ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 1. Add Expense ────────────────────────────");
                        Console.ResetColor();
                        Console.Write("Give Expense ID : ");
                        tmpId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Give Expense Name : ");
                        tmpName = Console.ReadLine();
                        Console.Write("Give Expense Amount : ");
                        tmpAmount = Convert.ToDecimal(Console.ReadLine());
                        if (tmpAmount < 0)
                            throw new NegativeAmountException("You Gave Negative Amount of Expense");
                        Console.Write("Give Expense Category : ");
                        tmpCategoryName = Console.ReadLine();
                        Category category = new Category(name: tmpCategoryName);
                        Expense expense = new Expense(tmpId, tmpName, tmpAmount, category);
                        manager.WriteExpenseToList(expense);
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 2. Print Expense List ─────────────────────");
                        Console.ResetColor();
                        manager.PrintExpenses();
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 3. Search Expense By ID ───────────────────");
                        Console.ResetColor();
                        Console.Write("Give Expense ID : ");
                        tmpId = Convert.ToInt32(Console.ReadLine());
                        manager.FindExpenseByID(tmpId);
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 4. Remove Expense By ID ───────────────────");
                        Console.ResetColor();
                        Console.Write("Give Expense ID : ");
                        tmpId = Convert.ToInt32(Console.ReadLine());
                        manager.RemoveExpenseByID(tmpId);
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 5. View Expense List By Category ──────────");
                        Console.ResetColor();
                        Console.Write("Give Category Name : ");
                        tmpCategoryName = Console.ReadLine();
                        manager.FindExpensesByCategory(tmpCategoryName);
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 6. Find MAX Expense ───────────────────────");
                        Console.ResetColor();
                        manager.FindMaxAmountExpense();
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 7. Find MIN Expense ───────────────────────");
                        Console.ResetColor();
                        manager.FindMinAmountExpense();
                        break;
                    case 8:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 8. Save Expense List To JSON ──────────────");
                        Console.ResetColor();
                        await manager.SaveExpensesInJsonAsync();
                        break;
                    case 9:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 9. Load Expense List From JSON ────────────");
                        Console.ResetColor();
                        await manager.LoadExpensesFromJsonAsync();
                        break;
                    case 10:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n── 10. Exit ──────────────────────────────────");
                        Console.ResetColor();
                        loop = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[WARN] ");
                        Console.ResetColor();
                        Console.WriteLine("Please choose a number between 1 and 10.");
                        break;
                }
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[FAIL] ");
                Console.ResetColor();
                Console.WriteLine($"Invalid Format : {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[FAIL] ");
                Console.ResetColor();
                Console.WriteLine($"Invalid File Exception : {ex.Message}");
            }
            catch (NegativeAmountException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[FAIL] ");
                Console.ResetColor();
                Console.WriteLine($"Invalid Amount : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[FAIL] ");
                Console.ResetColor();
                Console.WriteLine($"General Exception : {ex.Message}");
            }
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[INFO] ");
        Console.ResetColor();
        Console.WriteLine("Console App Termination !!");
        Console.ReadKey();
    }
}
