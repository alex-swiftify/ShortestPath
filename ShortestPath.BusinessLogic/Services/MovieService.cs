using System.Text.Json;
using ShortestPath.BusinessLogic.Entities;
using ShortestPath.BusinessLogic.Entities.Interfaces;

namespace ShortestPath.BusinessLogic.Services;

public class MovieService
{
    private Dictionary<string, Movie> _movies = [];
    private Dictionary<string, Actor> _actors = [];

    public void LoadMovies()
    {
        string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Resources", "movies.json");
        string json = File.ReadAllText(jsonFilePath);
        
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var moviesElement = root.GetProperty("movies");
        
        foreach (var movieElement in moviesElement.EnumerateArray())
        {
            string movieTitle = movieElement.GetProperty("title").GetString() ?? string.Empty;
            
            IEnumerable<string> cast = movieElement.GetProperty("cast").EnumerateArray()
                .Select(actorElement => actorElement.GetString() ?? string.Empty);

            foreach (string actorName in cast)
            {
                if (!_actors.ContainsKey(actorName))
                    _actors[actorName] = new Actor(actorName);
            }
            
            _movies[movieTitle] = new Movie(movieTitle)
            {
                Cast = cast.Select(name => _actors[name]).ToList()
            };
        }

        // Add backward references from Actor to Movies
        foreach (Movie movie in _movies.Values)
        foreach (Actor actor in movie.Cast)
        {
            actor.Movies.Add(movie);
        }
    }
    
    public IEnumerable<IPathItem>? FindShortestPath(string fromActor, string toActor)
    {
        _actors.TryGetValue(fromActor, out Actor? from);
        _actors.TryGetValue(toActor, out Actor? to);
        
        if (from is null || to is null)
            throw new InvalidOperationException("Cannot find actors with given names.");
        
        return FindShortestPath(from, to, []);
    }
    
    private IEnumerable<IPathItem>? FindShortestPath(IPathItem from, IPathItem to, IEnumerable<IPathItem> visited)
    {
        if (from == to)
            return visited.Append(to);
        
        IEnumerable<IPathItem>? shortestPath = null;
        
        foreach (IPathItem connection in from.Connections.Except(visited))
        {
            IEnumerable<IPathItem>? path = FindShortestPath(connection, to, visited.Append(from));
            
            if ((path?.Count() ?? int.MaxValue) < (shortestPath?.Count() ?? int.MaxValue))
            {
                shortestPath = path;
            }
        }
        
        return shortestPath;
    }
}