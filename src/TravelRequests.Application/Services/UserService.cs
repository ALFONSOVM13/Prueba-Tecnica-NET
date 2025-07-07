using TravelRequests.Application.DTOs.Users;
using TravelRequests.Application.Interfaces;
using TravelRequests.Domain.Interfaces;
using TravelRequests.Domain.Entities;
using TravelRequests.Application.Exceptions;
using System.Security.Cryptography;
using System.Text;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace TravelRequests.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<UserDto> RegisterAsync(RegisterUserDto registerDto)
    {
        try
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("Email", "El email ya está registrado")
                };
                throw new ValidationException(failures);
            }

            var emailExists = await _userRepository.ExistsAsync(registerDto.Email);
            if (emailExists)
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("Email", "El email ya está registrado")
                };
                throw new ValidationException(failures);
            }

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password),
                Role = registerDto.Role
            };

            try
            {
                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("Email", "El email ya está registrado")
                };
                throw new ValidationException(failures);
            }
            
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante el registro: {ex.Message}");
            throw;
        }
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UnauthorizedException("Usuario no encontrado");
            }

            var hashedPassword = HashPassword(loginDto.Password);
            
            if (user.PasswordHash != hashedPassword)
            {
                throw new UnauthorizedException("Contraseña incorrecta");
            }


            var token = _jwtTokenGenerator.GenerateToken(user);
            return token;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante el login: {ex.Message}");
            throw;
        }
    }

    public Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> RequestPasswordResetAsync(PasswordResetRequestDto requestDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPasswordAsync(PasswordResetConfirmationDto confirmationDto)
    {
        throw new NotImplementedException();
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
} 