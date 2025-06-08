using ShortestPath.BusinessLogic.Entities.Interfaces;

namespace ShortestPath.BusinessLogic.Entities;

public class Movie(string title) : IPathItem
{
    public string Title { get; } = title;
    public List<Actor> Cast { get; set; } = [];

    public string DisplayName => Title;
    public List<IPathItem> Connections => [..Cast];

    public override string ToString() => DisplayName;
}