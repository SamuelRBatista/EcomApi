using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Product
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price {get; set;}
    public string Sku {get; set;} = string.Empty;
    public string BarCode {get; set;} = string.Empty;
    public string ImageUrl { get; set; } 
    public int CategoryId {get; set;}
    [JsonIgnore]
    public Category? Category{get; set;}= null!;
    private bool IsValidCodeBar(string barcode)
    {
        return barcode.Length == 13 && barcode.All(char.IsDigit);
    }
}

