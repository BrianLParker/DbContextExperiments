using System;

namespace DbContextExperiments.Api.Brokers.DateTimes;

public class DateTimeBroker : IDateTimeBroker
{
    public DateTimeOffset GetCurrentTime() => DateTimeOffset.Now;
}
