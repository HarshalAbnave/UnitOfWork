using UnitOfWork.Core.IRepositories;

namespace UnitOfWork.Core.IConfiguration;

public interface IUnitOfWork
{
    IUserRepository Users { get; }

    Task CompleteAsync();
}