using ExpenseTracker.Application.DTOs.Report;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Services.Report
{
    public interface IReportService
    {
        Task<List<ExpenseReportDto>> GetReportForCurrentUserAsync();
        Task<CompanyPaymentRateReportResponseDto> GetCompanyPaymentRateAsync(DateTime startDate, DateTime endDate);
        Task<UserPaymentReportResponseDto> GetUserPaymentRateAsync(DateTime? startDate, DateTime? endDate, int userId);
        Task<ExpenseApprovalStatusReportResponseDto> GetApprovalStatusReportAsync(DateTime? startDate, DateTime? endDate, ExpenseStatus? status);
    }
}
