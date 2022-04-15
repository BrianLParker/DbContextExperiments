using System;

namespace DbContextExperiments.Api.Brokers.DateTimes;

public interface IDateTimeBroker
{
    DateTimeOffset GetCurrentTime();
}
