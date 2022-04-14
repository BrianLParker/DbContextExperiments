using System;

namespace DbContextExperiments.Client.Models
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreatedPointInTime { get; set; }

        public DateTimeOffset UpdatedPointInTime { get; set; }

    }
}
