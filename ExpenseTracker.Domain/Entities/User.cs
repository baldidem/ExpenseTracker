namespace ExpenseTracker.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Password hashlenmis sekilde tutulacak.
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Iban { get; set; }
        public virtual Role Role { get; set; } //Role icerisinin dolu gelmesini ozellikle istemedigim durumlarda bos gelsin diye virtual tanimladim.
        public ICollection<Expense> Expenses { get; set; }
    }

    //public class AppUser : BaseEntity
    //{
    //    public string Name { get; set; }
    //    public string Surname { get; set; }
    //    public string Email { get; set; }
    //    public string PasswordHash { get; set; } // Password hashlenmis sekilde tutulacak.
    //    public int RoleId { get; set; }
    //    public string RoleName { get; set; }
    //    public string Iban { get; set; }
    //    public virtual Role Role { get; set; } //Role icerisinin dolu gelmesini ozellikle istemedigim durumlarda bos gelsin diye virtual tanimladim.
    //    public ICollection<Expense> Expenses { get; set; }
    //}
}
