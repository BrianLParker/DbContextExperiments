// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public sealed class MessageOrchestrationServiceException : Exception
{
    public MessageOrchestrationServiceException(Exception innerException)
        : base(message: "Message orchestration service error occurred, contact support.", innerException)
    { }
}
