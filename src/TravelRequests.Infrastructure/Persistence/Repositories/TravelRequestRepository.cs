using Microsoft.EntityFrameworkCore;
using TravelRequests.Domain.Entities;
using TravelRequests.Domain.Enums;
using TravelRequests.Domain.Interfaces;

namespace TravelRequests.Infrastructure.Persistence.Repositories;

public class TravelRequestRepository : ITravelRequestRepository
{
    private readonly ApplicationDbContext _context;

    public TravelRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TravelRequest>> GetAllAsync()
    {
        return await _context.TravelRequests
            .Include(tr => tr.User)
            .ToListAsync();
    }

    public async Task<TravelRequest> AddAsync(TravelRequest entity)
    {
        await _context.TravelRequests.AddAsync(entity);
        return entity;
    }

    public Task<TravelRequest> CreateAsync(TravelRequest travelRequest)
    {
        return AddAsync(travelRequest);
    }

    public async Task<TravelRequest?> GetByIdAsync(int id)
    {
        return await _context.TravelRequests
            .Include(tr => tr.User)
            .FirstOrDefaultAsync(tr => tr.Id == id);
    }

    public async Task<IEnumerable<TravelRequest>> GetByUserIdAsync(int userId)
    {
        return await _context.TravelRequests
            .Include(tr => tr.User)
            .Where(tr => tr.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<TravelRequest>> GetPendingRequestsAsync()
    {
        return await _context.TravelRequests
            .Include(tr => tr.User)
            .Where(tr => tr.Status == TravelRequestStatus.Pendiente)
            .ToListAsync();
    }

    public Task UpdateAsync(TravelRequest travelRequest)
    {
        _context.TravelRequests.Update(travelRequest);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TravelRequest entity)
    {
        _context.TravelRequests.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
} 