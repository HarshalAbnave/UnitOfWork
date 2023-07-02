using Microsoft.EntityFrameworkCore;
using UnitOfWork.Core.IRepositories;
using UnitOfWork.Data;

namespace UnitOfWork.Core.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected ApplicationDbContext dbContext = null!;
    protected DbSet<TEntity> dbSet = null!;
    private readonly ILogger<GenericRepository<TEntity>> _logger = null!;

    public GenericRepository(
        ApplicationDbContext context,
        ILogger<GenericRepository<TEntity>> logger)
    {
        dbContext = context;
        _logger = logger;

        dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> All()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<TEntity?> GetById(Guid Id)
    {
        return await dbSet.FindAsync(Id);
    }

    public virtual async Task<bool> Add(TEntity entity)
    {
        await dbSet.AddAsync(entity);
        return true;
    }

    public virtual Task<bool> Delete(Guid Id)
    {
        throw new NotImplementedException();
    }

    public virtual Task<bool> Upsert(TEntity entity)
    {
        throw new NotImplementedException();
    }
}