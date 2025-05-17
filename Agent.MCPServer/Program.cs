using Agent.MCPServer.Tools;
using MCPServer;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Semantic Kernel
var chatModelId = builder.Configuration.GetConnectionString("chatModelId");
if (string.IsNullOrEmpty(chatModelId))
{
  throw new ArgumentNullException(nameof(chatModelId), "The chatModelId connection string cannot be null or empty.");
}

var endpoint = builder.Configuration.GetConnectionString("endpoint");
if (string.IsNullOrEmpty(endpoint))
{
  throw new ArgumentNullException(nameof(endpoint), "The endpoint connection string cannot be null or empty.");
}

var apiKey = builder.Configuration.GetConnectionString("apiKey");
if (string.IsNullOrEmpty(apiKey))
{
  throw new ArgumentNullException(nameof(apiKey), "The apiKey connection string cannot be null or empty.");
}

var kernelBuilder = builder.Services.AddKernel();
kernelBuilder.Plugins.AddFromFunctions("Agents", [AgentKernelFunctionFactory.CreateFromAgent(CreateSalesAssistantAgent(chatModelId, endpoint, apiKey))]);

builder.Services.AddHttpClient();

builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithSKPlugins();

builder.Services.AddOpenTelemetry()
    .WithTracing(b => b.AddSource("*")
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation())
    .WithMetrics(b => b.AddMeter("*")
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation())
    .WithLogging()
    .UseOtlpExporter();


builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy", builder =>
      builder.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());
});



var app = builder.Build();

app.MapMcp();

app.MapGet("/hello", () => "Hello World!");
app.UseCors("CorsPolicy");

app.Run();

static ChatCompletionAgent CreateSalesAssistantAgent(string chatModelId, string endpoint, string apiKey)
{
  IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

  // Register the SK plugin for the agent to use
  kernelBuilder.Plugins.AddFromType<OrderProcessingUtils>();

  // Register chat completion service
  kernelBuilder.AddAzureOpenAIChatCompletion(chatModelId, endpoint, apiKey);

  Kernel kernel = kernelBuilder.Build();

  // Define the agent
  return new ChatCompletionAgent()
  {
    Name = "SalesAssistant",
    Instructions = "You are a sales assistant. Place orders for items the user requests and handle refunds.",
    Description = "Agent to invoke to place orders for items the user requests and handle refunds.",
    Kernel = kernel,
    Arguments = new KernelArguments(new PromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
  };
}

