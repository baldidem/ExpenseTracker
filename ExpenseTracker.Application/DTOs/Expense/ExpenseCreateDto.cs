using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.DTOs.Expense
{
    public class ExpenseCreateDto
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string? DocumentPath { get; set; }
        //public string? RejectionReason { get; set; }
        //public Currency Currency { get; set; }
    }
}
