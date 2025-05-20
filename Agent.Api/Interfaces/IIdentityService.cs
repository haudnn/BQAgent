using Agent.Api.Infrastructure.Entities;
using Agent.Api.Models;

namespace Agent.Api.Interfaces;

public interface IIdentityService
{
    Task<User?> Register(RegisterRequest request);
    Task<User?> Login(LoginRequest request);
    Task<User?> GetUserById(Guid id);
}