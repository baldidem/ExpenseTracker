using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configurations
{
    public class PaymentSimulationConfiguration : IEntityTypeConfiguration<PaymentSimulation>
    {
        public void Configure(EntityTypeBuilder<PaymentSimulation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount)
                .IsRequired()
                .HasPrecision(16, 2);
            builder.Property(x => x.PaidDate)
                .IsRequired();

            builder.HasOne(x => x.Expense)
                .WithOne(e => e.PaymentSimulation)
                .HasForeignKey<PaymentSimulation>(x => x.ExpenseId);

        }
    }
}
