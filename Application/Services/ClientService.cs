using Application.Validations;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services;

public class ClientService
{
    private readonly IClientReposiory _clientRepository;
    private readonly ClientValidator _clientValidator;

    public ClientService(IClientReposiory clientRepository, ClientValidator clientValidator)
    {
        _clientRepository = clientRepository;
        _clientValidator = clientValidator;
    }

    public async Task<IEnumerable<Client>> GetAllAsync(){
         return await _clientRepository.GetAllAsync();
    }
    public async Task AddAsync(Client client)
    {
        var validationResult = _clientValidator.Validate(client);
        if (!validationResult.IsValid)
             throw new ValidationException(validationResult.Errors);

        // bool isCodeBarUnique = await _repository.IsCodeBarUniqueAsync(product.BarCode);
        // if (!isCodeBarUnique )
        //     throw new ValidationException("O código de barras já está em uso.");

        await _clientRepository.AddAsync(client);
    } 

}