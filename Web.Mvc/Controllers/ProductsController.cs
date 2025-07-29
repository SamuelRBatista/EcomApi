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
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
    {
        var imagePath = "";

        if (request.Image != null && request.Image.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}_{request.Image.FileName}";
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");

      
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            imagePath = $"/uploads/products/{fileName}";
        }

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Sku = request.Sku,
            BarCode = request.BarCode,
            CategoryId = request.CategoryId,
            ImageUrl = imagePath
        };

        await _productService.AddAsync(product);

        return Ok(product);
    }


    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateProductAsync(int id, [FromForm] UpdateCreateRequest request)
    {
        if (id != request.Id)
            return BadRequest(new { error = "O ID do produto não corresponde ao ID fornecido." });

        string imagePath = request.ExistingImageUrl ?? ""; // Caminho da imagem atual mantido, se não for alterado

        if (request.Image != null && request.Image.Length > 0)
        {
            // Apagar imagem anterior (se houver)
            if (!string.IsNullOrEmpty(request.ExistingImageUrl))
            {
                var trimmedPath = request.ExistingImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","uploads", "products", trimmedPath);

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            // Salvar nova imagem
            var fileName = $"{Guid.NewGuid()}_{request.Image.FileName}";
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");

            // Garantir que o diretório exista
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            imagePath = $"/uploads/products/{fileName}";
        }

        var product = new Product
        {
            Id = id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Sku = request.Sku,
            BarCode = request.BarCode,
            CategoryId = request.CategoryId,
            ImageUrl = imagePath
        };

        await _productService.UpdateAsync(product);

        return Ok(new { message = "Produto atualizado com sucesso.", product });
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        await _productService.DeleteAsync(id);
        return Ok(new { message = "Produto deletado com sucesso." });
    }
}
