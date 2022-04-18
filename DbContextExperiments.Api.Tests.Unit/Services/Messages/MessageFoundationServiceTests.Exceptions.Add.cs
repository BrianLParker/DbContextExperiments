// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using DbContextExperiments.Api.Models.Exceptions;
using DbContextExperiments.Api.Models.Foundations.Messages;
using DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DbContextExperiments.Api.Tests.Unit.Services.Messages;

public partial class MessageFoundationServiceTests
{
    [Fact]
    public async Task ShouldThrowMessageFoundationValidationExceptionOnAddIfMessageAlreadyExistsAndLogItAsync()
    {
        // given
        Message someMessage = CreateRandomMessage();
        string someErrorMessage = GetRandomErrorMessage();

        var someDbUpdateException = new DbUpdateException(someErrorMessage);

        var duplicateKeyException =
              new DuplicateKeyException(someErrorMessage);

        var alreadyExistsMessageException =
            new AlreadyExistsMessageException(duplicateKeyException);

        var expectedMessageFoundationServiceDependencyValidationException =
            new MessageFoundationServiceDependencyValidationException(alreadyExistsMessageException);

        this.storageContextFactoryMock.Setup(factory =>
            factory.CreateStorageContext<Message>(It.IsAny<string>()))
                .Returns(storageContextMock.Object);

        this.storageContextMock.Setup(context =>
            context.SaveChangesAsync())
                .Throws(someDbUpdateException);

        this.dbUpdateExceptionBrokerMock.Setup(broker =>
            broker.ConvertToMeaningfulException(someDbUpdateException))
                .Returns(duplicateKeyException);

        this.storageContextMock.Setup(context =>
            context.Add(someMessage))
                .Returns(someMessage);
        // when
        ValueTask<Message> insertMessageTask =
            this.messageFoundationService.InsertMessageAsync(someMessage);

        // then
        await Assert.ThrowsAsync<MessageFoundationServiceDependencyValidationException>(() =>
            insertMessageTask.AsTask());

        this.storageContextMock.Verify(context =>
            context.SaveChangesAsync(), Times.Once);

        this.storageContextMock.Verify(context =>
            context.Dispose(), Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedMessageFoundationServiceDependencyValidationException))), Times.Once);

        this.storageContextMock.Verify(context =>
            context.Add(It.IsAny<Message>()), Times.Once);

        this.storageContextMock.Verify(context =>
            context.SaveChangesAsync(), Times.Once);

        this.dateTimeBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageContextMock.VerifyNoOtherCalls();
        this.storageContextFactoryMock.VerifyNoOtherCalls();
    }
}
