using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ClientService
{
    private readonly IClientReposiory _clientRepository;

    public ClientService(IClientReposiory clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Client>> GetAllAsync(){
         return await _clientRepository.GetAllAsync();
    }
    public async Task AddAsync(Client client)
    {
        // var validationResult = _validator.Validate(product);
        // if (!validationResult.IsValid)
        //     throw new ValidationException(validationResult.Errors);

        // bool isCodeBarUnique = await _repository.IsCodeBarUniqueAsync(product.BarCode);
        // if (!isCodeBarUnique )
        //     throw new ValidationException("O código de barras já está em uso.");

        await _clientRepository.AddAsync(client);
    } 

}