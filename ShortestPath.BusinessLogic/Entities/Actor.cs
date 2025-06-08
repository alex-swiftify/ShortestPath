using System.Text.Json.Serialization;
using ShortestPath.BusinessLogic.Converters;
using ShortestPath.BusinessLogic.Entities.Interfaces;

namespace ShortestPath.BusinessLogic.Entities;

[JsonConverter(typeof(ActorConverter))]
public class Actor(string name) : IPathItem
{
    public string Name { get; } = name;

    [JsonIgnore]
    public List<Movie> Movies { get; set; } = [];

    [JsonIgnore]
    public string DisplayName => Name;
    
    [JsonIgnore]
    public List<IPathItem> Connections => [..Movies];
}