namespace ExpenseTracker.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Iban { get; set; }
        public virtual Role Role { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }

}
