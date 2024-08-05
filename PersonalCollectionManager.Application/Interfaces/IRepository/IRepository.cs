using System.Linq.Expressions;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> Update(T entity);
        Task Remove(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        Task RemoveRangeAsync(T entity);
        Task<bool> UpdateRangeAsync(IEnumerable<T> entity);
    }
}
