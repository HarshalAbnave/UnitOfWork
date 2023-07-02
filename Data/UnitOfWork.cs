using UnitOfWork.Core.IConfiguration;
using UnitOfWork.Core.IRepositories;
using UnitOfWork.Core.Repositories;
using UnitOfWork.Models;

namespace UnitOfWork.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    public IUserRepository Users {get; private set; }

    public UnitOfWork(
        ApplicationDbContext context,
        ILogger<UnitOfWork> logger,
        ILogger<GenericRepository<User>> Generic_logger,
        ILogger<UserRepository> User_logger )
    {
        _context = context;
        _logger = logger;

        Users = new UserRepository(context, Generic_logger, User_logger);
    }
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async void Dispose()
    {
        await _context.DisposeAsync();
    }
}