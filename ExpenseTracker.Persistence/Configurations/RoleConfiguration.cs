using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);

            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    CreatedUserId = 1,
                    CreatedDate = DateTime.Parse("2025-04-22T10:00:00Z")
                },
                new Role
                {
                    Id = 2,
                    Name = "Staff",
                    CreatedUserId = 1,
                    CreatedDate = DateTime.Parse("2025-04-22T10:00:00Z")
                });
        }
    }
}
