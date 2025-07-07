using TravelRequests.Domain.Entities;

namespace TravelRequests.Domain.Interfaces;

public interface ITravelRequestRepository : IRepository<TravelRequest>
{
    Task<IEnumerable<TravelRequest>> GetByUserIdAsync(int userId);
    Task<IEnumerable<TravelRequest>> GetPendingRequestsAsync();
} 