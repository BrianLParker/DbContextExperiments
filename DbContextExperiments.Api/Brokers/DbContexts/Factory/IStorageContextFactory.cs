namespace DbContextExperiments.Api.Brokers.DbContexts.Factory;

public interface IStorageContextFactory
{
    IStorageContext<T> CreateStorageContext<T>()
        where T : class;
    IStorageContext<T> CreateStorageContext<T>(string dbSetName)
        where T : class;
}
