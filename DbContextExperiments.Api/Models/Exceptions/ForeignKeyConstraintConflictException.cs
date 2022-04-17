// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Exceptions;

public class ForeignKeyConstraintConflictException : Exception
{
    public ForeignKeyConstraintConflictException(string message) : base(message) { }
}
