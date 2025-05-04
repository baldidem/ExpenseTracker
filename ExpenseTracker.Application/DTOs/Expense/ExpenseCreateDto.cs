using ExpenseTracker.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Application.DTOs.Expense
{
    public class ExpenseCreateDto
    {
        //public int UserId { get; set; } // Bunu ben otomatik dolduracagim.
        public decimal Amount { get; set; }
        public int ExpenseCategoryId { get; set; }
        //public string? DocumentPath { get; set; }
        [JsonIgnore]  // Eğer JSON ile gönderiyorsan bu ignore edilir (biz FromForm kullanacağız).
        public IFormFile? Document { get; set; }  // Dosya alanı eklendi
    }
}
