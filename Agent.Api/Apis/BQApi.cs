using Agent.Api.Infrastructure.Entities;
using Agent.Api.Models;
using Agent.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace Agent.Api.Apis;

public static class Api
{
	public static IEndpointRouteBuilder BQApi(this IEndpointRouteBuilder builder)
	{

		var vApi = builder.NewVersionedApi("Bq");
		var v1 = vApi.MapGroup("api/v{version:apiVersion}").HasApiVersion(1, 0);
		v1.MapPost("/auth/login", Register)
			.WithName("Login")
			.Produces<Ok<User>>(StatusCodes.Status200OK)
			.Produces<Conflict>(StatusCodes.Status409Conflict)
			.Produces<BadRequest>(StatusCodes.Status400BadRequest)
			.AllowAnonymous();
		return builder;
	}



	public static async Task<Results<Ok<User>, Conflict, BadRequest>> Register(
	[AsParameters] SystemServices services, RegisterRequest request)
	{
		var userExists = services.DbContext.Users.AsNoTracking().FirstOrDefault(u => u.EmployeeCode == request.EmployeeCode);
		if (userExists != null)
			return TypedResults.Conflict();

		var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
		var user = new User
		{
			EmployeeCode = request.EmployeeCode,
			DisplayName = request.DisplayName,
			Email = request.Email,
			PasswordHash = passwordHash,
			AvatarUrl = null,
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow
		};
		try
		{
			await services.DbContext.Users.AddAsync(user);
			await services.DbContext.SaveChangesAsync();
			services.Logger.LogInformation("User registered");
			return TypedResults.Ok(user);
		}
		catch (Exception ex)
		{
			services.Logger.LogError(ex, "Error registering user");
			return TypedResults.BadRequest();
		}
	}
}
