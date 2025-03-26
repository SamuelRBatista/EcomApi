using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;


public class SupplierService
{
    public readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

     public async Task<IEnumerable<Supplier>> GetAllAsync(){
         return await _supplierRepository.GetAllAsync();
    }


     public async Task AddAsync(Supplier supplier)
    {
        // var validationResult = _validator.Validate(product);
        // if (!validationResult.IsValid)
        //     throw new ValidationException(validationResult.Errors);

        // bool isCodeBarUnique = await _repository.IsCodeBarUniqueAsync(product.BarCode);
        // if (!isCodeBarUnique )
        //     throw new ValidationException("O código de barras já está em uso.");

        await _supplierRepository.AddAsync(supplier);
    } 






}