using Agent.Api.Apis;

using Agent.Api.Bootstraping;

	
var builder = WebApplication.CreateBuilder(args);


builder.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}
//app.UseAuthentication();
//app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapBQApi();

app.Run();