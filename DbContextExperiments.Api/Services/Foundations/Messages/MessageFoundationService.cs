// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbContextExperiments.Api.Brokers.DateTimes;
using DbContextExperiments.Api.Brokers.DbContexts;
using DbContextExperiments.Api.Brokers.DbUpdateExceptions;
using DbContextExperiments.Api.Brokers.Loggings;
using DbContextExperiments.Api.Brokers.Storages;
using DbContextExperiments.Api.Models.Foundations.Messages;

namespace DbContextExperiments.Api.Services.Foundations.Messages;

public partial class MessageFoundationService : IMessageFoundationService, IDisposable
{
    private readonly IStorageBroker storageBroker;
    private readonly IDateTimeBroker dateTimeBroker;
    private readonly IDbUpdateExceptionBroker dbUpdateExceptionBroker;
    private readonly ILoggingBroker loggingBroker;
    private readonly List<IDisposable> disposableResources;

    public MessageFoundationService(
        IStorageBroker storageBroker,
        IDateTimeBroker dateTimeBroker,
        IDbUpdateExceptionBroker dbUpdateExceptionBroker,
        ILoggingBroker loggingBroker)
    {
        this.storageBroker = storageBroker;
        this.dateTimeBroker = dateTimeBroker;
        this.dbUpdateExceptionBroker = dbUpdateExceptionBroker;
        this.loggingBroker = loggingBroker;
        this.disposableResources = new();
    }

    public ValueTask<Message> InsertMessageAsync(Message message) =>
    TryCatch(async () =>
    {
        ValidateNewMessage(message);
        return await storageBroker.InsertMessageAsync(message);
    });

    public IQueryable<Message> QueryableMessages() =>
    TryCatch(() =>
    {
        IStorageContext<Message> messageContext = storageBroker.QueryableMessages();
        disposableResources.Add(messageContext);
        return messageContext;
    });

    public async ValueTask<Message> FindMessageAsync(Guid messageId) => await storageBroker.FindMessageAsync(messageId);
    public async ValueTask<Message> UpdateMessageAsync(Message message) => await storageBroker.UpdateMessageAsync(message);
    public async ValueTask<Message> DeleteMessageAsync(Message message) => await storageBroker.RemoveMessageAsync(message);

    public void Dispose()
    {
        foreach (var c in disposableResources)
        {
            c.Dispose();
        }
    }
}
