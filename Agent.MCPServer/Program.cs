using Agent.MCPServer.Tools;
using MCPServer;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Plugins.OpenApi;
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
kernelBuilder.Plugins.AddFromFunctions("Agents", [AgentKernelFunctionFactory.CreateFromAgent(await CreateAssistantAgent(chatModelId, endpoint, apiKey))]);

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

static async Task<ChatCompletionAgent> CreateAssistantAgent(string chatModelId, string endpoint, string apiKey)
{
    IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

    // kernelBuilder.Plugins.AddFromType<OrderProcessingUtils>();

    kernelBuilder.AddAzureOpenAIChatCompletion(chatModelId, endpoint, apiKey);

    // Build the kernel
    Kernel kernel = kernelBuilder.Build();

    var plugin = await kernel.ImportPluginFromOpenApiAsync(
        pluginName: "lights",
        uri: new Uri("https://localhost:7275/swagger/v1/swagger.json"),
        executionParameters: new OpenApiFunctionExecutionParameters()
        {
            EnablePayloadNamespacing = true
        }
    );

    return new ChatCompletionAgent()
    {
        Name = "Assistant",
        Instructions = @"You are an assistant for a business, you can:
        1. Register new users using the Register function",
        Description = "User will make requests to interact with the system",
        Kernel = kernel,
        Arguments = new KernelArguments(new PromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
    };
}