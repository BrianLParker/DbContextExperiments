// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public sealed class MessageFoundationServiceDependencyValidationException : Exception
{
    public MessageFoundationServiceDependencyValidationException(Exception innerException)
        : base(message: "Message dependency validation error occurred, please try again.", innerException)
    { }
}
