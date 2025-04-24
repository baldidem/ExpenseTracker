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
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            return new GenericRepository<TEntity>(_context);
        }
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
