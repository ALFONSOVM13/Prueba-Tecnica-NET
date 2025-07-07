namespace TravelRequests.Application.DTOs.Users;

public class PasswordResetRequestDto
{
    public string Email { get; set; } = string.Empty;
}

public class PasswordResetConfirmationDto
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
} 