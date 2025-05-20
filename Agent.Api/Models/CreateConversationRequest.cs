namespace Agent.Api.Models;

public class CreateConversationRequest
{
    public required string Name { get; set; }
    public bool IsGroup { get; set; }
    public Guid CreatedById { get; set; }
}
