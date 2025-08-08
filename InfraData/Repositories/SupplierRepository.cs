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
    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await _appDbContext.Suppliers
                .Include(s => s.State)
                .Include(c => c.City)
                .ToListAsync();
    }

    public async Task<Supplier> GetByIdAsync(int id) => await _appDbContext.Suppliers.FindAsync(id);

    public async Task<Supplier> AddAsync(Supplier supplier)
    {
       _appDbContext.AddAsync(supplier);
       await _appDbContext.SaveChangesAsync();
       return supplier;
    }
   

   

    public async Task UpdateAsync(Supplier supplier)
    {
        _appDbContext.Suppliers.Update(supplier);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var supplier = await _appDbContext.Suppliers.FindAsync(id);
        if (supplier != null)
            _appDbContext.Suppliers.Remove(supplier);
        await _appDbContext.SaveChangesAsync();
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
}