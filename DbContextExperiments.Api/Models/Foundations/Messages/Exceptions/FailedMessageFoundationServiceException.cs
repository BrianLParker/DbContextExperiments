// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public sealed class FailedMessageFoundationServiceException : Exception
{
    public FailedMessageFoundationServiceException(Exception innerException)
        : base(message: "Failed message foundation service error occurred, contact support.", innerException)
    { }
}
