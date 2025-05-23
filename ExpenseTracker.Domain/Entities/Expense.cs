﻿using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }  = Currency.TRY;
        public int ExpenseCategoryId { get; set; }
        public string? DocumentPath { get; set; }
        public ExpenseStatus ExpenseStatus { get; set; }
        public string? RejectionReason { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public User User { get; set; }
        public ICollection<PaymentSimulation?> PaymentSimulation { get; set; }
    }
}
