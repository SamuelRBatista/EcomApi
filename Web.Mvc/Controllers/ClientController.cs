using Application.Services;
using Domain.Entities;  // Referência para o Product
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

  
    [HttpGet]      
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
    }
    
  
    [HttpPost]

    public async Task<ActionResult<Client>> AddClientAsync(Client client)
    {
        if(client == null){
            return BadRequest("Client inválido. ");
        }
        await _clientService.AddAsync(client);
       
        return Ok("Client cadastrado com sucesso.");
    }
 }
