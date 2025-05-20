using Agent.Api.Infrastructure.Entities;
using Agent.Api.Models;


namespace Agent.Api.Interfaces;

public interface IMessagesService
{
    Task<Message?> CreateMessageAsync(MessageRequest request);

    Task<List<Message>> GetMessagesByConversationId(Guid conversationId, int pageIndex = 1, int pageSize = 100);
}
