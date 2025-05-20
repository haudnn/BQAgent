namespace Agent.Api.Models;

public class LoginRequest
{
    public required string EmployeeCode { get; set; }
    public required string Password { get; set; }
}