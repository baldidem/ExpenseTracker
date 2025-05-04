using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTracker.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ExpenseTrackerDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ExpenseTrackerDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int id, params string[] includes)
        {
            //var query = _dbSet.AsQueryable();
            //query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
            //return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, x => x.Id == id);
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<List<TEntity>> GetAllByParametersAsync(params Expression<Func<TEntity, bool>>[] predicates)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null && includes.Any())
            {
                foreach (var include  in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetByParametersAsync(params Expression<Func<TEntity, bool>>[] predicates)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
