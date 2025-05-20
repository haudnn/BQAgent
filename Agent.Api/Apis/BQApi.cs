using Agent.Api.Infrastructure.Entities;
using Agent.Api.Models;
using Agent.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
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
         .Produces<BadRequest>(StatusCodes.Status400BadRequest);

        builder.MapPost("/auth/login", Login)
         .WithName("Login")
         .Produces<Ok<User>>(StatusCodes.Status200OK)
         .Produces<BadRequest>(StatusCodes.Status400BadRequest);

        builder.MapPost("/users/{id:guid}", GetUserById)
         .WithName("GetUserById")
         .Produces<Ok<User>>(StatusCodes.Status200OK)
         .Produces<NotFound>(StatusCodes.Status404NotFound);

        return builder;
    }

    [KernelFunction]
    [Description("Register new user")]
    public static async Task<Results<Ok<User>, BadRequest>> Register([AsParameters] SystemServices services, RegisterRequest request)
    {
        var user = await services.IdentityService.Register(request);
        if (user == null)
            return TypedResults.BadRequest();
        return TypedResults.Ok(user);
    }

    public static async Task<Results<Ok<User>, BadRequest>> Login([AsParameters] SystemServices services, LoginRequest request)
    {
        var user = await services.IdentityService.Login(request);
        if (user == null)
            return TypedResults.BadRequest();
        return TypedResults.Ok(user);
    }

    public static async Task<Results<Ok<User>, NotFound>> GetUserById([AsParameters] SystemServices services, Guid id)
    {
        var user = await services.IdentityService.GetUserById(id);
        if (user == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(user);
    }
}
