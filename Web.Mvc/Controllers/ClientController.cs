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

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClientById(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client == null)
        {
            return NotFound(new { message = "Client não encontrado." });
        }

        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<Client>> AddClientAsync(Client client)
    {
        if (client == null)
        {
            return BadRequest("Client inválido. ");
        }
        await _clientService.AddAsync(client);

        return Ok("Client cadastrado com sucesso.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClientAsync(int id, [FromBody] Client client)
    {
        if (id != client.Id)
            return BadRequest(new { error = "O ID do cliente não corresponde ao ID fornecido." });

        await _clientService.UpdateAsync(client);
        return Ok(new { message = "Client atualizado com sucesso." });
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClientAsync(int id)
    {
        await _clientService.DeleteAsync(id);
        return Ok(new { message = "Cliente deletado com sucesso." });
    }
 }
