using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.DTOs.Report
{
    public class ExpenseReportDto
    {
        public int ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; } = Currency.TRY;
        public ExpenseStatus ExpenseStatus { get; set; }
        public string ExpenseCategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? RejectionReason { get; set; }
        public int UserId { get; set; }
    }
}
