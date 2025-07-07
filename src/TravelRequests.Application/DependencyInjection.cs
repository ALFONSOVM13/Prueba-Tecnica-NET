using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TravelRequests.Application.DTOs.TravelRequests;
using TravelRequests.Application.DTOs.Users;
using TravelRequests.Application.Interfaces;
using TravelRequests.Application.Services;
using TravelRequests.Application.Validators.TravelRequests;
using TravelRequests.Application.Validators.Users;

namespace TravelRequests.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITravelRequestService, TravelRequestService>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IValidator<CreateTravelRequestDto>, CreateTravelRequestDtoValidator>();
        services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

        return services;
    }
} 