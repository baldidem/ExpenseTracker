using ExpenseTracker.Domain.Enums;
using System.Transactions;

namespace ExpenseTracker.Domain.Entities
{
    public class PaymentSimulation : BaseEntity
    {
        public int ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidDate { get; set; }
        public Expense Expense { get; set; }
        public PaymentTransactionStatus PaymentTransactionStatus { get; set; }
    }
}
