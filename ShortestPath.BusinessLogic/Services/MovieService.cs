using System.Text.Json;
using ShortestPath.BusinessLogic.Entities;
using ShortestPath.BusinessLogic.Entities.Interfaces;

namespace ShortestPath.BusinessLogic.Services;

public class MovieService
{
    private List<Movie> _movies = [];
    private List<Actor> _actors = [];

    public void LoadMovies()
    {
        string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Resources", "movies.json");
        string json = File.ReadAllText(jsonFilePath);
        
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var moviesData = JsonSerializer.Deserialize<MoviesData>(json, options);
        
        if (moviesData == null)
            throw new InvalidOperationException("Deserialization resulted in null movies data.");
        
        _movies = moviesData.Movies;

        // Initialize backward references from Actor to Movies
        foreach (var movie in _movies)
        foreach (var actor in movie.Cast)
            actor.Movies.Add(movie);
        
        _actors = _movies
            .SelectMany(m => m.Cast)
            .DistinctBy(c => c.Name)
            .ToList();
    }
    
    public List<IPathItem>? FindShortestPath(string fromActor, string toActor)
    {
        IPathItem? from = _actors.FirstOrDefault(a => a.Name == fromActor);
        IPathItem? to = _actors.FirstOrDefault(a => a.Name == toActor);
        
        if (from is null || to is null)
            throw new InvalidOperationException("Cannot find actors with given names.");
        
        return FindShortestPath(from, to, []);
    }
    
    private List<IPathItem>? FindShortestPath(IPathItem from, IPathItem to, List<IPathItem> visited)
    {
        if (from == to)
            return [to];
        
        if (from.Connections.Contains(to))
            return [from, to];
        
        List<IPathItem>? shortestSubPath = null;
        
        foreach (IPathItem connection in from.Connections.Except(visited))
        {
            List<IPathItem>? subPath = FindShortestPath(connection, to, visited.Append(from).ToList());
            
            if ((subPath?.Count ?? int.MaxValue) < (shortestSubPath?.Count ?? 0))
            {
                shortestSubPath = subPath;
            }
        }
        
        if (shortestSubPath == null)
            return null;
        
        return visited.Concat(shortestSubPath).ToList();
    }
}