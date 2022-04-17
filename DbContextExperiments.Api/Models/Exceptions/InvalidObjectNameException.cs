// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Exceptions;

public class InvalidObjectNameException : Exception
{
    public InvalidObjectNameException(string message) : base(message) { }
}
