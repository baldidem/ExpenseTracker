using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Application.DTOs.Expense
{
    public class ExpenseUpdateDto
    {
        public decimal? Amount { get; set; }
        public int? ExpenseCategoryId { get; set; }
        [JsonIgnore]
        public IFormFile? Document { get; set; }
    }
}
