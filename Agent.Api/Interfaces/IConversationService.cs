using Agent.Api.Infrastructure.Entities;
using Agent.Api.Models;

namespace Agent.Api.Interfaces;

public interface IConversationService
{
    Task<Conversation?> CreateConversationAsync(CreateConversationRequest command);
    Task<Conversation?> GetConversationById(Guid id);
    Task<List<Conversation>> GetConversationsByUserId(Guid userId, int pageIndex = 1, int pageSize = 100);
    Task<Conversation?> UpdateConversationById(Guid id, UpdateConversationRequest command);
}
