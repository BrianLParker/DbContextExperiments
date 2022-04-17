// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Exceptions;

public class DuplicateKeyWithUniqueIndexException : Exception
{
    public string DuplicateKeyValue { get; }

    public DuplicateKeyWithUniqueIndexException(string message)
        : base(message)
    {
        string[] subStrings = message.Split('(', ')');

        if (subStrings.Length is 3)
        {
            DuplicateKeyValue = subStrings[1];
        }
    }
}
