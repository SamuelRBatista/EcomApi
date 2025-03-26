using Domain.Entities;
using Domain.Interfaces;
using InfraData.Context;
using Microsoft.EntityFrameworkCore;


namespace InfraData.Repositories;


public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public  async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();
    public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
    public async Task<Product> AddAsync(Product product){
                
         _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    
    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();        
    }   

    public async Task DeleteAsync(int id){
       var product = await _context.Products.FindAsync(id);
        if(product != null)
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsCodeBarUniqueAsync(string barCode)
    {
        var existingBarCode = await _context.Products.Where(p => p.BarCode == barCode).FirstOrDefaultAsync();
        return existingBarCode == null;
    }
}