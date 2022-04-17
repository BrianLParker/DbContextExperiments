// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Threading.Tasks;
using DbContextExperiments.Api.Brokers.DbContexts;
using DbContextExperiments.Api.Models.Foundations.Messages;

namespace DbContextExperiments.Api.Brokers.Storages;

public interface IStorageBroker
{
    ValueTask<Message> InsertMessageAsync(Message message);
    ValueTask<Message> FindMessageAsync(Guid messageId);
    ValueTask<Message> UpdateMessageAsync(Message message);
    ValueTask<Message> RemoveMessageAsync(Message message);
    IStorageContext<Message> QueryableMessages();
}
