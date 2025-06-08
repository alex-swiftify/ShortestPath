using System.Text.Json.Serialization;
using ShortestPath.BusinessLogic.Converters;
using ShortestPath.BusinessLogic.Entities.Interfaces;

namespace ShortestPath.BusinessLogic.Entities;

[JsonConverter(typeof(ActorConverter))]
public class Actor: IPathItem
{
    public string Name { get; set; }
    
    [JsonIgnore]
    public List<Movie> Movies { get; set; }

    public string DisplayName => Name;
    public List<IPathItem> Connections => [..Movies];
}