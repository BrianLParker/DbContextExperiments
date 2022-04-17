// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public sealed class MessageFoundationServiceDependencyException : Exception
{
    public MessageFoundationServiceDependencyException(Exception innerException)
        : base(message: "Message foundation service dependency error occurred, contact support.", innerException)
    { }
}
