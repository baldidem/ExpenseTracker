using ExpenseTracker.Domain.Entities;
using System.Linq.Expressions;

namespace ExpenseTracker.Application.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(int id); //Find null donebilir. Bu nedenle nullable yaptim.
        Task<List<TEntity>> GetAllAsync(); // bunlara predicate veya params[] ekle include islemleri icin.
        Task<List<TEntity>> GetByParametersAsync(params Expression<Func<TEntity, bool>>[] predicates);
        Task<TEntity> CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        //Task SaveChangesAsync();

        //Task<TEntity> GetByIdAsync(int id, Expression<Func<TEntity,object>>[] includes);
        //Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>[] includes); 


        //TODO: GetByParameters ekle bir de getlere expression ekle include icin.
    }
}
