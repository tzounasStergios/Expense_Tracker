public class Expense
{
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public decimal Amount { get; private set; }
    public Category? Category { get; private set; }

    public Expense(int id, string name, decimal amount, Category category)
    {
        Id = id;
        Name = name;
        Amount = amount;
        Category = category;
    }
}