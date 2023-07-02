using Microsoft.EntityFrameworkCore;
using UnitOfWork.Models;

namespace UnitOfWork.Data;

public class ApplicationDbContext : DbContext
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DbContext" /> class using the specified options.
    ///     The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be called to allow further
    ///     configuration of the options.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see> and
    ///     <see href="https://aka.ms/efcore-docs-dbcontext-options">Using DbContextOptions</see> for more information and examples.
    /// </remarks>
    /// <param name="options">The options for this context.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
}