using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelRequests.Application.DTOs.Users;
using TravelRequests.Application.Interfaces;
using TravelRequests.Domain.Enums;

namespace TravelRequests.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerDto)
    {
        var result = await _userService.RegisterAsync(registerDto);
        return CreatedAtAction(nameof(Register), result);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> Login(LoginDto loginDto)
    {
        var token = await _userService.LoginAsync(loginDto);
        return Ok(new { Token = token });
    }

    [HttpPost("password-reset-request")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> RequestPasswordReset(PasswordResetRequestDto requestDto)
    {
        var code = await _userService.RequestPasswordResetAsync(requestDto);
        return Ok(new { ResetCode = code });
    }

    [HttpPost("password-reset-confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ResetPassword(PasswordResetConfirmationDto confirmationDto)
    {
        await _userService.ResetPasswordAsync(confirmationDto);
        return Ok();
    }

    [Authorize(Roles = nameof(UserRole.Aprobador))]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
} 