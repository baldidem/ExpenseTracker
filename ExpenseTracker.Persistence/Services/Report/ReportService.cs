using Dapper;
using ExpenseTracker.Application.DTOs.Report;
using ExpenseTracker.Application.Interfaces.CurrentUser;
using ExpenseTracker.Application.Services.Report;
using ExpenseTracker.Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace ExpenseTracker.Persistence.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentService;

        public ReportService(IConfiguration configuration, ICurrentUserService currentService)
        {
            _configuration = configuration;
            _currentService = currentService;
        }


        public async Task<CompanyPaymentRateReportResponseDto> GetCompanyPaymentRateAsync(DateTime startDate, DateTime endDate)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MSSQLServer")))
            {
                var sql = @"
            SELECT 
                p.Amount,
                p.Currency,
                p.PaidDate
            FROM Payments p
            WHERE p.PaymentTransactionStatus = @ConfirmedStatus
              AND p.PaidDate BETWEEN @StartDate AND @EndDate
              AND p.IsActive = 1
        ";

                var details = (await connection.QueryAsync<CompanyPaymentRateReportDto>(sql, new
                {
                    ConfirmedStatus = PaymentTransactionStatus.Confirmed,
                    StartDate = startDate,
                    EndDate = endDate
                })).ToList();

                var totalAmount = details.Sum(x => x.Amount);
                var transactionCount = details.Count;

                return new CompanyPaymentRateReportResponseDto
                {
                    TransactionCount = transactionCount,
                    TotalAmount = totalAmount,
                    Report = details
                };
            }

        }

        public async Task<List<ExpenseReportDto>> GetReportForCurrentUserAsync()
        {
            var userId = _currentService.CurrentUserId;
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not found!");
            }
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MSSQLServer")))
            {
                var sql = @"
                    SELECT 
                        e.Id AS ExpenseId,
                        e.Amount,
                        e.Currency,
                        e.ExpenseStatus,
                        c.Name AS ExpenseCategoryName,
                        e.CreatedDate,
                        e.RejectionReason
                    FROM Expenses e
                    INNER JOIN ExpenseCategories c ON e.ExpenseCategoryId = c.Id
                    WHERE e.UserId = @UserId AND e.IsActive = 1
                    ORDER BY e.CreatedDate DESC;
                ";

                var result = await connection.QueryAsync<ExpenseReportDto>(sql, new { UserId = userId });
                return result.ToList();
            }
        }
        public async Task<UserPaymentReportResponseDto> GetUserPaymentRateAsync(DateTime? startDate, DateTime? endDate, int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MSSQLServer")))
            {
                var sql = @"
                 SELECT 
                u.Name AS UserName,
                u.Surname AS UserSurname,
                p.Amount,
                p.Currency,
                p.PaidDate
            FROM Payments p
            INNER JOIN Users u ON p.CreatedUserId = u.Id
            WHERE p.PaymentTransactionStatus = 2
              AND p.IsActive = 1
        ";

                // Dinamik filtreler ekleme
                if (startDate.HasValue && endDate.HasValue)
                {
                    sql += " AND p.PaidDate BETWEEN @StartDate AND @EndDate";
                }


                sql += " AND e.UserId = @UserId";


                sql += " ORDER BY p.PaidDate DESC;";

                var payments = await connection.QueryAsync<UserPaymentRateReportDto>(sql, new
                {
                    ConfirmedStatus = PaymentTransactionStatus.Confirmed,
                    StartDate = startDate,
                    EndDate = endDate,
                    UserId = userId
                });

                var paymentList = payments.ToList();

                var response = new UserPaymentReportResponseDto
                {
                    TransactionCount = paymentList.Count,
                    TotalAmount = paymentList.Sum(x => x.Amount),
                    Report = paymentList
                };

                return response;
            }
        }
        public async Task<ExpenseApprovalStatusReportResponseDto> GetApprovalStatusReportAsync(DateTime? startDate, DateTime? endDate, ExpenseStatus? status = null)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MSSQLServer")))
            {
                var sql = @"
            SELECT 
                e.Amount,
                e.Currency,
                c.Name AS ExpenseCategoryName,
                e.ExpenseStatus,
                e.CreatedDate
            FROM Expenses e
            INNER JOIN ExpenseCategories c ON e.ExpenseCategoryId = c.Id
            WHERE e.IsActive = 1
              AND (@Status IS NULL OR e.ExpenseStatus = @Status)
              AND (@StartDate IS NULL OR e.CreatedDate >= @StartDate)
              AND (@EndDate IS NULL OR e.CreatedDate <= @EndDate)
            ORDER BY e.CreatedDate DESC;
        ";

                var expenses = await connection.QueryAsync<ExpenseApprovalStatusReportDto>(sql, new
                {
                    Status = status,
                    StartDate = startDate,
                    EndDate = endDate
                });

                var expenseList = expenses.ToList();

                var response = new ExpenseApprovalStatusReportResponseDto
                {
                    ApprovedCount = expenseList.Count(x => x.ExpenseStatus == ExpenseStatus.Approved),
                    RejectedCount = expenseList.Count(x => x.ExpenseStatus == ExpenseStatus.Rejected),
                    Report = expenseList
                };

                return response;
            }
        }



    }
}
