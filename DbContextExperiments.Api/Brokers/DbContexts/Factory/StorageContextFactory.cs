// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace DbContextExperiments.Api.Brokers.DbContexts.Factory;

public class StorageContextFactory<TContext> : IStorageContextFactory
    where TContext : DbContext
{
    private readonly IDbContextFactory<TContext> contextFactory;

    public StorageContextFactory(IDbContextFactory<TContext> contextFactory) => this.contextFactory = contextFactory;

    public IStorageContext<T> CreateStorageContext<T>()
        where T : class => new StorageContext<T>(contextFactory.CreateDbContext());

    public IStorageContext<T> CreateStorageContext<T>(string dbSetName)
        where T : class => new StorageContext<T>(contextFactory.CreateDbContext(), dbSetName);
}
