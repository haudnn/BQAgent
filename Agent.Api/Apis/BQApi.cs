using Agent.Api.Infrastructure.Entities;
using Agent.Api.Models;
using Agent.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using System.ComponentModel;


namespace Agent.Api.Apis;

public static class TestApi
{
	public static IEndpointRouteBuilder MapBQApi(this IEndpointRouteBuilder builder)
	{

        //var vApi = builder.NewVersionedApi("Bq");
        //var v1 = vApi.MapGroup("api/v{version:apiVersion}").HasApiVersion(1, 0);

        builder.MapPost("/auth/register", Register)
            .WithName("Register")
            .Produces<Ok<User>>(StatusCodes.Status200OK)
            .Produces<Conflict>(StatusCodes.Status409Conflict)
            .Produces<BadRequest>(StatusCodes.Status400BadRequest);

        builder.MapGet("/test", Test).WithName("Tesst").Produces<Ok<string>>(StatusCodes.Status200OK);
		return builder;
	}



    public static Ok<string> Test()
    {
        return TypedResults.Ok("Hello world!");
    }


    [KernelFunction]
	[Description("Register new user")]
	public static async Task<Results<Ok<User>, Conflict, BadRequest>> Register(RegisterRequest request)
	{
        var user = new User
        {
            EmployeeCode = request.EmployeeCode,
            DisplayName = request.DisplayName,
            Email = request.Email,
            PasswordHash = "123",
            AvatarUrl = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        return TypedResults.Ok(user);
        //var userExists = services.DbContext.Users.AsNoTracking().FirstOrDefault(u => u.EmployeeCode == request.EmployeeCode);
        //if (userExists != null)
        //	return TypedResults.Conflict();

        //var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        //var user = new User
        //{
        //	EmployeeCode = request.EmployeeCode,
        //	DisplayName = request.DisplayName,
        //	Email = request.Email,
        //	PasswordHash = passwordHash,
        //	AvatarUrl = null,
        //	CreatedAt = DateTime.UtcNow,
        //	UpdatedAt = DateTime.UtcNow
        //};
        //try
        //{
        //	await services.DbContext.Users.AddAsync(user);
        //	await services.DbContext.SaveChangesAsync();
        //	services.Logger.LogInformation("User registered");
        //	return TypedResults.Ok(user);
        //}
        //catch (Exception ex)
        //{
        //	services.Logger.LogError(ex, "Error registering user");
        //	return TypedResults.BadRequest();
        //}
    }
}
