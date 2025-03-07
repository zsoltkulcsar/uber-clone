using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Uber.Common.Entities;

namespace Uber.Common.Repositories
{
    public abstract class BaseRepository<T, V> : IRepository<T, V>
       where T : Entity<V>
       where V : struct
    {
        protected readonly DbContext _context;
        private IDbContextTransaction? _transaction;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<T?> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var result = _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().AddRange(entities);

            var updated = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return updated > 0;
        }

        public virtual async Task<int> CountAllEntriesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .CountAsync(cancellationToken);
        }

        public virtual async Task<bool> DeleteAsync(V id, CancellationToken cancellationToken = default)
        {
            int deleted = await _context.Set<T>()
                                .Where(t => t.Id.Equals(id))
                                .ExecuteDeleteAsync(cancellationToken);
            return deleted > 0;
        }

        public virtual async Task<bool> ExistsAsync(V id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .CountAsync(x => x.Id.Equals(id), cancellationToken) == 1;
        }

        public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(V id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is not null)
            {
                await _transaction.CommitAsync(cancellationToken);
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync(cancellationToken);
                await _transaction.DisposeAsync();
            }
        }
    }
}
