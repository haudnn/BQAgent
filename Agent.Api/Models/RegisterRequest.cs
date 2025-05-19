using System.ComponentModel;

namespace Agent.Api.Models;

public class RegisterRequest
{
	[Description("Employee code")]
    public string EmployeeCode { get; set; } = default!;
	[Description("Name")]
    public string DisplayName { get; set; } = default!;
	[Description("Email")]
    public string Email { get; set; } = default!;
	[Description("Password")]
    public string Password { get; set; } = default!;
}
