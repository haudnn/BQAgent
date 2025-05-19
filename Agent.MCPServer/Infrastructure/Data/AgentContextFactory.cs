using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Agent.MCPServer.Infrastructure.Data;

public class AgentContextFactory : IDesignTimeDbContextFactory<AgentDbContext>
{
  public AgentDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<AgentDbContext>();

    optionsBuilder.UseNpgsql("Host=localhost;Database=agent;Username=postgres;Password=postgres")
                  .UseSnakeCaseNamingConvention();

    return new AgentDbContext(optionsBuilder.Options);
  }
}