// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------


using System;

namespace DbContextExperiments.Api.Models.Exceptions;

public class DuplicateKeyException : Exception
{
    public DuplicateKeyException(string message) : base(message) { }
}
