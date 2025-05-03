using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Context
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<PaymentSimulation> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpenseTrackerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        //    int.TryParse(userIdClaim, out int userId);

        //    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedDate = DateTime.UtcNow;
        //                entry.Entity.CreatedUserId = userId;
        //                entry.Entity.IsActive = true;
        //                break;

        //            case EntityState.Modified:
        //                entry.Entity.UpdatedDate = DateTime.UtcNow;
        //                entry.Entity.UpdatedUserId = userId;
        //                break;

        //            case EntityState.Deleted:
        //                entry.State = EntityState.Modified; // Soft-delete
        //                entry.Entity.DeletedDate = DateTime.UtcNow;
        //                entry.Entity.DeletedUserId = userId;
        //                entry.Entity.IsActive = false;
        //                break;
        //        }
        //    }

        //    return await base.SaveChangesAsync(cancellationToken);
        //}
        //TO DO:INTERCEPTOR EKLE. // BURDA DIREKT HTTPCONTEXTACCESSORA TIGHTLY COUPLED VAR. O YUZDEN KALDIRDIM
        //EXPENSE CATEGORY REDIS.
    }
}
