namespace Agent.Api.Infrastructure.Entities;
public class Message
{
  public Guid Id { get; set; } = Guid.CreateVersion7();
  public Guid ConversationId { get; set; }
  public Guid SenderId { get; set; }
  public string Content { get; set; } = default!;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public string Role { get; set; } = default!;

  // Navigation properties
  public Conversation Conversation { get; set; } = default!;
  public User Sender { get; set; } = default!;
}
