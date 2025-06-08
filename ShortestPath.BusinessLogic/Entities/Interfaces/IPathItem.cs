namespace ShortestPath.BusinessLogic.Entities.Interfaces;

public interface IPathItem
{
    public string DisplayName { get; }
    public List<IPathItem> Connections { get; }
}