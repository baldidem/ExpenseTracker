namespace ExpenseTracker.Application.DTOs.Expense
{
    public class ExpenseUpdateDto
    {
        public decimal Amount { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string? DocumentPath { get; set; }
    }
}
