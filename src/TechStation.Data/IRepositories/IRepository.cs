using TechStation.Domain.Commons;

namespace TechStation.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<int> CountAsync();
    Task<bool> ClearAsync();
    IQueryable<TEntity> SelectAll();
    Task<bool> DeleteAsync(long id);
    Task<TEntity> SelectByIdAsync(long id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
}
