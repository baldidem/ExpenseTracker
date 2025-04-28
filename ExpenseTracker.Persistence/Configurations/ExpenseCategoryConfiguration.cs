using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configurations
{
    public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).HasMaxLength(30);

            builder.HasMany(x => x.Expenses)
                .WithOne(e => e.ExpenseCategory)
                .HasForeignKey(x => x.ExpenseCategoryId);
        }
    }
}
