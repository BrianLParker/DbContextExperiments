// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public class MessageValidationException : Exception
{
    public MessageValidationException(Exception innerException)
        : base(message: "Message validation error occurred, please try again.", innerException)
    { }
}
