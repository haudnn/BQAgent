using System.ComponentModel.DataAnnotations.Schema;

namespace Agent.MCPServer.Domain.Entities;


[Table("conversation_members")]
public class ConversationMember
{
  public Guid Id { get; set; } = Guid.CreateVersion7();
  public Guid ConversationId { get; set; }
  public Guid UserId { get; set; }
  public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
  public bool IsAdmin { get; set; } = false;

  // Navigation properties
  public Conversation Conversation { get; set; } = default!;
  public User User { get; set; } = default!;
}

// Indexes {
//   (conversation_id, user_id) [unique]  
// }