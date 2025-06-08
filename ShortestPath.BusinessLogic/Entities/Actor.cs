using System.Text.Json.Serialization;

namespace ShortestPath.BusinessLogic.Entities;

public class Actor
{
    public string Name { get; set; }
    
    [JsonIgnore]
    public List<string> Movies { get; set; }
}