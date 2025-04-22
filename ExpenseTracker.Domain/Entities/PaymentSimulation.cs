namespace ExpenseTracker.Domain.Entities
{
    public class PaymentSimulation : BaseEntity
    {
        public int ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public Expense Expense { get; set; }
    }
}
