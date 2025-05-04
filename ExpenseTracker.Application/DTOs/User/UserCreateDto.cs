using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.DTOs.User
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Iban { get; set; }
    }
}
