using Agent.Api.Infrastructure.Data;
using Agent.Api.Infrastructure.Entities;
using Agent.Api.Interfaces;
using Agent.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Agent.Api.Services;

public class ConversationService(AgentDbContext dbContext) : IConversationService
{
    private readonly AgentDbContext _dbContext= dbContext;
    public async Task<Conversation?> CreateConversationAsync(CreateConversationRequest command)
    {
        var conversation = new Conversation()
        {
            Name = command.Name,
            IsGroup = command.IsGroup,
            CreatedAt = DateTime.UtcNow,
            CreatedById = command.CreatedById,
            UpdatedAt = DateTime.UtcNow,
        };
        try
        {
            await _dbContext.Conversations.AddAsync(conversation);
            await _dbContext.SaveChangesAsync();
            return conversation;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Conversation?> GetConversationById(Guid id)
    {
        var conversation = await _dbContext.Conversations.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (conversation == null)
            return null;
        return conversation;
    }

    public async Task<ICollection<Conversation>> GetConversationsByUserId(Guid userId, int pageIndex = 1, int pageSize = 100)
    {
        IQueryable<Conversation> conversations = _dbContext.Conversations;

        conversations = conversations.Where(p => p.CreatedById == userId);

        return await conversations
              .OrderBy(c => c.CreatedAt)
              .Skip(pageIndex * pageSize)
              .Take(pageSize)
              .ToListAsync();

    }

    public async Task<Conversation?> UpdateConversationById(Guid id, UpdateConversationRequest command)
    {
        var conversation = await _dbContext.Conversations.FirstOrDefaultAsync(c => c.Id == id);
        if (conversation == null)
            return null;
        conversation.Name = command.Name;
        conversation.UpdatedAt = DateTime.UtcNow;
        try
        {
            _dbContext.Conversations.Update(conversation);
            await _dbContext.SaveChangesAsync();
            return conversation;
        }
        catch
        {
            return null;
        }
    }
}