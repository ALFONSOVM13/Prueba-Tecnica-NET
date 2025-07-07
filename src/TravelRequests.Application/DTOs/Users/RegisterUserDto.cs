using TravelRequests.Domain.Enums;

namespace TravelRequests.Application.DTOs.Users;

public class RegisterUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
} 