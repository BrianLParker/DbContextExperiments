// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public sealed class MessageFoundationServiceException : Exception
{
    public MessageFoundationServiceException(Exception innerException)
        : base(message: "Message foundation service error occurred, contact support.", innerException)
    { }
}
