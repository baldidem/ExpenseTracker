namespace ExpenseTracker.Application.Interfaces.CurrentUser
{
    public interface ICurrentUserService
    {
        public int? CurrentUserId { get; }
        string? CurrentUserRole { get; }
    }
}
