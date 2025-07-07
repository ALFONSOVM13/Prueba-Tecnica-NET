using TravelRequests.Domain.Entities;

namespace TravelRequests.Domain.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
} 