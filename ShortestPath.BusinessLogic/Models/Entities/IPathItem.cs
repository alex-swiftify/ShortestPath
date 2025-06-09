namespace ShortestPath.BusinessLogic.Models.Entities;

public interface IPathItem
{
    public string DisplayName { get; }
    public List<IPathItem> Connections { get; }
}