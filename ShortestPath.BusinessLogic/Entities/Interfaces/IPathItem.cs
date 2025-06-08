using System.Text.Json.Serialization;

namespace ShortestPath.BusinessLogic.Entities.Interfaces;

public interface IPathItem
{
    [JsonIgnore]
    public string DisplayName { get; }
    
    [JsonIgnore]
    public List<IPathItem> Connections { get; }
}