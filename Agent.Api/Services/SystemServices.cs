using Agent.Api.Infrastructure.Data;

namespace Agent.Api.Services;

public class SystemServices(AgentDbContext dbContext, ILogger<SystemServices> logger)
{
	public AgentDbContext DbContext { get; } = dbContext;

	public ILogger<SystemServices> Logger => logger;

}