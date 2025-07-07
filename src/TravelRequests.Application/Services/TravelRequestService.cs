using FluentValidation;
using TravelRequests.Application.DTOs.TravelRequests;
using TravelRequests.Application.Exceptions;
using TravelRequests.Application.Interfaces;
using TravelRequests.Domain.Entities;
using TravelRequests.Domain.Enums;
using TravelRequests.Domain.Interfaces;

namespace TravelRequests.Application.Services;

public class TravelRequestService : ITravelRequestService
{
    private readonly ITravelRequestRepository _travelRequestRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateTravelRequestDto> _createValidator;

    public TravelRequestService(
        ITravelRequestRepository travelRequestRepository,
        IUserRepository userRepository,
        IValidator<CreateTravelRequestDto> createValidator)
    {
        _travelRequestRepository = travelRequestRepository;
        _userRepository = userRepository;
        _createValidator = createValidator;
    }

    public async Task<IEnumerable<TravelRequestDto>> GetAllAsync()
    {
        var requests = await _travelRequestRepository.GetAllAsync();
        return requests.Select(r => new TravelRequestDto
        {
            Id = r.Id,
            UserId = r.UserId,
            UserName = r.User?.Name ?? string.Empty,
            OriginCity = r.OriginCity,
            DestinationCity = r.DestinationCity,
            DepartureDate = r.DepartureDate,
            ReturnDate = r.ReturnDate,
            Justification = r.Justification,
            Status = r.Status
        });
    }

    public async Task<TravelRequestDto> CreateAsync(CreateTravelRequestDto dto, int userId)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new Exceptions.ValidationException(validationResult.Errors);
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User", userId);
        }

        var request = new TravelRequest
        {
            UserId = userId,
            OriginCity = dto.OriginCity,
            DestinationCity = dto.DestinationCity,
            DepartureDate = dto.DepartureDate,
            ReturnDate = dto.ReturnDate,
            Justification = dto.Justification,
            Status = TravelRequestStatus.Pendiente
        };

        await _travelRequestRepository.AddAsync(request);
        await _travelRequestRepository.SaveChangesAsync();

        return new TravelRequestDto
        {
            Id = request.Id,
            UserId = request.UserId,
            UserName = user.Name,
            OriginCity = request.OriginCity,
            DestinationCity = request.DestinationCity,
            DepartureDate = request.DepartureDate,
            ReturnDate = request.ReturnDate,
            Justification = request.Justification,
            Status = request.Status
        };
    }

    public async Task<TravelRequestDto?> GetByIdAsync(int id)
    {
        var request = await _travelRequestRepository.GetByIdAsync(id);
        if (request == null) return null;

        return new TravelRequestDto
        {
            Id = request.Id,
            UserId = request.UserId,
            UserName = request.User?.Name ?? string.Empty,
            OriginCity = request.OriginCity,
            DestinationCity = request.DestinationCity,
            DepartureDate = request.DepartureDate,
            ReturnDate = request.ReturnDate,
            Justification = request.Justification,
            Status = request.Status
        };
    }

    public async Task<IEnumerable<TravelRequestDto>> GetUserRequestsAsync(int userId)
    {
        var requests = await _travelRequestRepository.GetByUserIdAsync(userId);
        return requests.Select(r => new TravelRequestDto
        {
            Id = r.Id,
            UserId = r.UserId,
            UserName = r.User?.Name ?? string.Empty,
            OriginCity = r.OriginCity,
            DestinationCity = r.DestinationCity,
            DepartureDate = r.DepartureDate,
            ReturnDate = r.ReturnDate,
            Justification = r.Justification,
            Status = r.Status
        });
    }

    public async Task<IEnumerable<TravelRequestDto>> GetPendingRequestsAsync()
    {
        var requests = await _travelRequestRepository.GetPendingRequestsAsync();
        return requests.Select(r => new TravelRequestDto
        {
            Id = r.Id,
            UserId = r.UserId,
            UserName = r.User?.Name ?? string.Empty,
            OriginCity = r.OriginCity,
            DestinationCity = r.DestinationCity,
            DepartureDate = r.DepartureDate,
            ReturnDate = r.ReturnDate,
            Justification = r.Justification,
            Status = r.Status
        });
    }

    public async Task<TravelRequestDto> UpdateStatusAsync(UpdateTravelRequestStatusDto dto)
    {
        var request = await _travelRequestRepository.GetByIdAsync(dto.Id);
        if (request == null)
        {
            throw new NotFoundException($"Travel request with ID {dto.Id} not found.");
        }

        request.Status = dto.Status;
        await _travelRequestRepository.UpdateAsync(request);
        await _travelRequestRepository.SaveChangesAsync();

        return new TravelRequestDto
        {
            Id = request.Id,
            UserId = request.UserId,
            UserName = request.User?.Name ?? string.Empty,
            OriginCity = request.OriginCity,
            DestinationCity = request.DestinationCity,
            DepartureDate = request.DepartureDate,
            ReturnDate = request.ReturnDate,
            Justification = request.Justification,
            Status = request.Status
        };
    }
} 