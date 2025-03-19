using System.Security.Cryptography;
using Domain.Entities;
using Domain.Interfaces;
using InfraData.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraData.Repositories;


public class SupplierRepository : ISupplierRepository
{

    private readonly AppDbContext _appDbContext;

    public SupplierRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Supplier> AddAsync(Supplier supplier)
    {
       _appDbContext.AddAsync(supplier);
       await _appDbContext.SaveChangesAsync();
       return supplier;
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await _appDbContext.Suppliers
                .Include(s => s.State)
                .Include(c => c.City)
                .ToListAsync();
    }

    public Task<IEnumerable<Supplier>> GetByCityIdAsync(int cityId)
    {
        throw new NotImplementedException();
    }

    public Task<Supplier?> GetById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Supplier>> GetByStateIdAsync(int stateId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Supplier supplier)
    {
        throw new NotImplementedException();
    }
}