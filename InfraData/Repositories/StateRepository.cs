using Domain.Entities;
using Domain.Interfaces;
using InfraData.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraData.Repositories;


public class StateRespository : IStateReposiory
{
    private readonly AppDbContext _context;

    public StateRespository(AppDbContext context){

        _context = context;
    }

    public async Task<IEnumerable<State>> GetAllAsync()
    {
       return  await _context.States
                .Include(s => s.Cities)
                .ToListAsync();
    }

    public Task<State?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<State?> GetByUfAsync(string uf)
    {
        throw new NotImplementedException();
    }
}