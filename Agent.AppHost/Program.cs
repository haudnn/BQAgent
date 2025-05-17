var builder = DistributedApplication.CreateBuilder(args);


var openai = builder.AddConnectionString("openai");
var chatModelId = builder.AddConnectionString("chatModelId");
var endpoint = builder.AddConnectionString("endpoint");
var apiKey = builder.AddConnectionString("apiKey");

var vectorDB = builder.AddQdrant("vectordb")
		.WithDataVolume()
		.WithLifetime(ContainerLifetime.Persistent);

var ingestionCache = builder.AddSqlite("ingestionCache");

var mcpserver = builder.AddProject<Projects.Agent_MCPServer>("mcp-server");
mcpserver.WithReference(chatModelId);
mcpserver.WithReference(endpoint);
mcpserver.WithReference(apiKey);

var webApp = builder.AddProject<Projects.Agent_Chat>("chat-app");
webApp.WithReference(openai);	
webApp
		.WithReference(vectorDB)
		.WaitFor(vectorDB);
webApp
		.WithReference(ingestionCache)
		.WaitFor(ingestionCache);
webApp
		.WithReference(mcpserver)
		.WaitFor(mcpserver);

builder.Build().Run();