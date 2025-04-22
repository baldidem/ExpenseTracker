namespace ExpenseTracker.Domain.Entities
{
    public class ExpenseCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
