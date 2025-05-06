using ExpenseTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTOs.Report
{
    public class ExpenseApprovalStatusReportDto
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string ExpenseCategoryName { get; set; }
        public ExpenseStatus ExpenseStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ExpenseApprovalStatusReportResponseDto
    {
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public List<ExpenseApprovalStatusReportDto> Report { get; set; }
    }
}
