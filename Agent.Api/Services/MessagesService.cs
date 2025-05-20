using Agent.Api.Infrastructure.Data;
using Agent.Api.Infrastructure.Entities;
using Agent.Api.Interfaces;
using Agent.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Agent.Api.Services;

public class MessagesService(AgentDbContext dbContext) : IMessagesService
{
    private readonly AgentDbContext _dbContext = dbContext;
    public async Task<Message?> CreateMessageAsync(MessageRequest request)
    {
        var message = new Message()
        {
            Content = request.Content,
            ConversationId = request.ConversationId,
            Role = request.Role.Value,
            SenderId = request.SenderId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow, 
        };
        try
        {
            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
            return message;
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<Message>> GetMessagesByConversationId(Guid conversationId, int pageIndex = 1, int pageSize = 100)
    {
        var res =  await _dbContext.Messages
              .Where(p => p.ConversationId == conversationId)
              .Include(u => u.Sender)
              .OrderBy(c => c.CreatedAt)
              .ToListAsync();
        return res;
    }
}
