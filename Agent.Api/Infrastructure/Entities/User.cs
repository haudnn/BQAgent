using System.ComponentModel.DataAnnotations.Schema;

namespace Agent.Api.Infrastructure.Entities;

[Table("users")]
public class User
{
  public Guid Id { get; set; } = Guid.CreateVersion7();
  public string EmployeeCode { get; set; } = default!;
  public string DisplayName { get; set; } = default!;
  public string Email { get; set; } = default!;
  public string PasswordHash { get; set; } = default!;
  public string? AvatarUrl { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}