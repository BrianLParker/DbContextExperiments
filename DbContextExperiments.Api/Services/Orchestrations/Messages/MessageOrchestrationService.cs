// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using DbContextExperiments.Api.Brokers.Loggings;
using DbContextExperiments.Api.Models.Foundations.Messages;
using DbContextExperiments.Api.Services.Foundations.Messages;

namespace DbContextExperiments.Api.Services.Orchestrations.Messages;

public partial class MessageOrchestrationService : IMessageOrchestrationService
{
    private readonly IMessageFoundationService messageService;
    private readonly ILoggingBroker loggingBroker;

    public MessageOrchestrationService(
        IMessageFoundationService messageService,
        ILoggingBroker loggingBroker)
    {
        this.messageService = messageService;
        this.loggingBroker = loggingBroker;
    }


    public ValueTask<Message> InsertMessageAsync(Message message) =>
    TryCatch(async () =>
    {
        return await messageService.InsertMessageAsync(message);
    });


    public IQueryable<Message> RetrieveAll() => messageService.QueryableMessages();
    public async ValueTask<Message> FindMessageAsync(Guid messageId)
        => await messageService.FindMessageAsync(messageId);
    public async ValueTask<Message> UpdateMessageAsync(Guid messageId, Message message)
        => await messageService.UpdateMessageAsync(message);
    public async ValueTask<Message> DeleteMessageByIdAsync(Guid messageId)
    {
        var message = await messageService.FindMessageAsync(messageId);
        return await messageService.UpdateMessageAsync(message);
    }
}