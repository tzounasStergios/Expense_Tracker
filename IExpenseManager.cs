public interface IExpenseManager
{
    void WriteExpenseToList(Expense expense);
    void PrintExpenses();
    void RemoveExpenseByID(int id);
    void FindMaxAmountExpense();
    void FindMinAmountExpense();
    void FindExpensesByCategory(string categoryName);
    void FindExpenseByID(int id);
    Task SaveExpensesInJsonAsync();
    Task LoadExpensesFromJsonAsync();
}