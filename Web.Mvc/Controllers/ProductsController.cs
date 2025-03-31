using Application.Services;
using Domain.Entities;  // Referência para o Product
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

  
    [HttpGet]
    // [Authorize] 
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }
    
  
    [HttpPost]
    // [Authorize] 
    public async Task<ActionResult<Product>> AddProductAsync(Product product)
    {
        if(product == null){
            return BadRequest("Produto inválido. ");
        }
        await _productService.AddAsync(product);
        //return CreatedAtAction(nameof(GetProductByIdAsync), new { id = product.Id }, product);
        return Ok("Produto cadastrado com sucesso.");
    }

    [HttpPut("{id}")]
    // [Authorize]
    public async Task<IActionResult> UpdateProductAsync(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest("O ID do produto não corresponde ao ID fornecido.");

        await _productService.UpdateAsync(product);
        return Ok("Produto atualizado com sucesso.");
    }

    [HttpDelete("{id}")]
    // [Authorize]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        await _productService.DeleteAsync(id);
        return Ok("Produto deletado com sucesso.");
    }
}
