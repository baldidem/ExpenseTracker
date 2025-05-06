using ExpenseTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTOs.Report
{
    public class UserPaymentRateReportDto
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime PaidDate { get; set; }
    }

    public class UserPaymentReportResponseDto
    {
        public int TransactionCount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<UserPaymentRateReportDto> Report { get; set; }
    }
}
