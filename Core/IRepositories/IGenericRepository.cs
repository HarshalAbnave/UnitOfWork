namespace UnitOfWork.Core.IRepositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> All();
    Task<TEntity?> GetById(Guid Id);
    Task<bool> Add(TEntity entity);
    Task<bool> Delete(Guid Id);
    Task<bool> Upsert(TEntity entity);
}