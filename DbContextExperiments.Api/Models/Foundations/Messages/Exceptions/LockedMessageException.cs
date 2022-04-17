using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;

public class LockedMessageException : Exception
{
    public LockedMessageException(Exception innerException)
        : base(message: "Message locked error occurred, please try again later.", innerException)
    { }
}