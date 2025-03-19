using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Client 
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Cpf {get; set;}
    public string Email {get; set;}
    public string PhoneNumber {get;set;}
    public string Address {get; set;}
    public string Neighborhood {get; set;}
    public string ZipCode {get; set;}
    
    public int StateId {get; set;}
    [JsonIgnore]
    public State? State{get; set;}=null;
   
    public int CityId { get; set; }
    [JsonIgnore]
    public City? City { get; set; } = null;

}