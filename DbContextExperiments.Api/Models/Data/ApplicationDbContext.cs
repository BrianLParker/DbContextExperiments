using Microsoft.EntityFrameworkCore;

namespace DbContextExperiments.Api.Models.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
         : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        OnMessageCreating(modelBuilder);
    }
}
