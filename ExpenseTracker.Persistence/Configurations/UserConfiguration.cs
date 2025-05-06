using ExpenseTracker.Application.Interfaces.Auth;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(80);
            builder.HasIndex(x => x.Email)
                .IsUnique();
            builder.HasIndex(x => x.Iban)
           .IsUnique();
            builder.Property(x=>x.RoleName)
                .IsRequired();

            builder.HasOne(x => x.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(x => x.RoleId);

            builder.HasMany(x => x.Expenses)
                .WithOne(u => u.User)
                .HasForeignKey(x => x.UserId);

            builder.HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Surname = "Admin",
                    Email = "admin@gmail.com",
                    PasswordHash = "$2a$11$Dn3UFdRCm0rIiH3nBsCvLO0.A37tKAxKzybZOt62fwdmtFEw3oc1y",
                    RoleId = 1,
                    Iban = "TR000000000000000000000001",
                    RoleName = "Admin",
                    IsActive = true,
                    CreatedUserId = 1,
                    CreatedDate = DateTime.Parse("2025-04-22T10:00:00Z")
                },
                new User
                {
                    Id = 2,
                    Name = "Staff",
                    Surname = "Staff",
                    Email = "staff@gmail.com",
                    PasswordHash = "$2a$11$sDo52TL5goRJa/SJLbttqOCYkek4mBgdmWq1Z0sHUaJQZh60vOICy",
                    RoleId = 2,
                    Iban = "TR000000000000000000000002",
                    RoleName = "Staff",
                    IsActive = true,
                    CreatedUserId = 1,
                    CreatedDate = DateTime.Parse("2025-04-22T10:00:00Z")
                });
        }

    }
}
