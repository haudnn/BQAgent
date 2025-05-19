using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Agent.Chat.Components;
using Agent.Chat.Services;
using Agent.Chat.Services.Ingestion;
using Microsoft.SemanticKernel.Connectors.Qdrant;


var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();


var openai = builder.AddAzureOpenAIClient("openai");
openai.AddChatClient("gpt-4o-mini")
    .UseFunctionInvocation()
    .UseOpenTelemetry(configure: c =>
        c.EnableSensitiveData = builder.Environment.IsDevelopment());
openai.AddEmbeddingGenerator("text-embedding-3-small");


builder.AddQdrantClient("vectordb");

builder.Services.AddSingleton<IVectorStore, QdrantVectorStore>(); 
builder.Services.AddScoped<DataIngestor>();
builder.Services.AddSingleton<SemanticSearch>();
builder.AddSqliteDbContext<IngestionCacheDbContext>("ingestionCache");

var app = builder.Build();
IngestionCacheDbContext.Initialize(app.Services);

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await DataIngestor.IngestDataAsync(
    app.Services,
    new PDFDirectorySource(Path.Combine(builder.Environment.WebRootPath, "Data")));

app.Run();