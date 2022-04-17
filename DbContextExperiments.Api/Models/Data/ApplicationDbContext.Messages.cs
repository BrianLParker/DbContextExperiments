// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using DbContextExperiments.Api.Models.Foundations.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbContextExperiments.Api.Models.Data;

public partial class ApplicationDbContext : DbContext
{
    private DbSet<Message> Messages { get; set; }

    private static void OnMessageCreating(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Message> entityMessage = modelBuilder.SharedTypeEntity<Message>(nameof(Messages));

        entityMessage.HasKey(message => message.Id);

        entityMessage.Property(message => message.Content)
            .IsRequired()
            .HasMaxLength(maxLength: 4000);

        entityMessage.Property(message => message.CreatedPointInTime)
            .HasColumnName(name: "Created");

        entityMessage.Property(message => message.UpdatedPointInTime)
            .HasColumnName(name: "Updated");

        entityMessage.ToTable(
            name: "Messages",
            buildAction: tableBuilder => tableBuilder.IsTemporal());
    }
}
