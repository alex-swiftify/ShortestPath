using System.Text.Json;
using ShortestPath.BusinessLogic.Models.Dtos;
using ShortestPath.BusinessLogic.Models.Entities;

namespace ShortestPath.BusinessLogic.Services;

public class MovieService
{
    private Dictionary<string, Movie> _movies = [];
    private Dictionary<string, Actor> _actors = [];

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
        
        foreach (MovieData movieData in moviesData.Movies)
        {
            foreach (string actorName in movieData.Cast)
            {
                // Create distinct Actors objects, one for each unique name
                _actors.TryAdd(actorName, new Actor(actorName));
            }

            _movies[movieData.Title] = new Movie(movieData.Title)
            {
                Cast = movieData.Cast
                    .Select(name => _actors[name])
                    .ToList()
            };
        }

        // Add backward references from Actor to Movies
        foreach (Movie movie in _movies.Values)
        foreach (Actor actor in movie.Cast)
        {
            actor.Movies.Add(movie);
        }
    }
    
    public IEnumerable<string> GetActors() => _actors.Keys;

    public IEnumerable<IPathItem>? FindShortestPath(string fromActor, string toActor)
    {
        _actors.TryGetValue(fromActor, out Actor? from);
        _actors.TryGetValue(toActor, out Actor? to);
        
        if (from is null || to is null)
            throw new InvalidOperationException("Cannot find actors with given names.");
        
        return FindShortestPath(from, to, [], []);
    }
    
    private List<IPathItem>? FindShortestPath(IPathItem from, IPathItem to, List<IPathItem> path, HashSet<IPathItem> visited)
    {
        if (!visited.Add(from))
            return null; // already visited

        path = [..path, from];

        if (from == to)
            return path;

        List<IPathItem>? shortestPath = null;

        foreach (IPathItem connection in from.Connections)
        {
            List<IPathItem>? subPath = FindShortestPath(connection, to, path, visited);
            
            if ((subPath?.Count ?? int.MaxValue) < (shortestPath?.Count ?? int.MaxValue)) {
                shortestPath = subPath;
            }
        }

        return shortestPath;
    }
}