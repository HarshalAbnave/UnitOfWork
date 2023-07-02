using UnitOfWork.Models;
using UnitOfWork.Core.IRepositories;
using UnitOfWork.Data;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork.Core.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(
        ApplicationDbContext context,
        ILogger<GenericRepository<User>> Generic_logger,
        ILogger<UserRepository> logger) : base(context, Generic_logger)
    {
        _logger = logger;
    }

    public override async Task<IEnumerable<User>> All()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in Method {nameof(All)} of class {nameof(UserRepository)}");
            return Enumerable.Empty<User>();
        }
    }

    public override async Task<bool> Upsert(User entity)
    {
        try
        {
            User? existingUser = await dbSet.FindAsync(entity.Id);
            if(existingUser is null) await dbSet.AddAsync(entity);

            existingUser.Email = entity.Email;
            existingUser.FirstName = entity.FirstName;
            existingUser.LastName = entity.LastName;
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in Method {nameof(Upsert)} of class {nameof(UserRepository)}");
            return false;
        }
    }

    public override async Task<bool> Delete(Guid Id)
    {
        try
        {
            User? existingUser = await dbSet.FindAsync(Id);
            if (existingUser is null) return false;

            dbSet.Remove(existingUser);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in Method {nameof(Delete)} of class {nameof(UserRepository)}");
            return false;
        }
    }
}