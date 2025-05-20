using Microsoft.Extensions.AI;

namespace Agent.Api.Models;

public class MessageRequest
{
    public Guid SenderId { get; set; }
    public string Content { get; set; } = default!;
    public ChatRole Role { get; set; } = ChatRole.User;
    public Guid ConversationId { get; set; }
}
