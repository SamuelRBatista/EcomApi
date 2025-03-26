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

      
    [HttpPost]

    public async Task<ActionResult<Supplier>> AddSupplierAsync(Supplier supplier)
    {
        if(supplier == null){
            return BadRequest("Supplier inválido. ");
        }
        await _supplierService.AddAsync(supplier);
       
        return Ok("Supplier cadastrado com sucesso.");
    }


}