namespace ExpenseTracker.Application.Interfaces.CurrentUser
{
    public interface ICurrentUserService
    {
        public int? CurrentUserId { get; } // Bu null olmamali. Zaten token uretildiyse bu null olamaz.
        string? CurrentUserRole { get; }
    }
}
