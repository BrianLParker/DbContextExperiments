using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbContextExperiments.Api.Models.Foundations.Messages;

public class Message
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public DateTimeOffset CreatedPointInTime { get; set; }

    public DateTimeOffset UpdatedPointInTime { get; set; }
}
