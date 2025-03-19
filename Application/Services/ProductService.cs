using Application.Validations;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;
    private readonly ProductValidator _validator;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
        _validator = new ProductValidator();
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Product> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task AddAsync(Product product)
    {
        var validationResult = _validator.Validate(product);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        bool isCodeBarUnique = await _repository.IsCodeBarUniqueAsync(product.BarCode);
        if (!isCodeBarUnique )
            throw new ValidationException("O código de barras já está em uso.");

        await _repository.AddAsync(product);
    } 
    public async Task UpdateAsync(Product product) => await _repository.UpdateAsync(product);
    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
}
