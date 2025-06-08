using ShortestPath.BusinessLogic.Entities.Interfaces;

namespace ShortestPath.BusinessLogic.Entities;

public class Actor(string name) : IPathItem
{
    public string Name { get; } = name;
    public List<Movie> Movies { get; set; } = [];

    public string DisplayName => Name;
    public List<IPathItem> Connections => [..Movies];
}