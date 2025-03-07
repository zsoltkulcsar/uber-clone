using System.Linq.Expressions;
using Uber.Common.Entities;

namespace Uber.Common.Repositories
{
    public interface IRepository<T, V>
        where T : Entity<V>
        where V : struct
    {
        Task<T?> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task<int> CountAllEntriesAsync(CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(V id, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(V id, CancellationToken cancellationToken = default);

        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<T?> GetByIdAsync(V id, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
