namespace ExpenseTracker.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }  // Role eklemek isterse admin yeni rol ekleyebilir.
        public ICollection<User> Users { get; set; } // Bir role tanimli bircok user olabilir.
    }
}
