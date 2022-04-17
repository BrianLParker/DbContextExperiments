// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public sealed class AlreadyExistsMessageException : Exception
{
    public AlreadyExistsMessageException(Exception innerException)
        : base("Message with the same id already exists.", innerException)
    { }
}
