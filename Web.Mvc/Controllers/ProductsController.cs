using Application.Services;
using Domain.Entities;
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
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
    var product = await _productService.GetByIdAsync(id);
    if (product == null)
    {
        return NotFound(new { message = "Produto não encontrado." });
    }

    return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> AddProductAsync([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest(new { error = "Produto inválido." });
        }

        await _productService.AddAsync(product);

        return Ok(new
        {
            message = "Produto cadastrado com sucesso.",
            product = product
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] Product product)
    {
        if (id != product.Id)
            return BadRequest(new { error = "O ID do produto não corresponde ao ID fornecido." });

        await _productService.UpdateAsync(product);
        return Ok(new { message = "Produto atualizado com sucesso." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        await _productService.DeleteAsync(id);
        return Ok(new { message = "Produto deletado com sucesso." });
    }
}
