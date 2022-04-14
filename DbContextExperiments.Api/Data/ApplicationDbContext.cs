using DbContextExperiments.Api.Models.Foundations.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbContextExperiments.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
         : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        EntityTypeBuilder<Message> entityMessage = modelBuilder.Entity<Message>();

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
