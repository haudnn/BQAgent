namespace Agent.Api.Infrastructure.Entities;
public class Conversation
{
  public Guid Id { get; set; } = Guid.CreateVersion7();
  public string Name { get; set; } = default!;
  public bool IsGroup { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public Guid CreatedById { get; set; }

  // Navigation properties
  public User CreatedBy { get; set; } = default!;
}