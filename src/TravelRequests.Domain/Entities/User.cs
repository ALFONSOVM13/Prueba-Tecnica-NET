using TravelRequests.Domain.Enums;

namespace TravelRequests.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? PasswordResetCode { get; set; }
    public DateTime? PasswordResetCodeExpiration { get; set; }
    
    // Relación con las solicitudes de viaje
    public virtual ICollection<TravelRequest> TravelRequests { get; set; } = new List<TravelRequest>();
} 