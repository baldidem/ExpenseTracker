using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ExpenseCategory> ExpenseCategoryRepository { get; }
        IGenericRepository<Expense> ExpenseRepository { get; }
        IGenericRepository<PaymentSimulation> PaymentSimulationRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        Task SaveChangesAsync();
    }
}
