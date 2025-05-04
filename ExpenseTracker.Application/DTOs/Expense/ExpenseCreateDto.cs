using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.DTOs.Expense
{
    public class ExpenseCreateDto
    {
        //public int UserId { get; set; } // Bunu ben otomatik dolduracagim.
        public decimal Amount { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string? DocumentPath { get; set; }
    }
}
