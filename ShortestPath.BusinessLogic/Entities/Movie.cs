using System.Text.Json.Serialization;
using ShortestPath.BusinessLogic.Entities.Interfaces;

namespace ShortestPath.BusinessLogic.Entities;

public class Movie(string title, List<Actor> cast) : IPathItem
{
    public string Title { get; } = title;

    public List<Actor> Cast { get; } = cast;

    [JsonIgnore]
    public string DisplayName => Title;

    [JsonIgnore]
    public List<IPathItem> Connections => [..Cast];
}