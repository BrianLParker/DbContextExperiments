// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using DbContextExperiments.Api.Models.Exceptions;
using DbContextExperiments.Api.Models.Foundations.Messages;
using DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DbContextExperiments.Api.Services.Foundations.Messages;

public partial class MessageFoundationService
{
    private delegate IQueryable<Message> ReturningMessagesFunction();
    private delegate ValueTask<Message> ReturningMessageFunction();

    private IQueryable<Message> TryCatch(ReturningMessagesFunction returningMessagesFunction)
    {
        try
        {
            return returningMessagesFunction();
        }
        catch (Exception exception)
        {
            var failedMessageFoundationServiceException =
                   new FailedMessageFoundationServiceException(exception);

            throw CreateAndLogServiceException(failedMessageFoundationServiceException);
        }
    }

    private async ValueTask<Message> TryCatch(ReturningMessageFunction returningMessageFunction)
    {
        try
        {
            return await returningMessageFunction();
        }
        catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
        {
            var lockedMessageException =
                new LockedMessageException(dbUpdateConcurrencyException);

            throw CreateAndLogDependencyValidationException(lockedMessageException);
        }
        catch (DbUpdateException dbUpdateException)
        {
            Exception updateException = this.dbUpdateExceptionBroker.ConvertToMeaningfulException(dbUpdateException);

            Exception localizedUpdateException = LocalizeUpdateException(updateException);

            throw localizedUpdateException switch
            {
                AlreadyExistsMessageException alreadyExistsMessageException => CreateAndLogDependencyValidationException(alreadyExistsMessageException),
                _ => CreateAndLogServiceException(localizedUpdateException)
            };

        }
        catch (Exception exception)
        {
            var failedMessageFoundationServiceException =
                new FailedMessageFoundationServiceException(exception);

            throw CreateAndLogServiceException(failedMessageFoundationServiceException);
        }
    }

    private static Exception LocalizeUpdateException(Exception updateException)
    {
        return updateException switch
        {
            DuplicateKeyException duplicateKeyException =>
                new AlreadyExistsMessageException(duplicateKeyException),

            ForeignKeyConstraintConflictException foreignKeyConstraintConflictException =>
                foreignKeyConstraintConflictException,

            DuplicateKeyWithUniqueIndexException duplicateKeyWithUniqueIndexException =>
                duplicateKeyWithUniqueIndexException,

            InvalidObjectNameException invalidObjectNameException =>
                invalidObjectNameException,

            InvalidColumnNameException invalidColumnNameException =>
                invalidColumnNameException,

            Exception exception => exception
        };
    }

    private MessageFoundationServiceException CreateAndLogServiceException(Exception exception)
    {
        var messageFoundationServiceException = new MessageFoundationServiceException(exception);
        this.loggingBroker.LogError(messageFoundationServiceException);

        return messageFoundationServiceException;
    }

    private MessageFoundationServiceDependencyValidationException CreateAndLogDependencyValidationException(Exception exception)
    {
        var messageFoundationServiceDependencyValidationException = new MessageFoundationServiceDependencyValidationException(exception);
        this.loggingBroker.LogError(messageFoundationServiceDependencyValidationException);

        return messageFoundationServiceDependencyValidationException;
    }
}
