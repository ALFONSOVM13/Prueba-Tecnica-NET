using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TravelRequests.Domain.Authentication;
using TravelRequests.Domain.Interfaces;
using TravelRequests.Infrastructure.Authentication;
using TravelRequests.Infrastructure.Persistence;
using TravelRequests.Infrastructure.Persistence.Repositories;

namespace TravelRequests.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        
        services.AddScoped<ITravelRequestRepository, TravelRequestRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
} 