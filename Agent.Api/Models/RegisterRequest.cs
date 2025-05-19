namespace Agent.Api.Models;

public class RegisterRequest
{
	public string EmployeeCode { get; set; } = default!;
	public string DisplayName { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string Password { get; set; } = default!;
}
