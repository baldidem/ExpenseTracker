using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public int UserId { get; set; } // Bu expense hangi personele ait
        public decimal Amount { get; set; } // Expense miktari
        public string Currency { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string? DocumentPath { get; set; } //Evrak zorunlu degil.
        public ExpenseStatus ExpenseStatus { get; set; }
        public string? RejectionReason { get; set; }

        public ExpenseCategory ExpenseCategory { get; set; }
        public User User { get; set; }
        public ICollection<PaymentSimulation?> PaymentSimulation { get; set; } // Bir expense icin payment olusturuldu. iptal edildi mesela tekrar odeme istegi atilir.
    }
}
