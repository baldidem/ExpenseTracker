using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Persistence.Context;

namespace ExpenseTracker.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExpenseTrackerDbContext _context;

        public UnitOfWork(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<ExpenseCategory> ExpenseCategoryRepository => new GenericRepository<ExpenseCategory>(_context);

        public IGenericRepository<Expense> ExpenseRepository => new GenericRepository<Expense>(_context);
        public IGenericRepository<PaymentSimulation> PaymentSimulationRepository => new GenericRepository<PaymentSimulation>(_context);
        public IGenericRepository<Role> RoleRepository => new GenericRepository<Role>(_context);
        public IGenericRepository<User> UserRepository => new GenericRepository<User>(_context);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
