// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using DbContextExperiments.Api.Models.Foundations.Messages;

namespace DbContextExperiments.Api.Services.Foundations.Messages;

public interface IMessageFoundationService
{
    ValueTask<Message> InsertMessageAsync(Message message);
    IQueryable<Message> QueryableMessages();
    ValueTask<Message> FindMessageAsync(Guid messageId);
    ValueTask<Message> UpdateMessageAsync(Message message);
    ValueTask<Message> DeleteMessageAsync(Message message);
}
