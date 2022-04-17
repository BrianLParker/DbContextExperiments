// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System.Threading.Tasks;
using DbContextExperiments.Api.Models.Foundations.Messages;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DbContextExperiments.Api.Tests.Unit.Services.Messages;

public partial class MessageFoundationServiceTests
{
    [Fact]
    public async Task ShouldAddMessageAsync()
    {
        Message m = new();
        Message inputMessage = m;
        Message storageMessage = inputMessage;
        Message expectedMessage = storageMessage.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.InsertMessageAsync(inputMessage))
                .ReturnsAsync(storageMessage);

        // when
        Message actualMessage =
            await messageFoundationServiceMockedBroker.InsertMessageAsync(inputMessage);

        // then
        actualMessage.Should().BeEquivalentTo(expectedMessage);

        this.storageBrokerMock.Verify(broker =>
            broker.InsertMessageAsync(inputMessage),
                Times.Once);

        this.dateTimeBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
