using System;
using System.Linq;
using System.Threading.Tasks;
using DbContextExperiments.Api.Models.Foundations.Messages;

namespace DbContextExperiments.Api.Services.Orchestrations.Messages;

public interface IMessageOrchestrationService
{
    ValueTask<Message> InsertMessageAsync(Message message);
    IQueryable<Message> RetrieveAll();
    ValueTask<Message> FindMessageAsync(Guid messageId);
    ValueTask<Message> UpdateMessageAsync(Guid messageId, Message message);
    ValueTask<Message> DeleteMessageByIdAsync(Guid messageId);
}
