// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Threading.Tasks;
using DbContextExperiments.Api.Brokers.DbContexts;
using DbContextExperiments.Api.Models.Foundations.Messages;

namespace DbContextExperiments.Api.Brokers.Storages;

public partial class StorageBroker : IStorageBroker
{
    public async ValueTask<Message> InsertMessageAsync(Message message)
    {
        using var messageContext = CreateMessageContext();
        var trackedMessage = messageContext.Add(message);
        await messageContext.SaveChangesAsync();
        return trackedMessage;
    }

    public IStorageContext<Message> QueryableMessages()
        => CreateMessageContext();

    public async ValueTask<Message> FindMessageAsync(Guid messageId)
    {
        using var messageContext = CreateMessageContext();
        return await messageContext.FindAsync(messageId);
    }

    public async ValueTask<Message> UpdateMessageAsync(Message message)
    {
        using var messageContext = CreateMessageContext();
        var trackedMessage = messageContext.Update(message);
        await messageContext.SaveChangesAsync();
        return trackedMessage;
    }

    public async ValueTask<Message> RemoveMessageAsync(Message message)
    {
        using var messageContext = CreateMessageContext();
        var trackedMessage = messageContext.Remove(message);
        await messageContext.SaveChangesAsync();
        return trackedMessage;
    }

    private IStorageContext<Message> CreateMessageContext()
        => storageContextFactory.CreateStorageContext<Message>(dbSetName: "Messages");
}
