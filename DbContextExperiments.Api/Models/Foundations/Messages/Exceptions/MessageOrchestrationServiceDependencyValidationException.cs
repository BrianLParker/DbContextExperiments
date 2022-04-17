// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public sealed class MessageOrchestrationServiceDependencyValidationException : Exception
{
    public MessageOrchestrationServiceDependencyValidationException(Exception innerException)
        : base(message: "Message orchestration validation error occurred, please try again.", innerException)
    { }
}
