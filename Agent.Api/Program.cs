using Agent.Api.Apis;

using Agent.Api.Bootstraping;

	
var builder = WebApplication.CreateBuilder(args);


builder.AddApplicationServices();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}
//app.UseAuthentication();
//app.UseAuthorization();
app.UseHttpsRedirection();
app.BQApi();

app.Run();