// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Brokers.Loggings;

public interface ILoggingBroker
{
    void LogInformation(string message);
    void LogTrace(string message);
    void LogDebug(string message);
    void LogWarning(string message);
    void LogError(Exception exception);
    void LogCritical(Exception exception);
}
