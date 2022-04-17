using System;
using System.Linq;
using System.Threading.Tasks;

namespace DbContextExperiments.Api.Brokers.DbContexts;

public interface IStorageContext<T> : IQueryable<T>, IDisposable
    where T : class
{
    T Add(T entity);
    ValueTask<T> FindAsync(params object[] keyValues);
    T Update(T entity);
    T Remove(T entity);
    ValueTask SaveChangesAsync();
}
