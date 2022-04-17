// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using DbContextExperiments.Api.Models.Foundations.Messages;
using DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

namespace DbContextExperiments.Api.Services.Orchestrations.Messages;

public partial class MessageOrchestrationService : IMessageOrchestrationService
{
    private delegate IQueryable<Message> ReturningMessagesFunction();
    private delegate ValueTask<Message> ReturningMessageFunction();

    private async ValueTask<Message> TryCatch(ReturningMessageFunction returningMessageFunction)
    {
        try
        {
            return await returningMessageFunction();
        }
        catch (MessageFoundationServiceDependencyValidationException exception)
        {
            throw CreateAndLogDependencyValidationException(exception);
        }
        catch (Exception exception)
        {
            var failedMessageOrchestrationServiceException =
                new FailedMessageOrchestrationServiceException(exception);

            throw CreateAndLogServiceException(failedMessageOrchestrationServiceException);
        }
    }

    private MessageOrchestrationServiceException CreateAndLogServiceException(Exception exception)
    {
        var messageOrchestrationServiceException = new MessageOrchestrationServiceException(exception);
        this.loggingBroker.LogError(messageOrchestrationServiceException);

        return messageOrchestrationServiceException;
    }

    private MessageOrchestrationServiceDependencyValidationException CreateAndLogDependencyValidationException(
          Exception exception)
    {
        var messageOrchestrationServiceDependencyValidationException =
            new MessageOrchestrationServiceDependencyValidationException(exception.InnerException);

        this.loggingBroker.LogError(messageOrchestrationServiceDependencyValidationException);

        return messageOrchestrationServiceDependencyValidationException;
    }

}