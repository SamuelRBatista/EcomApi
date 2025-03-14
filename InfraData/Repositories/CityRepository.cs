using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using InfraData.Context;

namespace InfraData.Repositories;

public class CityRepository : ICityRepository
{
     private readonly AppDbContext _context;

     public CityRepository(AppDbContext context)
     {
        _context = context;
     }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _context.Cities
                .Include(c => c.State)
                .ToListAsync();
    }

    public Task<City?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<City>> GetByStateIdAsync(int stateId)
    {
        throw new NotImplementedException();
    }
}