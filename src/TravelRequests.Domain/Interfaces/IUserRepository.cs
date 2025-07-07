using TravelRequests.Domain.Entities;

namespace TravelRequests.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsAsync(string email);
    Task<User?> GetByPasswordResetCodeAsync(string code);
} 