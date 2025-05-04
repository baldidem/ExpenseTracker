using ExpenseTracker.Application.DTOs.Common;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.DTOs.Expense
{
    public class ExpenseResponseDto : BaseResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? DocumentPath { get; set; }
        public string ExpenseStatus { get; set; }
        public string? RejectionReason { get; set; }

    }
}
