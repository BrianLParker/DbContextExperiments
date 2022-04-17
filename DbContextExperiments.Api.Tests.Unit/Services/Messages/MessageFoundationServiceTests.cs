// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Collections;
using System.Linq.Expressions;
using DbContextExperiments.Api.Brokers.DateTimes;
using DbContextExperiments.Api.Brokers.DbContexts;
using DbContextExperiments.Api.Brokers.DbContexts.Factory;
using DbContextExperiments.Api.Brokers.DbUpdateExceptions;
using DbContextExperiments.Api.Brokers.Loggings;
using DbContextExperiments.Api.Brokers.Storages;
using DbContextExperiments.Api.Models.Foundations.Messages;
using DbContextExperiments.Api.Services.Foundations.Messages;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Tynamix.ObjectFiller;

namespace DbContextExperiments.Api.Tests.Unit.Services.Messages;

public partial class MessageFoundationServiceTests
{
    private readonly Mock<IStorageContextFactory> storageContextFactoryMock;
    private readonly Mock<IStorageContext<Message>> storageContextMock;
    private readonly Mock<IStorageBroker> storageBrokerMock;
    private readonly Mock<IDbUpdateExceptionBroker> dbUpdateExceptionBrokerMock;
    private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;


    private readonly IStorageBroker storageBroker;
    private readonly IMessageFoundationService messageFoundationServiceMockedBroker;
    private readonly IMessageFoundationService messageFoundationService;

    public MessageFoundationServiceTests()
    {
        storageContextFactoryMock = new Mock<IStorageContextFactory>();
        storageContextMock = new Mock<IStorageContext<Message>>();
        storageBrokerMock = new Mock<IStorageBroker>();
        dbUpdateExceptionBrokerMock = new Mock<IDbUpdateExceptionBroker>();
        dateTimeBrokerMock = new Mock<IDateTimeBroker>();
        loggingBrokerMock = new Mock<ILoggingBroker>();

        storageBroker = new StorageBroker(storageContextFactoryMock.Object);

        messageFoundationServiceMockedBroker = new MessageFoundationService(
            storageBroker: storageBrokerMock.Object,
            dateTimeBroker: dateTimeBrokerMock.Object,
            dbUpdateExceptionBroker: dbUpdateExceptionBrokerMock.Object,
            loggingBroker: loggingBrokerMock.Object);

        messageFoundationService = new MessageFoundationService(
            storageBroker: storageBroker,
            dateTimeBroker: dateTimeBrokerMock.Object,
            dbUpdateExceptionBroker: dbUpdateExceptionBrokerMock.Object,
            loggingBroker: loggingBrokerMock.Object);
    }

    private static string GetRandomErrorMessage() =>
        new MnemonicString(wordCount: GetRandomNumber()).GetValue();

    private static int GetRandomNumber() =>
        new IntRange(min: 2, max: 10).GetValue();

    private static Message CreateRandomMessage() =>
          CreateMessageFiller(dates: GetRandomDateTimeOffset()).Create();

    private static DateTimeOffset GetRandomDateTimeOffset() =>
        new DateTimeRange(earliestDate: new DateTime()).GetValue();

    private static Filler<Message> CreateMessageFiller(DateTimeOffset dates)
    {
        var filler = new Filler<Message>();

        filler.Setup()
            .OnType<DateTimeOffset>().Use(dates);

        return filler;
    }
    private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
    {
        return actualException =>
            actualException.Message == expectedException.Message
            && actualException.InnerException.Message == expectedException.InnerException.Message
            && DataEquals((actualException.InnerException as Exception).Data, expectedException.InnerException.Data);
    }
    public static bool DataEquals(IDictionary left, IDictionary right)
    {
        foreach (DictionaryEntry item in right)
        {
            bool num = !left.Contains(item.Key);
            bool flag = CompareData(left[item.Key], right[item.Key]);
            if (num || flag)
            {
                return false;
            }
        }

        return true;
    }

    private static bool CompareData(object firstObject, object secondObject)
    {
        AssertionScope assertionScope = new AssertionScope();
        firstObject.Should().BeEquivalentTo(secondObject, "");
        return assertionScope.HasFailures();
    }
}
