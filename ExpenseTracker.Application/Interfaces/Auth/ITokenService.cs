using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
