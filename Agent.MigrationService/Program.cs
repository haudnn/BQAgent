
using Agent.Api.Infrastructure.Data;
using Agent.MigrationService;

using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();
builder.AddNpgsqlDbContext<AgentDbContext>("bqDb", configureDbContextOptions: dbContextOptionsBuilder =>
{
	dbContextOptionsBuilder.UseNpgsql(builder => builder.MigrationsAssembly(typeof(AgentDbContext).Assembly.FullName));
}
);
var host = builder.Build();
host.Run();
