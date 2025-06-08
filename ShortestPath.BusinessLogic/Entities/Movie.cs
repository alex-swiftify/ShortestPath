using ShortestPath.BusinessLogic.Entities.Interfaces;

namespace ShortestPath.BusinessLogic.Entities;

public class Movie: IPathItem
{
    public string Title { get; set; }
    
    public List<Actor> Cast { get; set; }
    
    public string DisplayName => Title;
    public List<IPathItem> Connections => [..Cast];
}