using TravelRequests.Application.DTOs.TravelRequests;

namespace TravelRequests.Application.Interfaces;

public interface ITravelRequestService
{
    Task<IEnumerable<TravelRequestDto>> GetAllAsync();
    Task<TravelRequestDto> CreateAsync(CreateTravelRequestDto dto, int userId);
    Task<TravelRequestDto?> GetByIdAsync(int id);
    Task<IEnumerable<TravelRequestDto>> GetUserRequestsAsync(int userId);
    Task<IEnumerable<TravelRequestDto>> GetPendingRequestsAsync();
    Task<TravelRequestDto> UpdateStatusAsync(UpdateTravelRequestStatusDto dto);
} 