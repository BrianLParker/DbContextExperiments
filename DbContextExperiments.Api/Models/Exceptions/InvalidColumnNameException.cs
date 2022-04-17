// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Exceptions;

public class InvalidColumnNameException : Exception
{
    public InvalidColumnNameException(string message) : base(message) { }
}
