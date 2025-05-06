using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount)
                .IsRequired()
                .HasPrecision(16, 2);
            builder.Property(x => x.ExpenseCategoryId)
                .IsRequired();
            builder.Property(x => x.ExpenseStatus)
                .IsRequired();
        }
    }
}
