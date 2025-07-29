using Microsoft.AspNetCore.Http;

public class ProductCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string BarCode { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public IFormFile? Image { get; set; }  // Aqui sim é correto usar IFormFile
}
