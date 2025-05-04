using ExpenseTracker.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTOs.ExpenseCategory
{
    public class ExpenseCategoryResponseDto : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
