using ExpenseTracker.Application.Interfaces.Auth;

namespace ExpenseTracker.Infrastructure.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmPassword(string password, string hashedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
