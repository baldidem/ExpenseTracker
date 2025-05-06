using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.DTOs.Report
{
    public class CompanyPaymentRateReportDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime PaidDate { get; set; }
    }

    public class CompanyPaymentRateReportResponseDto
    {
        public int TransactionCount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CompanyPaymentRateReportDto> Report { get; set; }
    }
}
