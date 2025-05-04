using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTOs.Expense
{
    public class ExpenseFilterDto
    {
        public DateTime? ExpenseDate { get; set; }
        public int? ExpenseCategoryId { get; set; }
        public string? ExpenseStatus { get; set; }
    }
}
