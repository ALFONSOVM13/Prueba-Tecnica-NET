using TravelRequests.Application.DTOs.Users;

namespace TravelRequests.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> RegisterAsync(RegisterUserDto registerDto);
    Task<string> LoginAsync(LoginDto loginDto);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<string> RequestPasswordResetAsync(PasswordResetRequestDto requestDto);
    Task<bool> ResetPasswordAsync(PasswordResetConfirmationDto confirmationDto);
} 