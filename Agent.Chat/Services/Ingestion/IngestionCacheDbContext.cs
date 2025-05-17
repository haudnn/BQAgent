using Microsoft.EntityFrameworkCore;

namespace Agent.Chat.Services.Ingestion;


public class IngestionCacheDbContext(DbContextOptions<IngestionCacheDbContext> options) : DbContext(options)
{
  public DbSet<IngestedDocument> Documents { get; set; } = default!;
  public DbSet<IngestedRecord> Records { get; set; } = default!;

  public static void Initialize(IServiceProvider services)
  {
    using var scope = services.CreateScope();
    using var db = scope.ServiceProvider.GetRequiredService<IngestionCacheDbContext>();
    db.Database.EnsureCreated();
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<IngestedDocument>().HasMany(d => d.Records).WithOne().HasForeignKey(r => r.DocumentId).OnDelete(DeleteBehavior.Cascade);
  }
}

public class IngestedDocument
{
  // TODO: Make Id+SourceId a composite key
  public required string Id { get; set; }
  public required string SourceId { get; set; }
  public required string Version { get; set; }
  public List<IngestedRecord> Records { get; set; } = [];
}

public class IngestedRecord
{
  public required Guid Id { get; set; }
  public required string DocumentId { get; set; }
}
