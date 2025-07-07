using TravelRequests.Domain.Enums;

namespace TravelRequests.Application.DTOs.TravelRequests;

public record UpdateTravelRequestStatusDto(
    int Id,
    TravelRequestStatus Status,
    string? RejectionReason = null); 