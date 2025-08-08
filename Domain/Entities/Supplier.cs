
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Supplier
{
    public int Id {get; set;}
    public string Cnpj {get;set;}= string.Empty;
    public string Name {get;set;}= string.Empty;
    public string Email {get;set;}= string.Empty;
    public string Telephone {get; set;}= string.Empty;
    public string Address {get; set;}= string.Empty;
    public string Neighborhood {get; set;}= string.Empty;
    public string ZipCode {get; set;}= string.Empty;
    
    public int StateId {get; set;}
    [JsonIgnore]
    public State? State{get; set;}=null;
   
    public int CityId { get; set; }
    [JsonIgnore]
    public City? City { get; set; } = null;
    
}