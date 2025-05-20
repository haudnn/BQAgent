using Agent.Api.Infrastructure.Data;
using Agent.Api.Infrastructure.Entities;
using Agent.Api.Interfaces;
using Agent.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Agent.Api.Services;

public class IdentityService(AgentDbContext dbContext) : IIdentityService
{
    private readonly AgentDbContext _dbContext = dbContext;

    public async Task<User?> Register(RegisterRequest request)
    {
        var userExists = _dbContext.Users.AsNoTracking().FirstOrDefault(u => u.EmployeeCode == request.EmployeeCode);
        if (userExists != null)
            return null;

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
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        catch
        {
            return null;
        }
    }

    public async Task<User?> Login(LoginRequest request)
    {
        try
        {
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.EmployeeCode == request.EmployeeCode);
        if (user == null)
            return null;
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return null;
        return user;
        }
        catch
        {
            return null;
        }
    }

    public async Task<User?> GetUserById(Guid id)
    {
        try
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return null;
            return user;
        }
        catch
        {
            return null;
        }
    }
}
