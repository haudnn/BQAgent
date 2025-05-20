using Agent.Api.Infrastructure.Data;
using Agent.Api.Interfaces;

namespace Agent.Api.Services;

public class SystemServices(AgentDbContext dbContext, ILogger<SystemServices> logger, IIdentityService identityService)
{
	public AgentDbContext DbContext { get; } = dbContext;
	public ILogger<SystemServices> Logger => logger;
	public IIdentityService IdentityService => identityService;
}