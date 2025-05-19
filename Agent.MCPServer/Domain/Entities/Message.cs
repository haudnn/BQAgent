using System.ComponentModel.DataAnnotations.Schema;

namespace Agent.MCPServer.Domain.Entities;


[Table("messages")]
public class Message
{
  public Guid Id { get; set; } = Guid.CreateVersion7();
  public Guid ConversationId { get; set; }
  public Guid SenderId { get; set; }
  public string Content { get; set; } = default!;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

  // Navigation properties
  public Conversation Conversation { get; set; } = default!;
  public User Sender { get; set; } = default!;
}
