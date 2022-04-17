// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using DbContextExperiments.Api.Brokers.DbContexts.Factory;

namespace DbContextExperiments.Api.Brokers.Storages;

public partial class StorageBroker : IStorageBroker
{
    private readonly IStorageContextFactory storageContextFactory;

    public StorageBroker(IStorageContextFactory storageContextFactory) => this.storageContextFactory = storageContextFactory;
}
