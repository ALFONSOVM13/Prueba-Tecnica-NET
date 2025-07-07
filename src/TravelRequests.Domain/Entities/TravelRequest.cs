using TravelRequests.Domain.Enums;

namespace TravelRequests.Domain.Entities;

public class TravelRequest
{
    public int Id { get; set; }
    public string OriginCity { get; set; } = string.Empty;
    public string DestinationCity { get; set; } = string.Empty;
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Justification { get; set; } = string.Empty;
    public TravelRequestStatus Status { get; set; }
    
    // Relaciones
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
} 