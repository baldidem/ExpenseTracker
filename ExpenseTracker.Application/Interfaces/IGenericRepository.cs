using ExpenseTracker.Domain.Entities;
using System.Linq.Expressions;

namespace ExpenseTracker.Application.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(int id); //Find null donebilir. Bu nedenle nullable yaptim.
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        //Task SaveChangesAsync();

        //Task<TEntity> GetByIdAsync(int id, Expression<Func<TEntity,object>>[] includes);
        //Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>[] includes);
    }
}
