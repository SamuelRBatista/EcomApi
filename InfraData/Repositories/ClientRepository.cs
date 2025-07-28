using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using InfraData.Context;

namespace InfraData.Repositories;

public class ClientRepository : IClientReposiory
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Client> AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }
    public async Task<IEnumerable<Client>> GetAllAsync()
    {
          return await _context.Clients
               .Include(c => c.State)
               .Include(c => c.City)
               .ToListAsync();
    }
    public async Task<Client> GetByIdAsync(int id) => await _context.Clients.FindAsync(id);
    public Task<IEnumerable<Client>> GetByCityIdAsync(int cityId)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<Client>> GetByStateIdAsync(int stateId)
    {
        throw new NotImplementedException();
    }
    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();        
    }
    public async Task DeleteAsync(int id){
       var client = await _context.Clients.FindAsync(id);
        if(client != null)
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }   
}