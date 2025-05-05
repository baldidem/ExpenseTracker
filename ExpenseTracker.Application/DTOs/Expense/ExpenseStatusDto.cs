using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.DTOs.Expense
{
    public class ExpenseStatusDto
    {
        public ExpenseStatus NewStatus { get; set; }
        public string? RejectionReason { get; set; }
    }
}
