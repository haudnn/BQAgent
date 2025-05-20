using Agent.Api.Infrastructure.Data;
using Agent.Api.Infrastructure.Entities;
using Agent.Api.Interfaces;
using Agent.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;


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

    public async Task<ICollection<Message>> GetMessagesByConversationId(Guid conversationId, int pageIndex = 1, int pageSize = 100)
    {
        IQueryable<Message> messages = _dbContext.Messages;
        messages = messages.Where(p => p.ConversationId == conversationId);
        return await messages
              .Include(u => u.Sender)
              .OrderBy(c => c.CreatedAt)
              .Skip(pageIndex * pageSize)
              .Take(pageSize)
              .ToListAsync();
    }
}
