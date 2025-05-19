var builder = DistributedApplication.CreateBuilder(args);


var openai = builder.AddConnectionString("openai");
var chatModelId = builder.AddConnectionString("chatModelId");
var endpoint = builder.AddConnectionString("endpoint");
var apiKey = builder.AddConnectionString("apiKey");



var postgres = builder.AddPostgres("postgres")
		.WithDataVolume()
		.WithImageTag("latest")
		.WithLifetime(ContainerLifetime.Persistent)
		.WithPgWeb();

IResourceBuilder<PostgresDatabaseResource>? bqDb = postgres.AddDatabase("bqDb");

var vectorDB = builder.AddQdrant("vectordb")
		.WithDataVolume()
		.WithLifetime(ContainerLifetime.Persistent);

var ingestionCache = builder.AddSqlite("ingestionCache");

var migrationService = builder.AddProject<Projects.Agent_MigrationService>("migration-service")
		.WithReference(bqDb)
		.WaitFor(bqDb);



var api = builder.AddProject<Projects.Agent_Api>("api")
		.WithExternalHttpEndpoints()
		.WithReference(bqDb)
		.WaitFor(postgres)
		.WaitFor(migrationService);


var mcpserver = builder.AddProject<Projects.Agent_MCPServer>("mcp-server")
	.WithExternalHttpEndpoints()
	.WithReference(chatModelId)
	.WithReference(endpoint)
	.WithReference(apiKey);


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