using Agent.Api.Infrastructure.Data;
using Agent.Api.Interfaces;
using Agent.Api.Services;
using Asp.Versioning;

using Microsoft.EntityFrameworkCore;


namespace Agent.Api.Bootstraping;

public static class ApplicationServiceExtensions
{
  public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
  {
    builder.AddServiceDefaults();
    builder.Services.AddOpenApi();

    //builder.Services.AddApiVersioning(
    //    opts =>
    //    {
    //      opts.ReportApiVersions = true;
    //      opts.ApiVersionReader = ApiVersionReader.Combine(
    //              new UrlSegmentApiVersionReader(),
    //              new HeaderApiVersionReader("X-Version")
    //              );
    //    }
    //);
    builder.AddNpgsqlDbContext<AgentDbContext>("bqDb", configureDbContextOptions: dbContextOptionsBuilder =>
    {
      dbContextOptionsBuilder
      .UseNpgsql(builder => builder.MigrationsAssembly(typeof(AgentDbContext).Assembly.FullName));
    });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                  "AllowAll",
                  builder =>
                  {
                      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                  }
              );
        });

        builder.Services.AddScoped<IIdentityService, IdentityService>();
        builder.Services.AddScoped<IMessagesService, MessagesService>();
        builder.Services.AddScoped<IConversationService, ConversationService>();
        return builder;
  }
}
