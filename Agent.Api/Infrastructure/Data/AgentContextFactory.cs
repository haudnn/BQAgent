using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Agent.Api.Infrastructure.Data;

public class AgentContextFactory : IDesignTimeDbContextFactory<AgentDbContext>
{
  public AgentDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<AgentDbContext>();

    optionsBuilder.UseNpgsql("Host=localhost;Database=bqDb;Username=postgres;Password=postgres");

    return new AgentDbContext(optionsBuilder.Options);
  }
}