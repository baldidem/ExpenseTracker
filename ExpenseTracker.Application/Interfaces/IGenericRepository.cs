using ExpenseTracker.Domain.Entities;
using System.Linq.Expressions;

namespace ExpenseTracker.Application.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(int id, params string[] includes); //Find null donebilir. Bu nedenle nullable yaptim.
        Task<List<TEntity>> GetAllAsync(); 
        Task<List<TEntity>> GetAllByParametersAsync(params Expression<Func<TEntity, bool>>[] predicates);
        Task<TEntity> GetByParametersAsync(params Expression<Func<TEntity, bool>>[] predicates);
        Task<TEntity> CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);
    }
}
