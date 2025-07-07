using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelRequests.Application.DTOs.TravelRequests;
using TravelRequests.Application.Interfaces;
using TravelRequests.Domain.Enums;

namespace TravelRequests.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TravelRequestsController : ControllerBase
{
    private readonly ITravelRequestService _travelRequestService;

    public TravelRequestsController(ITravelRequestService travelRequestService)
    {
        _travelRequestService = travelRequestService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TravelRequestDto>> Create(CreateTravelRequestDto createDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var result = await _travelRequestService.CreateAsync(createDto, userId);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TravelRequestDto>> GetById(int id)
    {
        var result = await _travelRequestService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (result.UserId != userId && userRole != "Manager" && userRole != "Admin")
        {
            return Forbid();
        }

        return Ok(result);
    }

    [HttpGet("my")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<TravelRequestDto>>> GetMyRequests()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var result = await _travelRequestService.GetUserRequestsAsync(userId);
        return Ok(result);
    }

    [Authorize(Roles = "Manager,Admin")]
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<TravelRequestDto>>> GetPendingRequests()
    {
        var result = await _travelRequestService.GetPendingRequestsAsync();
        return Ok(result);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TravelRequestDto>> UpdateStatus(int id, UpdateTravelRequestStatusDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var request = await _travelRequestService.UpdateStatusAsync(dto);
        return Ok(request);
    }
} 