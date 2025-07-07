using TravelRequests.Domain.Enums;

namespace TravelRequests.Application.DTOs.TravelRequests;

public class TravelRequestDto
{
    public int Id { get; set; }
    public string OriginCity { get; set; } = string.Empty;
    public string DestinationCity { get; set; } = string.Empty;
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Justification { get; set; } = string.Empty;
    public TravelRequestStatus Status { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
} 