// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System.Threading.Tasks;

namespace DbContextExperiments.Api.Brokers.DbContexts;

public partial class StorageContext<T> : IStorageContext<T>
    where T : class
{
    public T Add(T entity)
    {
        var entityEntry = dbSet.Add(entity);
        return entityEntry.Entity;
    }

    public async ValueTask<T> FindAsync(params object[] keyValues)
        => await dbSet.FindAsync(keyValues);

    public T Update(T entity)
    {
        var entityEntry = dbSet.Update(entity);
        return entityEntry.Entity;
    }

    public T Remove(T entity)
    {
        var entityEntry = dbSet.Remove(entity);
        return entityEntry.Entity;
    }

    public async ValueTask SaveChangesAsync()
        => await dbContext.SaveChangesAsync();
}
