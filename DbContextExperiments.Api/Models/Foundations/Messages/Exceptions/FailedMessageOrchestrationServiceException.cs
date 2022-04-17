// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public sealed class FailedMessageOrchestrationServiceException : Exception
{
    public FailedMessageOrchestrationServiceException(Exception innerException)
        : base("Failed message orchestration error occurred, contact support.", innerException)
    { }
}
