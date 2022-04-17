// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public sealed class MessageOrchestrationServiceDependencyException : Exception
{
    public MessageOrchestrationServiceDependencyException(Exception innerException)
        : base(message: "Message orchestration service dependency error occurred, contact support.", innerException)
    { }
}
