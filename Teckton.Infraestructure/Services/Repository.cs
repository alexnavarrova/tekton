using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tekton.Infraestructure.Interfaces.DataAccess;

namespace Tekton.Infraestructure.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Properties
        private readonly TektonContext _context;
        protected DbSet<TEntity> Entity { get; set; }
        #endregion

        public Repository(TektonContext context)
        {
            _context = context;
            _context.ChangeTracker.LazyLoadingEnabled = true;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            Entity = _context.Set<TEntity>();
        }

        #region Queries
        public async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            var result = await Entity.AsNoTracking().ToListAsync();
            return result.Any() ? result : null;
        }

        public async Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Entity.AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            if (where != null) query = query.Where(where);

            var result = await query.AsNoTracking().ToListAsync();
            return result.Any() ? result : null;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = Entity.AsQueryable();

            return await query.AsNoTracking().AnyAsync(where);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Entity.AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(where);
        }

        #endregion

        #region Commands
        public async Task<bool> AddEntity(TEntity entity)
        {
            await _context.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}

