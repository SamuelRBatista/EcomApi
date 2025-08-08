using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SupplierController :  ControllerBase
{

    private readonly SupplierService _supplierService;

    public SupplierController(SupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]      
    public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
    {
        var suppliers = await _supplierService.GetAllAsync();
        return Ok(suppliers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Supplier>> GetSupplierById(int id)
    {
        var supplier = await _supplierService.GetByIdAsync(id);
        if (supplier == null)
        {
            return NotFound(new { message = "Fornecedor não encontrado." });
        }

        return Ok(supplier);
    }


    [HttpPost]
    public async Task<ActionResult<Supplier>> AddSupplierAsync(Supplier supplier)
    {
        if(supplier == null){
            return BadRequest("Supplier inválido. ");
        }
        await _supplierService.AddAsync(supplier);
       
        return Ok("Supplier cadastrado com sucesso.");
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplierAsync(int id, [FromBody] Supplier supplier)
    {
        if (id != supplier.Id)
            return BadRequest(new { error = "O ID do fornecedor não corresponde ao ID fornecido." });
        await _supplierService.UpdateAsync(supplier);
        return Ok(new { message = "Client atualizado com sucesso." });

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplierAsync(int id)
    {
        await _supplierService.DeleteAsync(id);
        return Ok(new { message = "Fornecedor deletado com sucesso." });

    }


}