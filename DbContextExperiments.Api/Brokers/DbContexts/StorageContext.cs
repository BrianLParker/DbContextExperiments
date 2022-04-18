using Microsoft.EntityFrameworkCore;

namespace DbContextExperiments.Api.Brokers.DbContexts;

public partial class StorageContext<T> : IStorageContext<T>
    where T : class
{
    private readonly DbContext dbContext;
    private readonly DbSet<T> dbSet;

    internal StorageContext(DbContext dbContext)
        : this(dbContext, dbContext.Set<T>())
    { }

    internal StorageContext(DbContext dbContext, string dbSetName)
        : this(dbContext, dbContext.Set<T>(dbSetName))
    { }

    private StorageContext(DbContext dbContext, DbSet<T> dbSet)
    {
        this.dbContext = dbContext;
        this.dbSet = dbSet;
    }

    public void Dispose() => dbContext?.Dispose();
}
