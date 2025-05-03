using ExpenseTracker.Application.Interfaces.Auth;

namespace ExpenseTracker.Infrastructure.Auth
{
    public class BcryptPasswordHasher : IBcryptPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool ConfirmPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password,hashedPassword);
        }
    }
}
