// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;

namespace DbContextExperiments.Api.Models.Foundations.Messages;

public class Message
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public DateTimeOffset CreatedPointInTime { get; set; }

    public DateTimeOffset UpdatedPointInTime { get; set; }
}
